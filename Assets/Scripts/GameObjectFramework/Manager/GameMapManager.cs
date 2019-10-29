using UnityEngine;
using System.Collections.Generic;
using System;
using System.IO;
using GameEnum;
using Interface;
using GameData;
using UnityEngine.SceneManagement;
using GameManager.UI;

namespace GameManager {
    /// <summary>
    /// 游戏主逻辑，游戏核心功能都从这里调用
    /// 单例类型
    /// </summary>
    public sealed class GameMapManager : Singleton<GameMapManager>
    {
        public GameDifficulty gameDifficulty;
        public CampEnum PlayerCamp { get; set; }   //玩家阵营
        Dictionary<string, UnitData> UnitsDataTable = new Dictionary<string, UnitData>(); //保存序列化读取的游戏对象基础属性
        Dictionary<string, Attack_Data> Attack_DataTable = new Dictionary<string, Attack_Data>();
        public CoroutinesManager coroutinesManager;
        public bool IsCanBuilding { get; set; }  //玩家当前是否可以建造
        public GameObject AllianceCenter { get; private set; }
        public GameObject EmpireCenter { get; private set; }
        public GameObject AICenter { get; private set; }
        public GameObject PlayerCenter { get; private set; }

        #region 初始化
        public override void Init()
        {
            base.Init();
            OnAfterDeserialize();
            OnAfterDeserialize_1();
            
        }


        public void Init_MainScene()
        {
            AllianceCenter = GameObject.Find("AllianceCenter");
            EmpireCenter = GameObject.Find("EmpireCenter");
            if (PlayerCamp == CampEnum.Alliance)
            {
                AICenter = EmpireCenter;
                PlayerCenter = AllianceCenter;
            }
            else
            {
                AICenter = AllianceCenter;
                PlayerCenter = EmpireCenter;
            }
            coroutinesManager = GameObject.Find("Center").GetComponent<CoroutinesManager>();
        }

        #endregion

        #region  序列化读取游戏单位数据
        [Serializable]
        public struct UnitData
        {
            public string object_UnitName;
            public string object_Camp;
            public string object_UnitType;
            public int object_HP;
            public float unit_Radius;
        }
        [Serializable]
        struct GameUnitsDataList
        {
            public List<UnitData> GameUnitsData;
        }

        public void OnAfterDeserialize()
        {
            string jsonString = File.ReadAllText(Application.dataPath + "/Scripts/GameObjectFramework/ObjectJsonData/GameUnitsDataJson.json");
            GameUnitsDataList dataList = JsonUtility.FromJson<GameUnitsDataList>(jsonString);
            for (int i = 0; i < dataList.GameUnitsData.Count; ++i)
            {
                UnitsDataTable.Add(dataList.GameUnitsData[i].object_UnitName, dataList.GameUnitsData[i]);
            }
        }

        public UnitData GetUnitData(string unit_name)
        {
            if (UnitsDataTable.ContainsKey(unit_name))
                return UnitsDataTable[unit_name];
            Debug.LogWarning("BasicData has no this object's Basicdata");
            return new UnitData();
        }
        #endregion

        #region  序列化读取作战单位数据
        [Serializable]
        public struct Attack_Data
        {
            public string unit_Name;
            public string shell_Type;
            public float shell_Damage;
            public float attack_Speed;
            public string attack_Priority;
            public int maxDetectable_Range;
        }
        [Serializable]
        struct Attack_DataList
        {
            public List<Attack_Data> BattleUnitsConfig;
        }

        public void OnAfterDeserialize_1()
        {
            string jsonString = File.ReadAllText(Application.dataPath + "/Scripts/GameObjectFramework/ObjectJsonData/BattleUnitsConfigJson.json");
            Attack_DataList dataList = JsonUtility.FromJson<Attack_DataList>(jsonString);
            for (int i = 0; i < dataList.BattleUnitsConfig.Count; ++i)
            {
                Attack_DataTable.Add(dataList.BattleUnitsConfig[i].unit_Name, dataList.BattleUnitsConfig[i]);
            }
        }
        public Attack_Data GetAttackData(string unit_name)
        {
            if (Attack_DataTable.ContainsKey(unit_name))
                return Attack_DataTable[unit_name];
            Debug.LogWarning("Attack_DataTable has no this object's Basicdata");
            return new Attack_Data();
        }
        #endregion

        public void GameOver(bool IsPlayerCamp)
        {
            Time.timeScale = 0;
            UIManager.Instance.LoadUIPanel(UIPanelEnum.GameEndUI, IsPlayerCamp);
            
        }
        
    }
}
