using GameEnum;
using GameEvent;
using GameUI;
using Interface;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using FairyGUI;

[Serializable]
public struct UnitDescribeData
{
    public string unit_Name;
    public string unit_Describe;
    public string unit_Cost;
}
[Serializable]
public struct UnitUIData
{
    public string unit_Icon;
    public string unit_UIName;
    public string unit_UIInf;
    public string unit_ProdIcon;
}
namespace GameManager.UI
{
    //
    // 摘要:
    //     Base class for effects that modify the the generated Vertex Buffers.
    public class UIManager : Singleton<UIManager>
    {
        
        private StartUI startUI;
        private MapSettingUI mapSettingUI;
        public MainUI mainUI;
        private GameEndUI gameEndUI;
        private int PlayerCampLayer;
        bool IsInit = false;

        private List<Window> UIPanelList = new List<Window>();

        #region 获取对象UI信息
        Dictionary<string, UnitUIData> GameUnitsUIData = new Dictionary<string, UnitUIData>(); //保存序列化读取的游戏对象基础属性

        [Serializable]
        struct UnitUIDataList
        {
            public List<UnitUIData> GameUnitsUIData;
        }

        /// <summary>
        /// 反序列化   从文本信息到对象
        /// </summary>
        void OnAfterDeserialize()
        {
            string jsonString = File.ReadAllText(Application.dataPath + "/Scripts/GameObjectFramework/ObjectJsonData/GameUnitsUIDataJson.txt");
            UnitUIDataList dataList = JsonUtility.FromJson<UnitUIDataList>(jsonString);
            for (int i = 0; i < dataList.GameUnitsUIData.Count; ++i)
            {
                GameUnitsUIData.Add(dataList.GameUnitsUIData[i].unit_Icon, dataList.GameUnitsUIData[i]);
            }
        }
        #endregion

        #region 获取对象UI描述信息
        public Dictionary<string, UnitDescribeData> game_UnitDescribe = new Dictionary<string, UnitDescribeData>(); //保存序列化读取的游戏对象基础属性

        [Serializable]
        struct UnitDescribeDataList
        {
            public List<UnitDescribeData> GameUnitDescribe;
        }

        /// <summary>
        /// 反序列化   从文本信息到对象
        /// </summary>
        void OnAfterDeserializeOne()
        {
            string jsonString = File.ReadAllText(Application.dataPath + "/Scripts/GameObjectFramework/ObjectJsonData/GameUnitDescribe.txt");
            UnitDescribeDataList dataList = JsonUtility.FromJson<UnitDescribeDataList>(jsonString);
            for (int i = 0; i < dataList.GameUnitDescribe.Count; ++i)
            {
                game_UnitDescribe.Add(dataList.GameUnitDescribe[i].unit_Name, dataList.GameUnitDescribe[i]);
            }
        }
        #endregion
        public override void Init()
        {
            if (IsInit)
                return;
            base.Init();
            OnAfterDeserialize();
            OnAfterDeserializeOne();
            startUI = new StartUI();
            mapSettingUI = new MapSettingUI();
            mainUI = new MainUI();
            gameEndUI = new GameEndUI();

            UIPanelList.Add(startUI);
            UIPanelList.Add(mapSettingUI);
            UIPanelList.Add(mainUI);

            if (GameMapManager.Instance.PlayerCamp == CampEnum.Alliance)
                PlayerCampLayer = 8;
            else
                PlayerCampLayer = 9;
            IsInit = true;
        }


        protected override void BindEvent()
        {
            base.BindEvent();
            if (!EventManager.Instance.Check(GameEventEnum.PlayerPressLeftMouseEvent, ClickBuilding))
                EventManager.Instance.Subscribe(GameEventEnum.PlayerPressLeftMouseEvent, ClickBuilding);

        }

        /// <summary>
        /// 获取某个单位的ui信息
        /// </summary>
        /// <param name="unit_UIId"></param>
        /// <returns></returns>
        public UnitUIData GetUnitUIData(string name)
        {
            if (GameUnitsUIData.ContainsKey(name))
                return GameUnitsUIData[name];
            return new UnitUIData();
        }

        public UnitDescribeData GetUnitDescribeData(string name)
        {
            if (game_UnitDescribe.ContainsKey(name))
                return game_UnitDescribe[name];
            return new UnitDescribeData();
        }

        /// <summary>
        /// 点击建筑时弹出ui
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickBuilding(object sender, GameEventArgs e)
        {
            MouseClickEventArgs m = e as MouseClickEventArgs;
            if (e.dataGameobject.layer != PlayerCampLayer)
                return;
            switch (e.dataGameobject.GetComponent<IGameUnitDataFun>().GetGameUnitType())
            {
                case UnitTypeEnum.CommandCenter:
                case UnitTypeEnum.Barrack:
                case UnitTypeEnum.OrdnanceFactory:
                case UnitTypeEnum.Harvester:
                case UnitTypeEnum.Airport:
                    LoadUIPanel(UIPanelEnum.MainUI, m);
                    break;
            }
        }

        /// <summary>
        /// 打开对应UI面板
        /// </summary>
        /// <param name="packageName">包名</param>
        /// <param name="uiName">ui名</param>
        public void LoadUIPanel(UIPanelEnum uiPanel, object data = null)
        {
            switch (uiPanel)
            {
                case UIPanelEnum.StartUI:
                    ShowUI(startUI);
                    break;
                case UIPanelEnum.MapSettingUI:
                    ShowUI(mapSettingUI);
                    break;
                case UIPanelEnum.MainUI:
                    mainUI.data = data;
                    ShowUI(mainUI);
                    break;
                case UIPanelEnum.GameEndUI:
                    gameEndUI.data = data;
                    ShowUI(gameEndUI, true);
                    break;

            }
        }


        void ShowUI(Window ui, bool IsAloneShow = false)
        {
            ui.Show();
            if (IsAloneShow)
                return;
            foreach (var v in UIPanelList)
            {
                if (v != ui)
                {
                    v.Hide();
                }
            }
            
        }
        
        public void CloseUI(UIPanelEnum uiPanel)
        {
            switch (uiPanel)
            {
                case UIPanelEnum.StartUI:
                    startUI.Hide();
                    break;
                case UIPanelEnum.MapSettingUI:
                    mapSettingUI.Hide();
                    break;
                case UIPanelEnum.MainUI:
                    mainUI.Hide();
                    break;
                case UIPanelEnum.GameEndUI:
                    gameEndUI.Hide();
                    break;
            }
        }

    }

}
