﻿using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using XTable;

namespace XEditor
{
    public class EquipPreviewEditor : EditorWindow
    {
        private CombineConfig combineConfig = null;

        private int m_profession = 1;
        private List<EquipPart> m_FashionList = null;
        private List<EquipPart> m_EquipList = null;
        private Vector2 fashionScrollPos = Vector2.zero;
        private Vector2 equipScrollPos = Vector2.zero;


        private GameObject newGo;
        private void Preview(EquipPart part)
        {
            //1.mesh collection
            List<CombineInstance> ciList = new List<CombineInstance>();
            Texture[] m_tex = new Texture[8];
            DefaultEquip.RowData data = XTableMgr.GetTable<DefaultEquip>().GetByUID(m_profession + 1);
            string name = "";
            for (int i = 0; i < part.partPath.Length; ++i)
            {
                string path = part.partPath[i];
                CombineInstance ci = new CombineInstance();
                if (string.IsNullOrEmpty(path))
                {
                    path = XEquipUtil.GetDefaultPath((EPartType)i, data);
                }
                else if (name == "")
                {
                    int index = path.LastIndexOf("_");
                    if (index >= 0)
                    {
                        name = path.Substring(0, index);
                    }
                }
                if (!string.IsNullOrEmpty(path))
                {
                    path = "Assets/Resources/Equipments/" + path;
                    Mesh mesh = AssetDatabase.LoadAssetAtPath<Mesh>(path + AssetType.Asset);
                    Texture tex = AssetDatabase.LoadAssetAtPath<Texture>(path + AssetType.TGA);
                    if (mesh != null)
                    {
                        ci.mesh = mesh;
                        m_tex[i] = tex;
                    }
                    if (ci.mesh != null) ciList.Add(ci);
                }
            }

            if (ciList.Count > 0)
            {
                if (newGo != null) GameObject.DestroyImmediate(newGo);
                string skinPrfab = "Assets/Resources/Prefabs/" + combineConfig.PrefabName[m_profession] + AssetType.Prefab;
                newGo = GameObject.Instantiate(AssetDatabase.LoadAssetAtPath<Object>(skinPrfab)) as GameObject;
                if (name != "") newGo.name = name;
                newGo.transform.localRotation = Quaternion.Euler(new Vector3(0, 180, 0));

                //2.combine
                Transform t = newGo.transform;
                SkinnedMeshRenderer newSmr = t.GetComponent<SkinnedMeshRenderer>();
                newSmr.sharedMesh = new Mesh();
                newSmr.sharedMesh.CombineMeshes(ciList.ToArray(), true, false);

                //3.set material
                Material mat = new Material(Shader.Find("Custom/Skin/RimlightBlend8"));
                for (int i = 0; i < m_tex.Length; ++i)
                {
                    mat.SetTexture("_Tex" + i.ToString(), m_tex[i]);
                }
                newSmr.sharedMaterial = mat;

                if (data.WeaponPoint != null && data.WeaponPoint.Length > 0)
                {
                    string weapon = data.WeaponPoint[0].ToString();
                    Transform trans = newGo.transform.Find(weapon);
                    if (trans != null)
                    {
                        string path = part.mainWeapon;
                        if (string.IsNullOrEmpty(path))
                        {
                            path = data.Weapon;
                        }

                        GameObject mainWeapon = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Resources/Equipments/" + path + AssetType.Prefab);
                        if (mainWeapon != null)
                        {
                            GameObject instance = GameObject.Instantiate(mainWeapon) as GameObject;
                            instance.transform.parent = trans;
                            instance.transform.localPosition = Vector3.zero;
                            instance.transform.localRotation = Quaternion.identity;
                            instance.transform.localScale = Vector3.one;
                        }
                    }
                }
            }
        }

        public void Init()
        {
            combineConfig = FbxEditor.GetConfig();
            TempEquipSuit fashions = new TempEquipSuit();
            m_FashionList = new List<EquipPart>();
            m_EquipList = new List<EquipPart>();
            var fashionsuit = XTableMgr.GetTable<FashionSuit>();
            for (int i = 0, max = fashionsuit.length; i < max; ++i)
            {
                FashionSuit.RowData row = fashionsuit[i];
                if (row.FashionID != null)
                {
                    XEquipUtil.MakeEquip(row.SuitName, row.FashionID, m_FashionList, fashions, (int)row.SuitID);
                }
            }
            var equipsuit = XTableMgr.GetTable<EquipSuit>();
            for (int i = 0; i < equipsuit.length; ++i)
            {
                EquipSuit.RowData row = equipsuit[i];
                if (row.EquipID != null)
                    XEquipUtil.MakeEquip(row.SuitName, row.EquipID, m_EquipList, fashions, -1);
            }
        }

        protected void OnDestroy()
        {
            OnLostFocus();
        }

        protected void OnFocus()
        {
            OnLostFocus();
            Init();
        }

        protected void OnLostFocus()
        {
            if (newGo != null) GameObject.DestroyImmediate(newGo);
            m_FashionList = null;
            m_EquipList = null;
        }

        protected virtual void OnGUI()
        {
            m_profession = 1;//Archer
            if (m_FashionList == null || m_EquipList == null) return;
            //时装
            GUILayout.BeginHorizontal();
            GUILayout.BeginVertical();
            GUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("时装", GUILayout.MaxWidth(100));
            GUILayout.EndHorizontal();
            fashionScrollPos = GUILayout.BeginScrollView(fashionScrollPos, false, false);

            for (int i = 0; i < m_FashionList.Count; ++i)
            {
                EquipPart part = m_FashionList[i];
                for (int j = 0; j < part.suitName.Count; ++j)
                {
                    GUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField(part.suitName[j], GUILayout.MaxWidth(150));
                    if (j == 0)
                    {
                        if (GUILayout.Button("Preview", GUILayout.MaxWidth(100))) Preview(part);
                    }
                    GUILayout.EndHorizontal();
                }
                GUILayout.Space(5);
            }
            EditorGUILayout.EndScrollView();
            GUILayout.EndVertical();

            //装备
            GUILayout.BeginVertical();
            GUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("装备", GUILayout.MaxWidth(100));
            GUILayout.EndHorizontal();
            equipScrollPos = GUILayout.BeginScrollView(equipScrollPos, false, false);
            List<EquipPart> currentEquipPrefession = m_EquipList;
            for (int i = 0; i < currentEquipPrefession.Count; ++i)
            {
                EquipPart part = currentEquipPrefession[i];
                for (int j = 0; j < part.suitName.Count; ++j)
                {
                    GUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField(part.suitName[j], GUILayout.MaxWidth(200));
                    if (j == 0)
                    {
                        if (GUILayout.Button("Preview", GUILayout.MaxWidth(100))) Preview(part);
                    }
                    GUILayout.EndHorizontal();
                }
                GUILayout.Space(5);
            }
            EditorGUILayout.EndScrollView();
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
        }

    }
}