using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameEvent;
using GameEnum;
using GameData;
using GameManager;
using GameManager.UI;
using System;
using Interface;

namespace RTS.AI
{
    public class CommandCenterAI : MonoBehaviour
    {
        private CampEnum camp;  //
        private CampEnum enemy_Camp;    
        public Vector3[] MinePos; //   
        private Collider[] enemyArray;
        private Collider[] battleUnits;
        private GameCampData gameCampData;
        private string enemy_CampName;
        private string campName;

        public string productUnit_Next; //下一次要生产的单位
        private bool IsEnough_Product = true;
        
        private Dictionary<Vector3, bool> Product_Points = new Dictionary<Vector3, bool>();
        private Dictionary<string, float> Product_Weight = new Dictionary<string, float>();
        private Dictionary<string, int> ConstructUnitList = new Dictionary<string, int>();

        private int anamyCount;

        public int H_Num;
        public int S_Num;
        public int T_Num;
        public int P_Num;

        private void Awake()
        {
            if (gameObject.layer == 8)
            {
                camp = CampEnum.Alliance;
                gameCampData = GameDataManager.Instance.AllianceCampData;
                enemy_Camp = CampEnum.Empire;
            }
            else
            {
                camp = CampEnum.Empire;
                gameCampData = GameDataManager.Instance.EmpireCampData;
                enemy_Camp = CampEnum.Alliance;
            }
            enemy_CampName = enemy_Camp.ToString();
            campName = camp.ToString();
            Collider[] colliders = Physics.OverlapSphere(transform.position, 500, 1 << LayerMask.NameToLayer("Mine"));
            MinePos = new Vector3[colliders.Length];
            for (int i = 0; i < colliders.Length; ++i)
            {
                MinePos[i] = colliders[i].transform.position;
            }
            GameDataManager.Instance.Production_Judgment += Production_Judgment;
            productUnit_Next = UnitTypeEnum.Harvester.ToString();
            StartCoroutine(Enumerable());

            EventManager.Instance.Subscribe(GameEventEnum.AI_BattleUnitToGenerate, CommandArmy);
        }

        private void Start()
        {
            Dictionary<string, UnitDescribeData>.Enumerator iter = UIManager.Instance.game_UnitDescribe.GetEnumerator();
            while (iter.MoveNext())
            {
                KeyValuePair<string, UnitDescribeData> value = iter.Current;
                int[] cost = new int[2];
                string[] Cost = value.Value.unit_Cost.Split('|');
                for (int i = 0; i < 2; i++)
                {
                    cost[i] = Convert.ToInt32(Cost[i]);
                }
                ConstructUnitList.Add(value.Key, cost[1]);
            }
            iter.Dispose();
            GameDataManager.Instance.ConstructUnitList = ConstructUnitList;

            InitAIDifficulty();
            Priority_Product();

            Vector3 pos = transform.position;
            Product_Points.Add(new Vector3(pos.x, 0, pos.z - 100), false);
            Product_Points.Add(new Vector3(pos.x + 150, 0, pos.z), false);
            Product_Points.Add(new Vector3(pos.x + 150, 0, pos.z - 100), false);
            Product_Points.Add(new Vector3(pos.x - 150, 0, pos.z - 100), false);
            Product_Points.Add(new Vector3(pos.x, 0, pos.z + 100), false);
            Product_Points.Add(new Vector3(pos.x - 150, 0, pos.z + 100), false);
            Product_Points.Add(new Vector3(pos.x + 150, 0, pos.z + 100), false);

        }
            
        IEnumerator Enumerable()
        {   
            yield return new WaitForSeconds(1);
            EventManager.Instance.Fire(GameEventEnum.ProductHarvesterOrder, null, new GameEventArgs(campName + "Harvester"));
        }   
            
        void Update()
        {   
            enemyArray = Physics.OverlapSphere(transform.position, 250, 1 << LayerMask.NameToLayer(enemy_CampName));
            anamyCount = enemyArray.Length;
            
        }   
        
        public void Production_Judgment()
        {
            if (!IsEnough_Product)
                Product_Unit(productUnit_Next);
            else
                Priority_Product();
        }

        private void InitAIDifficulty()
        {
            if (GameMapManager.Instance.gameDifficulty == GameDifficulty.easy)
                Product_Weight.Add("Harvester", 5.0f);
            else if (GameMapManager.Instance.gameDifficulty == GameDifficulty.normal)
                Product_Weight.Add("Harvester", 15.0f);
            else
                Product_Weight.Add("Harvester", 25.0f);
            Product_Weight.Add("Solider", 5.0f);
            Product_Weight.Add("Tank", 3.0f);
            Product_Weight.Add("AirShip", 2.0f);

        }
        
        /// <summary>
        /// 生产单位优先级判断
        /// </summary>
        public void Priority_Product()
        {
            foreach(var k in Product_Weight)
            {
                bool flag = true;
                foreach (var v in Product_Weight)
                {
                    if (k.Key != v.Key)
                    {
                        float kNum = AISystemManager.Instance.units_Count[k.Key];
                        kNum = kNum == 0f ? 1f : kNum;
                        float vNum = AISystemManager.Instance.units_Count[v.Key];
                        vNum = vNum == 0f ? 1f : vNum;
                        if ((kNum / vNum) > (Product_Weight[k.Key] / Product_Weight[v.Key]))
                        {
                            flag = false;
                            break;
                        }
                    }
                }
                if (!flag)
                    continue;
                else
                {
                    productUnit_Next = k.Key;
                    Product_Unit(productUnit_Next);
                }
            }       
        }
        
        void Product_Unit(string unit_Name)
        {
            if (gameCampData.Mine_Count >= ConstructUnitList[unit_Name])  //资源足够
            {
                switch (unit_Name)   //判断是否有足够的位置生产
                {
                    case "Harvester":
                        if (GetComponent<IProductingUnits>().GetRemainingCount() > 0)
                        {
                            IsEnough_Product = true;
                            H_Num++;
                            AISystemManager.Instance.AddUnitCount(unit_Name);
                            gameCampData.Mine_Count -= ConstructUnitList[unit_Name];
                            GetComponent<IProductingUnits>().ProductUnits(campName + unit_Name);
                            Priority_Product();
                        }
                        break;
                    case "Solider":
                        Product_ArmyUnit("Barrack", unit_Name);
                        break;
                    case "Tank":
                        Product_ArmyUnit("OrdnanceFactory", unit_Name);
                        break;
                    case "AirShip":
                        Product_ArmyUnit("Airport", unit_Name);
                        break;
                    case "Barrack":
                        ProductBuilding(unit_Name);
                        break;
                    case "OrdnanceFactory":
                        ProductBuilding(unit_Name);
                        break;
                    case "Airport":
                        ProductBuilding(unit_Name);
                        break;
                }
            }
            else
            {
                IsEnough_Product = false;   //资源不足
                productUnit_Next = unit_Name;
            }
            return;
        }

        /// <summary>
        /// 生产兵种兵种
        /// </summary>
        void Product_ArmyUnit(string buliding_Name, string unit_Name)
        {
            productUnit_Next = unit_Name;
            foreach (var j in GameObjectsManager.Instance.GetBuildingsGroup(buliding_Name, campName))
            {
                if (j.Key.GetComponent<IProductingUnits>().GetRemainingCount() > 0)
                {
                    if (unit_Name == "Solider")
                        S_Num++;
                    else if (unit_Name == "Tank")
                        T_Num++;
                    else
                        P_Num++;
                    AISystemManager.Instance.AddUnitCount(unit_Name);
                    gameCampData.Mine_Count -= ConstructUnitList[unit_Name];
                    j.Key.GetComponent<IProductingUnits>().ProductUnits(campName + unit_Name);
                    IsEnough_Product = true;
                    Priority_Product();
                    return;
                }
            }
            ProductBuilding(buliding_Name);

        }   
            
        /// <summary>
        /// 生产建筑
        /// </summary>
        /// <param name="unit_Name"></param>
        void ProductBuilding(string unit_Name)
        {
            if (gameCampData.Mine_Count < ConstructUnitList[unit_Name])
            {
                IsEnough_Product = false;
                productUnit_Next = unit_Name;
                return;
            }
            else if(AISystemManager.Instance.Producting_Units.Contains(unit_Name))
            {
                IsEnough_Product = false;
                return;
            }
            Vector3 pos = transform.position;
            foreach (var v in Product_Points)
            {
                if (!v.Value)
                {
                    pos = v.Key;
                    break;
                }
            }
            foreach (var j in GameObjectsManager.Instance.GetUnitsGroup("Harvester", campName))
            {
                if (j.Key.GetComponent<Harvester>().harvesterState != HarvesterState.build)
                {
                    gameCampData.Mine_Count -= ConstructUnitList[unit_Name];
                    AISystemManager.Instance.Producting_Units.Add(unit_Name);
                    j.Key.GetComponent<Harvester>().ProductBuilding_AI(unit_Name, pos);
                    Product_Points[pos] = true;
                    IsEnough_Product = true;
                    Priority_Product();
                    return;
                }
            }
            IsEnough_Product = false;
            productUnit_Next = "Harvester";
            Product_Unit("Harvester");

        }   
        
        void CommandArmy(object sender, GameEventArgs e)
        {
            battleUnits = Physics.OverlapSphere(transform.position, 250, 1 << LayerMask.NameToLayer(campName));
            int num = 0;
            for(int i = 0; i < battleUnits.Length; ++i)
            {
                if (battleUnits[i].gameObject.GetComponent<IGameUnitBattleFun>() != null)
                    num++;
            }

            if (num < 12 || anamyCount > 0)
                return;
            EventManager.Instance.Fire(GameEventEnum.AI_AttackOrder);


        }   
            
    }       
}