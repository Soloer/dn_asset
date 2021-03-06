//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:2.0.50727.8825
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace XTable {
    
    
    public class SceneList : CSVReader {
        
        public class RowData :BaseRow {
			public int SceneID;
			public string Comment;
			public int SceneType;
			public int IsStaticScene;
			public int PreTask;
			public int[] PreScene;
			public int RequiredLevel;
			public int SyncMode;
			public bool SwitchToSelf;
			public bool CanReconnect;
			public int EndTimeOut;
			public bool IsCanQuit;
			public float DelayTransfer;
			public int LineRoleCount;
			public string Buff;
			public string SceneFile;
			public string UnitySceneFile;
			public string ScenePath;
			public string BGM;
			public string BlockFilePath;
			public int[] OperationSettings;
			public string StartPos;
			public int[] StartFace;
			public float[] StartRot;
			public string Chapter;
			public int[] UIPos;
			public string BoxUIPos;
			public string UIIcon;
			public string FatigueCost;
			public int[] ViewableDropList;
			public int Exp;
			public int[] Drop;
			public int[] Drop1;
			public int[] Drop2;
			public int[] Drop3;
			public int[] Drop4;
			public int[] Drop5;
			public int Money;
			public int FirstDownExp;
			public int[] FirstDownDrop;
			public int FirstDownMoney;
			public string FirstSSS;
			public string ExpSealReward;
			public string SceneChest;
			public string DiamondDropID;
			public string GoldDropID;
			public string SilverDropID;
			public string CopperDropID;
			public string SBox;
			public string SSBox;
			public string SSSBox;
			public bool IsBoss;
			public int RecommendPower;
			public string BossAvatar;
			public string BossIcon;
			public string EndCutScene;
			public string EndCutSceneTime;
			public string WinCondition;
			public string LoseCondition;
			public string WinDelayTime;
			public int DayLimit;
			public int CoolDown;
			public string DayLimitGroupID;
			public bool CanDrawBox;
			public bool HasFlyOut;
			public string DynamicScene;
			public string EnvSet;
			public bool CanRevive;
			public string ReviveNumb;
			public string ReviveCost;
			public string ReviveMoneyCost;
			public string ReviveTime;
			public string ReviveBuff;
			public string ReviveBuffTip;
			public bool CanPause;
			public bool ShowUp;
			public string LoadingTips;
			public string LoadingPic;
			public bool SceneCanNavi;
			public bool ShowAutoFight;
			public bool ShowBattleStatistics;
			public string RandomTaskType;
			public string RandomTaskSpecify;
			public int UseSupplement;
			public int HurtCoef;
			public string MiniMap;
			public int[] MiniMapSize;
			public string MiniMapOutSize;
			public int MiniMapRotation;
			public string StaticMiniMapCenter;
			public string SceneAI;
			public string PPTCoff;
			public string GuildExpBounus;
			public string FailText;
			public string LeaveSceneTip;
			public string RecommendHint;
			public string TeamInfoDefaultTab;
			public int CombatType;
			public int SweepNeedPPT;
			public int[] TimeCounter;
			public bool HasComboBuff;
			public bool DisplayPet;
			public string AutoReturn;
			public string StoryDriver;
			public int MinPassTime;
			public bool ShowSkill;
			public bool ShowNormalAttack;
			public string WinConditionTips;
			public string BattleExplainTips;
			public string DPS;
			public string CanVIPRevive;
			public bool HideTeamIndicate;
			public string ShieldSight;
			public int SpecifiedTargetLocatedRange;
			public string SpactivityActiveDrop;
			public string SpActivityDrop;
			public string VipReviveLimit;
		}


		private RowData[] Table;

		public override int length { get { return Table.Length; } }

		public RowData this[int index] { get { return Table[index]; } }

		public override string bytePath { get { return "Table/SceneList"; } }
        
        // 二分法查找
        public virtual RowData GetByUID(int id) {
			return BinarySearch(Table, 0, Table.Length - 1, id) as RowData;
        }
        
        public override void OnClear(int lineCount) {
			if (lineCount > 0) Table = new RowData[lineCount];
			else Table = null;
        }
        
        public override void ReadLine(System.IO.BinaryReader reader) {
			RowData row = new RowData();
			Read<int>(reader, ref row.SceneID, intParse); columnno = 0;
			Read<string>(reader, ref row.Comment, stringParse); columnno = 1;
			Read<int>(reader, ref row.SceneType, intParse); columnno = 2;
			Read<int>(reader, ref row.IsStaticScene, intParse); columnno = 3;
			Read<int>(reader, ref row.PreTask, intParse); columnno = 4;
			ReadArray<int>(reader, ref row.PreScene, intParse); columnno = 5;
			Read<int>(reader, ref row.RequiredLevel, intParse); columnno = 6;
			Read<int>(reader, ref row.SyncMode, intParse); columnno = 7;
			Read<bool>(reader, ref row.SwitchToSelf, boolParse); columnno = 8;
			Read<bool>(reader, ref row.CanReconnect, boolParse); columnno = 9;
			Read<int>(reader, ref row.EndTimeOut, intParse); columnno = 10;
			Read<bool>(reader, ref row.IsCanQuit, boolParse); columnno = 11;
			Read<float>(reader, ref row.DelayTransfer, floatParse); columnno = 12;
			Read<int>(reader, ref row.LineRoleCount, intParse); columnno = 13;
			Read<string>(reader, ref row.Buff, stringParse); columnno = 14;
			Read<string>(reader, ref row.SceneFile, stringParse); columnno = 15;
			Read<string>(reader, ref row.UnitySceneFile, stringParse); columnno = 16;
			Read<string>(reader, ref row.ScenePath, stringParse); columnno = 17;
			Read<string>(reader, ref row.BGM, stringParse); columnno = 18;
			Read<string>(reader, ref row.BlockFilePath, stringParse); columnno = 19;
			ReadArray<int>(reader, ref row.OperationSettings, intParse); columnno = 20;
			Read<string>(reader, ref row.StartPos, stringParse); columnno = 21;
			ReadArray<int>(reader, ref row.StartFace, intParse); columnno = 22;
			ReadArray<float>(reader, ref row.StartRot, floatParse); columnno = 23;
			Read<string>(reader, ref row.Chapter, stringParse); columnno = 24;
			ReadArray<int>(reader, ref row.UIPos, intParse); columnno = 25;
			Read<string>(reader, ref row.BoxUIPos, stringParse); columnno = 26;
			Read<string>(reader, ref row.UIIcon, stringParse); columnno = 27;
			Read<string>(reader, ref row.FatigueCost, stringParse); columnno = 28;
			ReadArray<int>(reader, ref row.ViewableDropList, intParse); columnno = 29;
			Read<int>(reader, ref row.Exp, intParse); columnno = 30;
			ReadArray<int>(reader, ref row.Drop, intParse); columnno = 31;
			ReadArray<int>(reader, ref row.Drop1, intParse); columnno = 32;
			ReadArray<int>(reader, ref row.Drop2, intParse); columnno = 33;
			ReadArray<int>(reader, ref row.Drop3, intParse); columnno = 34;
			ReadArray<int>(reader, ref row.Drop4, intParse); columnno = 35;
			ReadArray<int>(reader, ref row.Drop5, intParse); columnno = 36;
			Read<int>(reader, ref row.Money, intParse); columnno = 37;
			Read<int>(reader, ref row.FirstDownExp, intParse); columnno = 38;
			ReadArray<int>(reader, ref row.FirstDownDrop, intParse); columnno = 39;
			Read<int>(reader, ref row.FirstDownMoney, intParse); columnno = 40;
			Read<string>(reader, ref row.FirstSSS, stringParse); columnno = 41;
			Read<string>(reader, ref row.ExpSealReward, stringParse); columnno = 42;
			Read<string>(reader, ref row.SceneChest, stringParse); columnno = 43;
			Read<string>(reader, ref row.DiamondDropID, stringParse); columnno = 44;
			Read<string>(reader, ref row.GoldDropID, stringParse); columnno = 45;
			Read<string>(reader, ref row.SilverDropID, stringParse); columnno = 46;
			Read<string>(reader, ref row.CopperDropID, stringParse); columnno = 47;
			Read<string>(reader, ref row.SBox, stringParse); columnno = 48;
			Read<string>(reader, ref row.SSBox, stringParse); columnno = 49;
			Read<string>(reader, ref row.SSSBox, stringParse); columnno = 50;
			Read<bool>(reader, ref row.IsBoss, boolParse); columnno = 51;
			Read<int>(reader, ref row.RecommendPower, intParse); columnno = 52;
			Read<string>(reader, ref row.BossAvatar, stringParse); columnno = 53;
			Read<string>(reader, ref row.BossIcon, stringParse); columnno = 54;
			Read<string>(reader, ref row.EndCutScene, stringParse); columnno = 55;
			Read<string>(reader, ref row.EndCutSceneTime, stringParse); columnno = 56;
			Read<string>(reader, ref row.WinCondition, stringParse); columnno = 57;
			Read<string>(reader, ref row.LoseCondition, stringParse); columnno = 58;
			Read<string>(reader, ref row.WinDelayTime, stringParse); columnno = 59;
			Read<int>(reader, ref row.DayLimit, intParse); columnno = 60;
			Read<int>(reader, ref row.CoolDown, intParse); columnno = 61;
			Read<string>(reader, ref row.DayLimitGroupID, stringParse); columnno = 62;
			Read<bool>(reader, ref row.CanDrawBox, boolParse); columnno = 63;
			Read<bool>(reader, ref row.HasFlyOut, boolParse); columnno = 64;
			Read<string>(reader, ref row.DynamicScene, stringParse); columnno = 65;
			Read<string>(reader, ref row.EnvSet, stringParse); columnno = 66;
			Read<bool>(reader, ref row.CanRevive, boolParse); columnno = 67;
			Read<string>(reader, ref row.ReviveNumb, stringParse); columnno = 68;
			Read<string>(reader, ref row.ReviveCost, stringParse); columnno = 69;
			Read<string>(reader, ref row.ReviveMoneyCost, stringParse); columnno = 70;
			Read<string>(reader, ref row.ReviveTime, stringParse); columnno = 71;
			Read<string>(reader, ref row.ReviveBuff, stringParse); columnno = 72;
			Read<string>(reader, ref row.ReviveBuffTip, stringParse); columnno = 73;
			Read<bool>(reader, ref row.CanPause, boolParse); columnno = 74;
			Read<bool>(reader, ref row.ShowUp, boolParse); columnno = 75;
			Read<string>(reader, ref row.LoadingTips, stringParse); columnno = 76;
			Read<string>(reader, ref row.LoadingPic, stringParse); columnno = 77;
			Read<bool>(reader, ref row.SceneCanNavi, boolParse); columnno = 78;
			Read<bool>(reader, ref row.ShowAutoFight, boolParse); columnno = 79;
			Read<bool>(reader, ref row.ShowBattleStatistics, boolParse); columnno = 80;
			Read<string>(reader, ref row.RandomTaskType, stringParse); columnno = 81;
			Read<string>(reader, ref row.RandomTaskSpecify, stringParse); columnno = 82;
			Read<int>(reader, ref row.UseSupplement, intParse); columnno = 83;
			Read<int>(reader, ref row.HurtCoef, intParse); columnno = 84;
			Read<string>(reader, ref row.MiniMap, stringParse); columnno = 85;
			ReadArray<int>(reader, ref row.MiniMapSize, intParse); columnno = 86;
			Read<string>(reader, ref row.MiniMapOutSize, stringParse); columnno = 87;
			Read<int>(reader, ref row.MiniMapRotation, intParse); columnno = 88;
			Read<string>(reader, ref row.StaticMiniMapCenter, stringParse); columnno = 89;
			Read<string>(reader, ref row.SceneAI, stringParse); columnno = 90;
			Read<string>(reader, ref row.PPTCoff, stringParse); columnno = 91;
			Read<string>(reader, ref row.GuildExpBounus, stringParse); columnno = 92;
			Read<string>(reader, ref row.FailText, stringParse); columnno = 93;
			Read<string>(reader, ref row.LeaveSceneTip, stringParse); columnno = 94;
			Read<string>(reader, ref row.RecommendHint, stringParse); columnno = 95;
			Read<string>(reader, ref row.TeamInfoDefaultTab, stringParse); columnno = 96;
			Read<int>(reader, ref row.CombatType, intParse); columnno = 97;
			Read<int>(reader, ref row.SweepNeedPPT, intParse); columnno = 98;
			ReadArray<int>(reader, ref row.TimeCounter, intParse); columnno = 99;
			Read<bool>(reader, ref row.HasComboBuff, boolParse); columnno = 100;
			Read<bool>(reader, ref row.DisplayPet, boolParse); columnno = 101;
			Read<string>(reader, ref row.AutoReturn, stringParse); columnno = 102;
			Read<string>(reader, ref row.StoryDriver, stringParse); columnno = 103;
			Read<int>(reader, ref row.MinPassTime, intParse); columnno = 104;
			Read<bool>(reader, ref row.ShowSkill, boolParse); columnno = 105;
			Read<bool>(reader, ref row.ShowNormalAttack, boolParse); columnno = 106;
			Read<string>(reader, ref row.WinConditionTips, stringParse); columnno = 107;
			Read<string>(reader, ref row.BattleExplainTips, stringParse); columnno = 108;
			Read<string>(reader, ref row.DPS, stringParse); columnno = 109;
			Read<string>(reader, ref row.CanVIPRevive, stringParse); columnno = 110;
			Read<bool>(reader, ref row.HideTeamIndicate, boolParse); columnno = 111;
			Read<string>(reader, ref row.ShieldSight, stringParse); columnno = 112;
			Read<int>(reader, ref row.SpecifiedTargetLocatedRange, intParse); columnno = 113;
			Read<string>(reader, ref row.SpactivityActiveDrop, stringParse); columnno = 114;
			Read<string>(reader, ref row.SpActivityDrop, stringParse); columnno = 115;
			Read<string>(reader, ref row.VipReviveLimit, stringParse); columnno = 116;
			row.sortID = (int)row.SceneID;
			Table[lineno] = row;
			columnno = -1;
        }
    }
}
