﻿using System.Collections.Generic;
using UnityEngine;
using XTable;

/// <summary>
/// entity 可能是多重身份（如既是Ally 又是Role 又是Player）
/// </summary>
public enum EntityType
{
    //表现
    Entity = 1 << 0,
    Role = 1 << 1,
    Player = 1 << 2,
    Monster = 1 << 3,
    Boss = 1 << 4,
    Npc = 1 << 5,

    //同盟
    Ship_Start = 6,
    Enemy = 1 << 6, //敌对 
    Ally = 1 << 7,  //友好
    AllyAll = 1 << 8, //双方友好 如礼物
    EnemyAll = 1 << 9, //双方敌对 如风火轮
    EProtected = 1 << 10,//敌对但不可受击 如隐形怪
    AProtected = 1 << 11,//友军不可受击 如安全区域
    Ship_End = 11
}


public class XEntity : XObject
{
    protected EntityType _eEntity_Type = EntityType.Entity;
    protected XAttributes _attr = null;
    protected XEntityPresentation.RowData _present;
    protected GameObject _object = null;
    protected Transform _transf = null;
    protected int _layer = 0;
    protected float _speed = 0.03f;
    protected bool _freeze = false;
    protected SkinnedMeshRenderer _skin = null;
    protected Vector3 _forward = Vector3.zero;
    protected bool _force_move = false;
    private Vector3 _pos = Vector3.zero;
    protected XStateDefine _state = XStateDefine.XState_Idle;
    protected XAnimComponent anim;
    protected Dictionary<uint, XComponent> components;

    public uint EntityID
    {
        get { return _attr != null ? _attr.id : 0; }
    }

    public bool IsPlayer
    {
        get { return (_eEntity_Type & EntityType.Player) != 0; }
    }

    public bool IsRole
    {
        get { return (_eEntity_Type & EntityType.Role) != 0; }
    }
    
    public bool IsBoss
    {
        get { return (_eEntity_Type & EntityType.Boss) != 0; }
    }

    public bool IsNpc
    {
        get { return (_eEntity_Type & EntityType.Npc) != 0; }
    }

    public XStateDefine CurState { get { return _state; } }

    public float Radius
    {
        get { return _present != null ? _present.BoundRadius : 0f; }
    }

    public float Height
    {
        get { return _present != null ? _present.BoundHeight : 0f; }
    }

    public EntityType Type
    {
        get { return _eEntity_Type; }
    }

    public GameObject EntityObject
    {
        get { return _object; }
    }

    public Transform EntityTransfer
    {
        get { return _transf; }
    }

    public XAttributes EntityAttribute
    {
        get { return _attr; }
    }

    public float Speed
    {
        get { return _freeze ? 0.001f : _speed; }
    }
    
    public Vector3 Position
    {
        get { return _transf != null ? _transf.position : Vector3.zero; }
    }

    public Vector3 Forward
    {
        get { return Rotation * Vector3.forward; }
    }

    public Quaternion Rotation
    {
        get { return _transf != null ? _transf.rotation : Quaternion.identity; }
    }

    public XEntityPresentation.RowData present
    {
        get { return _present; }
    }

    public bool IsDead
    {
        get { return _attr == null ? true : _attr.IsDead; }
    }

    public SkinnedMeshRenderer skin
    {
        get { return _skin; }
        set { _skin = value; }
    }

    public float CreateTime { get; set; }

    public int Wave { get; set; }

    public XAttributes Attributes { get { return _attr; } }

    public void Initilize(GameObject o, XAttributes attr)
    {
        base.Initilize();
        _object = o;
        _transf = o.transform;
        _attr = attr;
        _present = XTableMgr.GetTable<XEntityPresentation>().GetItemID(_attr.PresentID);
        components = new Dictionary<uint, XComponent>();
        anim = AttachComponent<XAnimComponent>();
        OnInitial();
        InitAnim();
    }

    public void UnloadEntity()
    {
        OnUnintial();
        _attr = null;
        XResources.Destroy(_object);
        _object = null;
        DetachAllComponents();
        base.Unload();
    }

    public virtual void OnInitial() { }

    protected virtual void OnUnintial() { }

    protected virtual void InitAnim() { }

    public virtual void OnUpdate(float delta)
    {
        UpdateComponents(delta);
        
        if (_force_move && _transf != null)
        {
            _transf.forward = _forward;
            _pos = Position + _forward.normalized * Speed;
            _pos.y = XScene.singleton.TerrainY(_pos) + 0.02f;
            _transf.position = _pos;
        }
    }

    public virtual void OnLateUpdate() { }

    public bool TestVisible(Plane[] planes, bool fully)
    {
        if (_skin != null)
        {
            Bounds bound = _skin.bounds;
            if (fully)
            {
                for (int i = 0; i < planes.Length; i++)
                {
                    if (planes[i].GetDistanceToPoint(bound.min) < 0 ||
                        planes[i].GetDistanceToPoint(bound.max) < 0)
                    {
                        return false;
                    }
                }
                return true;
            }
            else
                return GeometryUtility.TestPlanesAABB(planes, bound);
        }
        return false;
    }

    public static bool Valide(Transform transf)
    {
        uint id = uint.Parse(transf.name);
        XEntity e = XEntityMgr.singleton.GetEntity(id);
        return Valide(e);
    }

    public static bool Valide(XEntity e)
    {
        return e != null && !e.IsDead && !e.Deprecated && e.Attributes != null;
    }

    public void MoveForward(Vector3 forward)
    {
        _forward = forward;
        _force_move = true;
        XAnimComponent anim = GetComponent<XAnimComponent>();
        if (anim != null)
        {
            anim.SetTrigger(AnimTriger.ToMove);
            _state = XStateDefine.XState_Move;
        }
    }

    public void StopMove()
    {
        _force_move = false;
        _forward = Vector3.zero;
        XAnimComponent anim = GetComponent<XAnimComponent>();
        if (anim != null)
        {
            anim.SetTrigger(AnimTriger.ToMove, false);
            anim.SetTrigger(AnimTriger.ToStand);
            _state = XStateDefine.XState_Idle;
        }
    }

    public void OnDied()
    {
        _state = XStateDefine.XState_Death;
        DetachAllComponents();
        XAnimComponent an = GetComponent<XAnimComponent>();
        if (an != null)
        {
            AnimationClip clip = XResources.Load<AnimationClip>(present.Death, AssetType.Anim);
            an.OverrideAnim(AnimTriger.ToDeath, clip);
        }
    }

    public virtual void OnSkill(bool cast)
    {
        if (_state != XStateDefine.XState_Death)
        {
            _state = cast ? XStateDefine.XState_Skill : XStateDefine.XState_Idle;
            if (IsPlayer) _freeze = cast;
        }
    }

    public virtual void OnHit(bool hit)
    {
        if (_state != XStateDefine.XState_Death)
        {
            _state = hit ? XStateDefine.XState_BeHit : XStateDefine.XState_Idle;
            if (IsPlayer) _freeze = hit;
        }
    }

    public void SetRelation(EntityType type)
    {
        _eEntity_Type |= type;
    }

    private float _timer = 0f;
    public bool SetTimer(float delay)
    {
        if (_timer > 0.01f)
        {
            if (Time.time - _timer > delay)
            {
                _timer = 0f;
                return true;
            }
        }
        else
        {
            _timer = Time.time;
        }
        return false;
    }

    public T AttachComponent<T>() where T : XComponent, new()
    {
        uint uid = XCommon.singleton.XHash(typeof(T).Name);
        if (components.ContainsKey(uid))
        {
            return components[uid] as T;
        }
        else
        {
            T com = new T();
            com.OnInitial(this);
            components.Add(uid, com);
            return com;
        }
    }

    public bool DetachComponent<T>() where T : XComponent, new()
    {
        return DetachComponent(typeof(T).Name);
    }

    public T GetComponent<T>() where T : XComponent
    {
        uint uid = XCommon.singleton.XHash(typeof(T).Name);
        if (components != null && components.ContainsKey(uid)) return components[uid] as T;
        return null;
    }

    //for native or lua interface
    public object GetComponent(string name)
    {
        uint uid = XCommon.singleton.XHash(name);
        if (components != null && components.ContainsKey(uid)) return components[uid];
        return null;
    }

    //for native or lua interface
    public bool DetachComponent(string name)
    {
        uint uid = XCommon.singleton.XHash(name);
        if (components != null && components.ContainsKey(uid))
        {
            components[uid].OnUninit();
            components[uid] = null;
            components.Remove(uid);
            return true;
        }
        return false;
    }


    public void DetachAllComponents()
    {
        if (components != null)
        {
            var e = components.GetEnumerator();
            while (e.MoveNext())
            {
                e.Current.Value.OnUninit();
            }
        }
        components.Clear();
    }

    protected void UpdateComponents(float delta)
    {
        if (components != null)
        {
            var e = components.GetEnumerator();
            while (e.MoveNext())
            {
                e.Current.Value.Update(delta);
            }
        }
    }

    protected void OverrideAnim(string key, string clip)
    {
        if (anim != null)
        {
            string path = present.AnimLocation + clip;
            anim.OverrideAnim(key, path);
        }
    }

}
