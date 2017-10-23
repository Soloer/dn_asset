﻿using System.Linq;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using Level;

namespace XEditor
{

    class LevelLayout
    {
        public SerializeLevel _levelMgr;

        private static GUIContent AddWaveButtonContent = new GUIContent("add wave", "add wave");
        private static GUIContent AddScriptButtonContent = new GUIContent("add script", "add script");
        private static GUIContent EditLevelScriptButtonContent = new GUIContent("edit script", "edit script");
        private static GUIContent GenerateWallInfoButtonContent = new GUIContent("save wall info", "save wall info");
        private static GUIContent LoadWallInfoButtonContent = new GUIContent("load wall info", "load wall info");
        private static GUIContent SaveWaveButtonContent = new GUIContent("Save", "Save to file");
        private static GUIContent LoadWaveButtonContent = new GUIContent("Load", "Load from file");
        private static GUIContent ClearButtonContent = new GUIContent("Clear", "Clear");

        public static int tabLength = 420;
        public static int minViewHeight = 90;
        public static int maxViewHeight = 920;
        public static int minViewWidth = 5;
        public static int maxViewWidth = 1900;
        public Vector2 scrollPosition = Vector2.zero;

        public static GUILayoutOption miniButtonWidth = GUILayout.Width(20f);
        public static GUILayoutOption detailLayout = GUILayout.Width(tabLength);

        protected Texture2D _grayText = null;

        public LevelLayout(SerializeLevel mgr)
        {
            _levelMgr = mgr;
            InitGrayTexture();
        }

        public void OnGUI()
        {
            GUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("current level : " + _levelMgr.current_level);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            if (GUILayout.Button(AddWaveButtonContent, GUILayout.Width(100f)))
            {
                _levelMgr.AddWave();
            }
            if (GUILayout.Button(AddScriptButtonContent, GUILayout.Width(100f)))
            {
                _levelMgr.AddScript();
            }
            if (GUILayout.Button(ClearButtonContent, GUILayout.Width(100f)))
            {
                _levelMgr.ClearWaves();
            }
            if (GUILayout.Button(EditLevelScriptButtonContent, GUILayout.Width(100f)))
            {
                _levelMgr.OpenLevelScriptFile();
            }
            if (GUILayout.Button(GenerateWallInfoButtonContent, GUILayout.Width(150f)))
            {
                _levelMgr.GenerateWallInfo();
            }
            if (GUILayout.Button(LoadWallInfoButtonContent, GUILayout.Width(150f)))
            {
                _levelMgr.LoadWallInfo();
            }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("TotalWave : " + _levelMgr.WaveCount);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            if (GUILayout.Button(SaveWaveButtonContent))
            {
                _levelMgr.SaveToFile();
            }
            if (GUILayout.Button(LoadWaveButtonContent))
            {
                _levelMgr.LoadFromFile();
            }
            GUILayout.EndHorizontal();

            GUILayout.Box("", new GUILayoutOption[] { GUILayout.ExpandWidth(true), GUILayout.Height(1) });
            GUILayout.BeginHorizontal();
            DrawTab();
            scrollPosition = GUI.BeginScrollView(new Rect(tabLength + minViewWidth, minViewHeight, maxViewWidth, maxViewHeight), scrollPosition, new Rect(0, 0, 3000, 3000));
            _levelMgr.Editor.BeginWindows();
            foreach (EditorWave _wave in _levelMgr._waves) _wave.DrawWaveWindow();
            _levelMgr.Editor.EndWindows();
            GUI.EndScrollView();
            GUILayout.EndHorizontal();

            DrawLinks();
        }

        public void InitGrayTexture()
        {
            _grayText = new Texture2D(128, 128, TextureFormat.ARGB32, true);
            _grayText.SetPixel(0, 1, Color.gray);
            _grayText.Apply();
        }

        private void DrawPreloadView()
        {
            Dictionary<uint, int> preloadTmp = new Dictionary<uint, int>();
            CalEnemyNum cen = new CalEnemyNum();
            Dictionary<uint, int> dic = cen.CalNum(_levelMgr._waves);
            foreach (var item in dic)
            {
                int cur = item.Value;
                int last = 0;
                _levelMgr._lastCachePreloadInfo.TryGetValue(item.Key, out last);
                if (last != cur)
                {
                    _levelMgr._preLoadInfo[item.Key] = cur;
                }
            }
            foreach (var item in _levelMgr._lastCachePreloadInfo)
            {
                int last = item.Value;
                int cur = 0;
                dic.TryGetValue(item.Key, out cur);
                if (last != cur)
                {
                    _levelMgr._preLoadInfo[item.Key] = cur;
                }
            }

            GUIStyle style = new GUIStyle();
            EditorGUILayout.LabelField("Preload Monster : ");
            foreach (var item in _levelMgr._preLoadInfo)
            {
                int preload = item.Value;
                int suggest = 0;
                dic.TryGetValue(item.Key, out suggest);
                if (preload > suggest || suggest == 0)
                {
                    style.normal.textColor = Color.red;
                }
                else if (preload < suggest)
                {
                    style.normal.textColor = Color.green;
                }
                else
                {
                    style.normal.textColor = Color.white;
                }

                GUILayout.BeginHorizontal();

                GUILayout.BeginVertical();
                EditorGUILayout.LabelField(string.Format("MonsterID: {0}   (推荐数量: {1})", item.Key, suggest), style);
                GUILayout.EndVertical();

                GUILayout.BeginVertical();
                preloadTmp[item.Key] = EditorGUILayout.IntField(preload);
                GUILayout.EndVertical();

                GUILayout.EndHorizontal();
                GUILayout.Box("", new GUILayoutOption[] { GUILayout.ExpandWidth(true), GUILayout.Height(1) });
            }

            _levelMgr._preLoadInfo.Clear();
            foreach (var item in preloadTmp)
            {
                int suggest = 0;
                dic.TryGetValue(item.Key, out suggest);
                if (item.Value < 0)
                {
                    if (suggest != 0)
                    {
                        _levelMgr._preLoadInfo[item.Key] = 0;
                    }
                    continue;
                }
                _levelMgr._preLoadInfo[item.Key] = item.Value;
            }

            _levelMgr._lastCachePreloadInfo = dic;
        }

        public void DrawTab()
        {
            GUILayout.BeginHorizontal(detailLayout);
            GUILayout.BeginVertical();
            if (_levelMgr.CurrentEdit > -1)
            {
                GUILayout.Space(10);
                DrawDetailView();
            }
            GUILayout.Space(24);
            DrawPreloadView();
            GUILayout.EndHorizontal();
            GUILayout.EndHorizontal();
        }

        public void DrawDetailView()
        {
            EditorWave wv = _levelMgr.GetWave(_levelMgr.CurrentEdit);
            if (wv != null)
            {
                GUILayout.BeginHorizontal();
                wv.SpawnType = (LevelSpawnType)EditorGUILayout.EnumPopup("Spawn Type", wv.SpawnType);
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                wv.Time = EditorGUILayout.FloatField("Spawn Time(s):", wv.Time);
                GUILayout.EndHorizontal();

                GUILayout.Space(20);

                GUILayout.BeginHorizontal();
                wv._exString = EditorGUILayout.TextField("ExString(,):", wv._exString);
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                wv._preWaves = EditorGUILayout.TextField("PreWaves(,):", wv._preWaves);
                wv._preWavePercent = EditorGUILayout.IntSlider("PreWavePercent(%):", wv._preWavePercent, 0, 100);
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                wv._buff_id = EditorGUILayout.TextField("Buff ID:", wv._buff_id);
                wv._buff_percent = EditorGUILayout.IntSlider("Buff Percent(%):", wv._buff_percent, 0, 100);
                GUILayout.EndHorizontal();

                GUILayout.Space(20);

                GUILayout.BeginHorizontal();
                wv.RoundRidous = EditorGUILayout.FloatField("RoundRidous:", wv.RoundRidous);
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                wv.RoundCount = EditorGUILayout.IntSlider("RoundCount:", wv.RoundCount, 0, 10);
                GUILayout.EndHorizontal();
            }
        }

        public void DrawLinks()
        {
            if (_levelMgr.CurrentEdit > -1)
            {
                EditorWave _wave = _levelMgr.GetWave(_levelMgr.CurrentEdit);
                if (_wave == null) return;
                if (_wave._preWaves != null && _wave._preWaves.Length > 0)
                {
                    string[] strPreWaves = _wave._preWaves.Split(',');
                    for (int i = 0; i < strPreWaves.Length; i++)
                    {
                        int preWaveId;
                        bool b = int.TryParse(strPreWaves[i], out preWaveId);
                        if (b) curveFromTo(preWaveId, _wave._id, Color.green, Color.yellow);
                    }
                }
                foreach (EditorWave wv in _levelMgr._waves)
                {
                    if (wv._preWaves != null && wv._preWaves.Length > 0)
                    {
                        string[] strPreWaves = wv._preWaves.Split(',');
                        if (strPreWaves.Contains(_levelMgr.CurrentEdit.ToString()))
                            curveFromTo(_levelMgr.CurrentEdit, wv._id, Color.green, Color.yellow);
                    }
                }
            }
        }

        protected void curveFromTo(int parentID, int childID, Color color, Color color2)
        {
            EditorWave parent = _levelMgr.GetWave(parentID);
            EditorWave child = _levelMgr.GetWave(childID);
            if (parent != null && child != null)
            {
                Rect parentRect = parent.LayoutWindow._rect;
                Rect childRect = child.LayoutWindow._rect;
                parentRect.x += tabLength;
                childRect.x += tabLength;
                curveFromTo(parentRect, childRect, color, color2);
            }
        }

        protected void curveFromTo(Rect from, Rect to, Color color, Color color2)
        {
            Vector2 offset = new Vector2(minViewWidth, minViewHeight);
            if (from.y + from.height <= to.y)
            {// up
                Drawing.bezierLine(
                new Vector2(from.x + from.width / 2, from.y + from.height) + offset - scrollPosition,
                new Vector2(from.x + from.width / 2, from.y + from.height + Mathf.Abs(to.y - (from.y + from.height))) + offset - scrollPosition,
                new Vector2(to.x + to.width / 2, to.y) + offset - scrollPosition,
                new Vector2(to.x + to.width / 2, to.y - Mathf.Abs(to.y - (from.y + from.height)) / 2) + offset - scrollPosition,
                color, color2, 2, true, 30);
            }
            else if (to.y + to.height <= from.y)
            {// down
                Drawing.bezierLine(
                    new Vector2(from.x + from.width / 2, from.y) + offset - scrollPosition,
                    new Vector2(from.x + from.width / 2, from.y - Mathf.Abs(from.y - (to.y + to.height)) / 2) + offset - scrollPosition,

                    new Vector2(to.x + to.width / 2, to.y + to.height) + offset - scrollPosition,
                    new Vector2(to.x + to.width / 2, to.y + to.height + Mathf.Abs(to.y - (to.y + to.height))) + offset - scrollPosition,

                    color, color2, 2, true, 30);
            }
            else if (from.x + from.width <= to.x)
            {// left
                Drawing.bezierLine(
                    new Vector2(from.x + from.width, from.y + from.height / 2) + offset - scrollPosition,
                    new Vector2(from.x + from.width + Mathf.Abs(to.x - (from.x + from.width)), from.y + from.height / 2) + offset - scrollPosition,

                    new Vector2(to.x, to.y + to.height / 2) + offset - scrollPosition,
                    new Vector2(to.x - Mathf.Abs(to.x - (from.x + from.width)), to.y + to.height / 2) + offset - scrollPosition,

                    color, color2, 2, true, 30);
            }
            else if (to.x + to.width <= from.x)
            {// right
                Drawing.bezierLine(
                    new Vector2(from.x, from.y + from.height / 2) + offset - scrollPosition,
                    new Vector2(from.x - Mathf.Abs(from.x - (to.x + to.width)), from.y + from.height / 2) + offset - scrollPosition,

                    new Vector2(to.x + to.width, to.y + to.height / 2) + offset - scrollPosition,
                    new Vector2(to.x + to.width + Mathf.Abs(from.x - (to.x + to.width)), to.y + to.height / 2) + offset - scrollPosition,

                    color, color2, 2, true, 30);
            }
            else
            {// overlap
                Drawing.bezierLine(
                    new Vector2(from.x, from.y) + offset - scrollPosition,
                    new Vector2(from.x, from.y) + offset - scrollPosition,

                    new Vector2(to.x, to.y) + offset - scrollPosition,
                    new Vector2(to.x, to.y) + offset - scrollPosition,

                    color, color2, 2, true, 10);
            }
        }

    }
}