using UnityEngine;
using FairyGUI;
using GameManager.UI;
using GameEvent;
using System;
using GameEnum;
using GameManager;

namespace GameUI
{
    struct ComponentData
    {
        public string UnitName;
        public GameObject unit;
        public bool isBuilding;

        public ComponentData(string name, GameObject Unit, bool IsBuilding)
        {
            UnitName = name;
            unit = Unit;
            isBuilding = IsBuilding;
        }
    }

    public class MainUI : Window
    {
        private GTextField txtUnitInf;
        private GTextField txtUnitName;
        private GComponent comUnitIcon;
        private GList listProducting;
        private UnitUIData unitUIData;
        private UnitDescribeData UnitDescribeData;
        private GameObject Unit;  //当前界面正在显示的单位

        private GComponent gropCost;
        private GTextField txtUnitDescribe;
        private GTextField txtMineCost;
        private GTextField txtGoldCost;

        private GTextField txtMineCount;
        private GTextField txtGoldCount;

        delegate void SetClickEnvet(GComponent gComponent);

        MouseClickEventArgs unitUIEventArgs;

        public GSlider gSlider;
        public GameObject GropCost;

        private GLoader unit_Icon;
        private GComponent miniMap;
        private GComponent frame;
        
        public MainUI()
        {
            contentPane = UIPackage.CreateObject("UI", "mainUI").asCom;
        }
        
        protected override void OnInit()
        {
            txtUnitInf = contentPane.GetChild("txtUnitInf").asTextField;
            txtUnitName = contentPane.GetChild("txtUnitName").asTextField;
            GComponent gComponent = contentPane.GetChild("UnitIcon").asCom;
            unit_Icon = gComponent.GetChild("ProdingIcon").asLoader;
            listProducting = contentPane.GetChild("listProducting").asList;
            gropCost = contentPane.GetChild("GropCost").asCom;
            txtUnitDescribe = gropCost.GetChild("txtUnitDescribe").asTextField;
            txtMineCost = gropCost.GetChild("txtMineNum").asTextField;
            txtGoldCost = gropCost.GetChild("txtGoldNum").asTextField;

            txtMineCount = contentPane.GetChild("txtMineCount").asTextField;
            txtGoldCount = contentPane.GetChild("txtGoldCount").asTextField;

            miniMap = contentPane.GetChild("miniMap").asCom;
            frame = contentPane.GetChild("frame").asCom;
            

            txtMineCount.text = GameDataManager.Instance.playerCampData.Mine_Count.ToString();
            txtGoldCount.text = GameDataManager.Instance.playerCampData.Gold_Count.ToString();

            gropCost.visible = false;

            if (!EventManager.Instance.Check(UIEventEnum.ShowUnitIntroducedUI, ShowUnitIntroducedUI))
                EventManager.Instance.Subscribe(UIEventEnum.ShowUnitIntroducedUI, ShowUnitIntroducedUI);
            if (!EventManager.Instance.Check(UIEventEnum.CloseUnitIntroducedUI, CloseUnitIntroducedUI))
                EventManager.Instance.Subscribe(UIEventEnum.CloseUnitIntroducedUI, CloseUnitIntroducedUI);

            GameDataManager.Instance.mineNumChange += ChangeMineCountView;

            miniMap.onClick.Add(SetCameraPos);
            

        }   
        
        public void SetFramePos(float x,float y)
        {
            x += 19;
            y += 695;
            frame.SetXY(x, y);
            
        }   
            
        void SetCameraPos()
        {
            Vector3 mousePos = Input.mousePosition;
            float x = mousePos.x;
            float z = mousePos.y;
            x -= 42;
            x = -x * 1800 / 318;
            z -= 50;
            z = -z * 2200 / 310;
            EventManager.Instance.Fire(GameEventEnum.Click_MiniMap, null, new GameEventArgs(new Vector3(x, 0, z)));

        }   
            
        public override void Show()
        {
            base.Show();
            OnRrfresh();
        }


        public void OnRrfresh()
        {
            if (this.data == null)
                return;
            unitUIEventArgs = this.data as MouseClickEventArgs;
            if (Unit == unitUIEventArgs.dataGameobject || unitUIEventArgs.dataGameobject == null)
                return;
            Unit = unitUIEventArgs.dataGameobject;
            unitUIData = UIManager.Instance.GetUnitUIData(unitUIEventArgs.dataGameobject.tag);
            txtUnitName.text = unitUIData.unit_UIName;
            unit_Icon.url = "ui://UI/" + unitUIData.unit_Icon;
            txtUnitInf.text = "        " + unitUIData.unit_UIInf;
            string[] icon = unitUIData.unit_ProdIcon.Split(',');
            string[, ] name = new string[icon.Length, 2];
            for (int i = 0; i < icon.Length; ++i)
            {
                name[i,0] = icon[i].Split('|')[0];
                name[i,1] = icon[i].Split('|')[1];
            }
            listProducting.numItems = icon.Length;
            for (int i = 0; i < icon.Length; i++)
            {
                GComponent gComponent = listProducting.GetChildAt(i).asCom;
                gComponent.data = new ComponentData(name[i, 0], unitUIEventArgs.dataGameobject, Convert.ToBoolean(Convert.ToInt32(name[i, 1])));
            }
        }

        public void ChangeMineCountView(int count)
        {
            txtMineCount.text = count.ToString();
            txtGoldCount.text = count.ToString();
        }
        
        public void ShowUnitIntroducedUI(object sender,GameEventArgs e)
        {
            Unit_UI unit_UI = (Unit_UI)(e.data);
            gropCost.visible = true;
            UnitDescribeData = UIManager.Instance.GetUnitDescribeData(unit_UI.name);
            txtUnitDescribe.text = UnitDescribeData.unit_Describe;
            txtMineCost.text = unit_UI.cost.ToString();
            txtGoldCost.text = txtMineCost.text;
        }

        public void CloseUnitIntroducedUI(object sender, GameEventArgs e)
        {
            gropCost.visible = false;
        }

        public override void Dispose()
        {
            base.Dispose();
            EventManager.Instance.UnSubscribe(UIEventEnum.ShowUnitIntroducedUI, ShowUnitIntroducedUI);
            EventManager.Instance.UnSubscribe(UIEventEnum.CloseUnitIntroducedUI, CloseUnitIntroducedUI);
        }

    }
}