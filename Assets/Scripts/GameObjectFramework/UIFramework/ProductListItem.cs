using System.Collections;
using UnityEngine;
using FairyGUI;
using Interface;
using GameEvent;
using GameManager;
using System;
using GameEnum;

namespace GameUI
{
    struct Unit_UI
    {
        public string name;
        public int cost;
        public Unit_UI(string name, int cost)
        {
            this.name = name;
            this.cost = cost;
        }
    }
    public class ProductListItem : GComponent
    {
        private GameObject unit;
        private GTextField txtProdingNum;
        private GLoader ProdingIcon;
        private string unit_Name;
        private string unit_Type;
        private bool IsBuilding;
        private IProductingUnits productingUnits;
        private IHarvester harvester;
        private GImage CoolingPanel;
        private int Cost;

        public override object data
        {
            set
            {
                base.data = value;
                SetShowData();
            }
        }

        public override void ConstructFromXML(FairyGUI.Utils.XML cxml)
        {
            base.ConstructFromXML(cxml);
            txtProdingNum = GetChild("txtProdingNum").asTextField;
            ProdingIcon = GetChild("ProdingIcon").asLoader;
            CoolingPanel = GetChild("CoolingPanel").asImage;
            onClick.Add(ClickProductUnit);
            if (!EventManager.Instance.Check(UIEventEnum.StartProducting, StartProducting))
                EventManager.Instance.Subscribe(UIEventEnum.StartProducting, StartProducting);
            if (!EventManager.Instance.Check(UIEventEnum.CompletedProucting, CompletedProucting))
                EventManager.Instance.Subscribe(UIEventEnum.CompletedProucting, CompletedProucting); 
            onRollOver.Add(OnRollOver);
            onRollOut.Add(OnRollOut);
        }
        
        void OnRollOver()
        {
            EventManager.Instance.Fire(UIEventEnum.ShowUnitIntroducedUI, null, new GameEventArgs(new Unit_UI(unit_Type, Cost)));
        }

        void OnRollOut()
        {
            EventManager.Instance.Fire(UIEventEnum.CloseUnitIntroducedUI);
        }

        void SetShowData()
        {
            ComponentData componentData = (ComponentData)data;
            unit = componentData.unit;
            
            unit_Name = componentData.UnitName;
            unit_Type = unit_Name;
            Cost = GameDataManager.Instance.ConstructUnitList[unit_Type];
            if (GameMapManager.Instance.PlayerCamp == CampEnum.Alliance)
                unit_Name = "Alliance"+ unit_Name;
            else
                unit_Name = "Empire"+ unit_Name;
            
            ProdingIcon.asLoader.url = "ui://UI/" + unit_Name;
            
            IsBuilding = componentData.isBuilding;
            CoolingPanel.visible = !IsBuilding;
            if (IsBuilding)
            {
                harvester = unit.GetComponent<IHarvester>();
                txtProdingNum.visible = false;
                CoolingPanel.visible = false;
                txtProdingNum.text = 0.ToString();
            }
            else
            {
                productingUnits = unit.GetComponent<IProductingUnits>();
                int count = 0;
                foreach (var value in productingUnits.GetProductionQueue())
                {
                    if (value.ToString() == unit_Name)
                        count++;
                }
                if (count > 0)
                {
                    txtProdingNum.visible = true;
                    CoolingPanel.visible = true;
                    txtProdingNum.text = count.ToString();
                    if (productingUnits.GetProductionQueue().ToArray()[0].ToString() == unit_Name)
                    {
                        int time = productingUnits.GetProductionTime() % 40;
                        if (time == 0)
                            time = 40;
                        GameMapManager.Instance.coroutinesManager._StartCoroutine(SetCoolingPanel(time));
                    }
                    else
                        CoolingPanel.fillAmount = 1;
                }
                else
                {
                    txtProdingNum.visible = false;
                    CoolingPanel.visible = false;
                    txtProdingNum.text = 0.ToString();
                }
            }
        }

        void StartProducting(object sender, GameEventArgs e)
        {
            StartProductEvent startProductEvent = e as StartProductEvent;
            if ((sender as GameObject) != unit)
                return;
            if (startProductEvent._unitName != unit_Name)
            {
                if (Convert.ToInt32(txtProdingNum.text) > 0)
                {
                    CoolingPanel.fillAmount = 1;
                    txtProdingNum.visible = true;
                    CoolingPanel.visible = true;
                }
                else
                {
                    txtProdingNum.visible = false;
                    CoolingPanel.visible = false;
                }
                return;
            }
            GameMapManager.Instance.coroutinesManager._StartCoroutine(SetCoolingPanel(startProductEvent.ProductTiem));
            
        }
        

        void CompletedProucting(object sender, GameEventArgs e)
        {
            StartProductEvent startProductEvent = e as StartProductEvent;
            if ((sender as GameObject) != unit || startProductEvent._unitName != unit_Name)
                return;
            int n = Convert.ToInt32(txtProdingNum.text) - 1;
            txtProdingNum.text = n.ToString();
            if (n <= 0)
            {
                txtProdingNum.visible = false;
                CoolingPanel.visible = true;
            }
        }

        IEnumerator SetCoolingPanel(int unitProductionTime)
        {
            txtProdingNum.visible = true;
            CoolingPanel.visible = true;
            while (unitProductionTime >= 0)
            {
                CoolingPanel.fillAmount = unitProductionTime / 40.0f;
                yield return new WaitForSeconds(0.1f);
                unitProductionTime--;
            }
        }

        void ClickProductUnit()
        {
            if (Cost > GameDataManager.Instance.playerCampData.Mine_Count)
                return;
            if (null == productingUnits && harvester == null)
                return;
            if (IsBuilding)
                harvester.ProductBuilding(unit_Name, Cost);
            else
            {
                if (productingUnits.GetProductionQueue().Count > 0 && productingUnits.GetProductionQueue().ToArray()[0].ToString() != unit_Name)
                {
                    CoolingPanel.fillAmount = 1;
                }
                txtProdingNum.visible = true;
                CoolingPanel.visible = true;
                int n = Convert.ToInt32(txtProdingNum.text);
                if (productingUnits.GetRemainingCount() > 0)
                {
                    GameDataManager.Instance.ChangeMineCount(-Cost, GameMapManager.Instance.PlayerCamp, true);
                    txtProdingNum.text = (n + 1).ToString();
                }
                productingUnits.ProductUnits(unit_Name);
            }
        }
        

        public override void Dispose()
        {
            base.Dispose();
            EventManager.Instance.UnSubscribe(UIEventEnum.StartProducting, StartProducting);
            EventManager.Instance.UnSubscribe(UIEventEnum.CompletedProucting, CompletedProucting);
        }

    }
}
