using GameEnum;
using Interface;
using System.Collections.Generic;
using UnityEngine;

namespace GameManager
{
    /// <summary>
    /// 管理游戏里所有游戏对象
    /// </summary>
    public class GameObjectsManager : Singleton<GameObjectsManager>
    {
        public Dictionary<GameObject, float> AllianceBuildings = new Dictionary<GameObject, float>();  //存储场景里联盟建筑单位
        public Dictionary<GameObject, float> AllianceBarracks = new Dictionary<GameObject, float>();
        public Dictionary<GameObject, float> AllianceOrdnanceFactorys = new Dictionary<GameObject, float>();
        public Dictionary<GameObject, float> AllianceAirports = new Dictionary<GameObject, float>();
        public Dictionary<GameObject, float> AllianceHarvesters = new Dictionary<GameObject, float>();
        public Dictionary<GameObject, float> AllianceSoliders = new Dictionary<GameObject, float>();    //存储场景里联盟地面兵种单位
        public Dictionary<GameObject, float> AllianceArmoredVehicles = new Dictionary<GameObject, float>();
        public Dictionary<GameObject, float> AllianceTanks = new Dictionary<GameObject, float>();
        public Dictionary<GameObject, float> AllianceMissileCars = new Dictionary<GameObject, float>();
        public Dictionary<GameObject, float> AllianceAirShips = new Dictionary<GameObject, float>();

        public Dictionary<GameObject, float> EmpireBuildings = new Dictionary<GameObject, float>();  //存储场景里联盟建筑单位
        public Dictionary<GameObject, float> EmpireBarracks = new Dictionary<GameObject, float>();
        public Dictionary<GameObject, float> EmpireOrdnanceFactorys = new Dictionary<GameObject, float>();
        public Dictionary<GameObject, float> EmpireAirports = new Dictionary<GameObject, float>();
        public Dictionary<GameObject, float> EmpireHarvesters = new Dictionary<GameObject, float>();
        public Dictionary<GameObject, float> EmpireSoliders = new Dictionary<GameObject, float>();    //存储场景里联盟地面兵种单位
        public Dictionary<GameObject, float> EmpireArmoredVehicles = new Dictionary<GameObject, float>();
        public Dictionary<GameObject, float> EmpireTanks = new Dictionary<GameObject, float>();
        public Dictionary<GameObject, float> EmpireMissileCars = new Dictionary<GameObject, float>();
        public Dictionary<GameObject, float> EmpireAirShips = new Dictionary<GameObject, float>();


        private IGameUnitDataFun gameCampUnitFun;
        

        #region 处理对象
            
        /// <summary>
        /// 获取单位集合
        /// </summary>
        /// <param name="group_Name"></param>
        /// <returns></returns>
        public Dictionary<GameObject, float> GetUnitsGroup(string group_Name, string camp)
        {
            if (camp == "Alliance")
            {
                
            }
            else
            {
                switch (group_Name)
                {
                    case "Harvester":
                        return EmpireHarvesters;
                    case "Solider":
                        return EmpireSoliders;
                    case "Tank":
                        return EmpireTanks;
                    case "AirShip":
                        return EmpireAirShips;
                }
            }
            return null;
        }

        public Dictionary<GameObject, float> GetBuildingsGroup(string group_Name, string camp)
        {
            if (camp == "Alliance")
            {

            }
            else
            {
                switch (group_Name)
                {
                    case "Barrack":
                        return EmpireBarracks;
                    case "OrdnanceFactory":
                        return EmpireOrdnanceFactorys;
                    case "Airport":
                        return EmpireAirports;
                }
            }
            return null;
        }

        public void AddGameObject(GameObject obj, float radius)
        {
            gameCampUnitFun = obj.GetComponent<IGameUnitDataFun>();
            if (gameCampUnitFun != null)
            {
                if (gameCampUnitFun.GetUnitCamp() == CampEnum.Alliance)
                {
                    switch (gameCampUnitFun.GetGameUnitType())
                    {
                        case UnitTypeEnum.CommandCenter:
                            if (!AllianceBuildings.ContainsKey(obj))
                                AllianceBuildings.Add(obj, radius);
                            break;
                        case UnitTypeEnum.Barrack:
                            if (!AllianceBarracks.ContainsKey(obj))
                                AllianceBarracks.Add(obj, radius);
                            if (!AllianceBuildings.ContainsKey(obj))
                                AllianceBuildings.Add(obj, radius);
                            break;
                        case UnitTypeEnum.OrdnanceFactory:
                            if (!AllianceOrdnanceFactorys.ContainsKey(obj))
                                AllianceOrdnanceFactorys.Add(obj, radius);
                            if (!AllianceBuildings.ContainsKey(obj))
                                AllianceBuildings.Add(obj, radius);
                            break;
                        case UnitTypeEnum.Airport:
                            if (!AllianceAirports.ContainsKey(obj))
                                AllianceAirports.Add(obj, radius);
                            if (!AllianceBuildings.ContainsKey(obj))
                                AllianceBuildings.Add(obj, radius);
                            break;
                        case UnitTypeEnum.Tower:
                            if (!AllianceBuildings.ContainsKey(obj))
                                AllianceBuildings.Add(obj, radius);
                            break;
                        case UnitTypeEnum.Harvester:
                            if (!AllianceHarvesters.ContainsKey(obj))
                                AllianceHarvesters.Add(obj, radius);
                            break;
                        case UnitTypeEnum.Solider:
                            if (!AllianceSoliders.ContainsKey(obj))
                                AllianceSoliders.Add(obj, radius);
                            break;
                        case UnitTypeEnum.ArmoredVehicle:
                            if (!AllianceArmoredVehicles.ContainsKey(obj))
                                AllianceArmoredVehicles.Add(obj, radius);
                            break;
                        case UnitTypeEnum.Tank:
                            if (!AllianceTanks.ContainsKey(obj))
                                AllianceTanks.Add(obj, radius);
                            break;
                        case UnitTypeEnum.MissileCar:
                            if (!AllianceMissileCars.ContainsKey(obj))
                                AllianceMissileCars.Add(obj, radius);
                            break;
                        case UnitTypeEnum.AirShip:
                            if (!AllianceAirShips.ContainsKey(obj))
                                AllianceAirShips.Add(obj, radius);
                            break;
                    }
                }
                else
                {
                    switch (gameCampUnitFun.GetGameUnitType())
                    {
                        case UnitTypeEnum.CommandCenter:
                            if (!EmpireBuildings.ContainsKey(obj))
                                EmpireBuildings.Add(obj, radius);
                            break;
                        case UnitTypeEnum.Barrack:
                            if (!EmpireBarracks.ContainsKey(obj))
                                EmpireBarracks.Add(obj, radius);
                            if (!EmpireBuildings.ContainsKey(obj))
                                EmpireBuildings.Add(obj, radius);
                            break;
                        case UnitTypeEnum.OrdnanceFactory:
                            if (!EmpireOrdnanceFactorys.ContainsKey(obj))
                                EmpireOrdnanceFactorys.Add(obj, radius);
                            if (!EmpireBuildings.ContainsKey(obj))
                                EmpireBuildings.Add(obj, radius);
                            break;
                        case UnitTypeEnum.Airport:
                            if (!EmpireAirports.ContainsKey(obj))
                                EmpireAirports.Add(obj, radius);
                            if (!EmpireBuildings.ContainsKey(obj))
                                EmpireBuildings.Add(obj, radius);
                            break;
                        case UnitTypeEnum.Tower:
                            if (!EmpireBuildings.ContainsKey(obj))
                                EmpireBuildings.Add(obj, radius);
                            break;
                        case UnitTypeEnum.Harvester:
                            if (!EmpireHarvesters.ContainsKey(obj))
                                EmpireHarvesters.Add(obj, radius);
                            break;
                        case UnitTypeEnum.Solider:
                            if (!EmpireSoliders.ContainsKey(obj))
                                EmpireSoliders.Add(obj, radius);
                            break;
                        case UnitTypeEnum.ArmoredVehicle:
                            if (!EmpireArmoredVehicles.ContainsKey(obj))
                                EmpireArmoredVehicles.Add(obj, radius);
                            break;
                        case UnitTypeEnum.Tank:
                            if (!EmpireTanks.ContainsKey(obj))
                                EmpireTanks.Add(obj, radius);
                            break;
                        case UnitTypeEnum.MissileCar:
                            if (!EmpireMissileCars.ContainsKey(obj))
                                EmpireMissileCars.Add(obj, radius);
                            break;
                        case UnitTypeEnum.AirShip:
                            if (!EmpireAirShips.ContainsKey(obj))
                                EmpireAirShips.Add(obj, radius);
                            break;
                    }
                }
            }
        }

        public void RemoveGameObject(GameObject obj)
        {
            gameCampUnitFun = obj.GetComponent<IGameUnitDataFun>();
            if (gameCampUnitFun != null)
            {
                if (gameCampUnitFun.GetUnitCamp() == CampEnum.Alliance)
                {
                    switch (gameCampUnitFun.GetGameUnitType())
                    {
                        case UnitTypeEnum.CommandCenter:
                            if (AllianceBuildings.ContainsKey(obj))
                                AllianceBuildings.Remove(obj);
                            break;
                        case UnitTypeEnum.Barrack:
                            if (AllianceBuildings.ContainsKey(obj))
                                AllianceBuildings.Remove(obj);
                            if (AllianceBarracks.ContainsKey(obj))
                                AllianceBarracks.Remove(obj);
                            break;
                        case UnitTypeEnum.OrdnanceFactory:
                            if (AllianceBuildings.ContainsKey(obj))
                                AllianceBuildings.Remove(obj);
                            if (AllianceOrdnanceFactorys.ContainsKey(obj))
                                AllianceOrdnanceFactorys.Remove(obj);
                            break;
                        case UnitTypeEnum.Airport:
                            if (AllianceBuildings.ContainsKey(obj))
                                AllianceBuildings.Remove(obj);
                            if (AllianceAirports.ContainsKey(obj))
                                AllianceAirports.Remove(obj);
                            break;
                        case UnitTypeEnum.Tower:
                            if (AllianceBuildings.ContainsKey(obj))
                                AllianceBuildings.Remove(obj);
                            break;
                        case UnitTypeEnum.Harvester:
                            if (AllianceHarvesters.ContainsKey(obj))
                                AllianceHarvesters.Remove(obj);
                            break;
                        case UnitTypeEnum.Solider:
                            if (AllianceSoliders.ContainsKey(obj))
                                AllianceSoliders.Remove(obj);
                            break;
                        case UnitTypeEnum.ArmoredVehicle:
                            if (AllianceArmoredVehicles.ContainsKey(obj))
                                AllianceArmoredVehicles.Remove(obj);
                            break;
                        case UnitTypeEnum.Tank:
                            if (AllianceTanks.ContainsKey(obj))
                                AllianceTanks.Remove(obj);
                            break;
                        case UnitTypeEnum.MissileCar:
                            if (AllianceMissileCars.ContainsKey(obj))
                                AllianceMissileCars.Remove(obj);
                            break;
                        case UnitTypeEnum.AirShip:
                            if (AllianceAirShips.ContainsKey(obj))
                                AllianceAirShips.Remove(obj);
                            break;
                    }
                }
                else
                {
                    switch (gameCampUnitFun.GetGameUnitType())
                    {
                        case UnitTypeEnum.CommandCenter:
                            if (EmpireBuildings.ContainsKey(obj))
                                EmpireBuildings.Remove(obj);
                            break;
                        case UnitTypeEnum.Barrack:
                            if (EmpireBuildings.ContainsKey(obj))
                                EmpireBuildings.Remove(obj);
                            if (EmpireBarracks.ContainsKey(obj))
                                EmpireBarracks.Remove(obj);
                            break;
                        case UnitTypeEnum.OrdnanceFactory:
                            if (EmpireBuildings.ContainsKey(obj))
                                EmpireBuildings.Remove(obj);
                            if (EmpireOrdnanceFactorys.ContainsKey(obj))
                                EmpireOrdnanceFactorys.Remove(obj);
                            break;
                        case UnitTypeEnum.Airport:
                            if (EmpireBuildings.ContainsKey(obj))
                                EmpireBuildings.Remove(obj);
                            if (EmpireAirports.ContainsKey(obj))
                                EmpireAirports.Remove(obj);
                            break;
                        case UnitTypeEnum.Tower:
                            if (EmpireBuildings.ContainsKey(obj))
                                EmpireBuildings.Remove(obj);
                            break;
                        case UnitTypeEnum.Harvester:
                            if (EmpireHarvesters.ContainsKey(obj))
                                EmpireHarvesters.Remove(obj);
                            break;
                        case UnitTypeEnum.Solider:
                            if (EmpireSoliders.ContainsKey(obj))
                                EmpireSoliders.Remove(obj);
                            break;
                        case UnitTypeEnum.ArmoredVehicle:
                            if (EmpireArmoredVehicles.ContainsKey(obj))
                                EmpireArmoredVehicles.Remove(obj);
                            break;
                        case UnitTypeEnum.Tank:
                            if (EmpireTanks.ContainsKey(obj))
                                EmpireTanks.Remove(obj);
                            break;
                        case UnitTypeEnum.MissileCar:
                            if (EmpireMissileCars.ContainsKey(obj))
                                EmpireMissileCars.Remove(obj);
                            break;
                        case UnitTypeEnum.AirShip:
                            if (EmpireAirShips.ContainsKey(obj))
                                EmpireAirShips.Remove(obj);
                            break;
                    }
                }
            }
        }
        #endregion

        #region 检测碰撞
        /// <summary>
        /// 检测是否碰撞到目标类型单位
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="radius"></param>
        /// <returns></returns>
        ///         
        public GameObject GetCollisionUnit(Vector3 pos, float radius, CollisionDetectionType unitType)
        {
            switch (unitType)
            {
                case CollisionDetectionType.AllianceBuilding:
                    return Iteration(AllianceBuildings.GetEnumerator(),pos,radius);
                case CollisionDetectionType.AllianceHarvester:
                    return Iteration(AllianceHarvesters.GetEnumerator(), pos, radius);
                case CollisionDetectionType.AllianceSolider:
                    return Iteration(AllianceSoliders.GetEnumerator(), pos, radius);
                case CollisionDetectionType.AllianceArmoredVehicle:
                    return Iteration(AllianceArmoredVehicles.GetEnumerator(), pos, radius);
                case CollisionDetectionType.AllianceTank:
                    return Iteration(AllianceTanks.GetEnumerator(), pos, radius);
                case CollisionDetectionType.AllianceMissileCar:
                    return Iteration(AllianceMissileCars.GetEnumerator(), pos, radius);
                case CollisionDetectionType.AllianceAirShip:
                    return Iteration(AllianceAirShips.GetEnumerator(), pos, radius);
                case CollisionDetectionType.EmpireBuilding:
                    return Iteration(EmpireBuildings.GetEnumerator(), pos, radius);
                case CollisionDetectionType.EmpireHarvester:
                    return Iteration(EmpireHarvesters.GetEnumerator(), pos, radius);
                case CollisionDetectionType.EmpireSolider:
                    return Iteration(EmpireSoliders.GetEnumerator(), pos, radius);
                case CollisionDetectionType.EmpireArmoredVehicle:
                    return Iteration(EmpireArmoredVehicles.GetEnumerator(), pos, radius);
                case CollisionDetectionType.EmpireTank:
                    return Iteration(EmpireTanks.GetEnumerator(), pos, radius);
                case CollisionDetectionType.EmpireMissileCar:
                    return Iteration(EmpireMissileCars.GetEnumerator(), pos, radius);
                case CollisionDetectionType.EmpireAirShip:
                    return Iteration(EmpireAirShips.GetEnumerator(), pos, radius);
            }
            return null;
        }

        GameObject Iteration(Dictionary<GameObject, float>.Enumerator iter, Vector3 pos, float radius)
        {
            while (iter.MoveNext())
            {
                KeyValuePair<GameObject, float> value = iter.Current;
                Vector3 pos1 = value.Key.transform.position;
                pos1.y = 0;
                Vector3 pos2 = pos;
                pos2.y = 0;
                if (Vector3.Distance(pos1, pos2) <= (radius + value.Value))
                {
                    return value.Key;
                }
            }
            iter.Dispose();
            return null;
        }

        #endregion

        #region 在某一区域的对象判断
        public void ObjectsInRange(Vector3 start,Vector3 end)
        {
            Vector3 p1 = Vector3.zero;
            Vector3 p2 = Vector3.zero;
            if (start.x > end.x)
            {//这些判断是用来确保p1的xy坐标小于p2的xy坐标，因为画的框不见得就是左下到右上这个方向的
                p1.x = end.x;
                p2.x = start.x;
            }
            else
            {
                p1.x = start.x;
                p2.x = end.x;
            }
            if (start.y > end.y)
            {
                p1.y = end.y;
                p2.y = start.y;
            }
            else
            {
                p1.y = start.y;
                p2.y = end.y;
            }
            Dictionary<GameObject, float>.Enumerator[] units = new Dictionary<GameObject, float>.Enumerator[6];
            if (GameMapManager.Instance.PlayerCamp == CampEnum.Alliance)
            {
                units[0] = AllianceHarvesters.GetEnumerator();
                units[1] = AllianceSoliders.GetEnumerator();
                units[2] = AllianceArmoredVehicles.GetEnumerator();
                units[3] = AllianceTanks.GetEnumerator();
                units[4] = AllianceMissileCars.GetEnumerator();
                units[5] = AllianceAirShips.GetEnumerator();
            }
            else
            {
                units[0] = EmpireHarvesters.GetEnumerator();
                units[1] = EmpireSoliders.GetEnumerator();
                units[2] = EmpireArmoredVehicles.GetEnumerator();
                units[3] = EmpireTanks.GetEnumerator();
                units[4] = EmpireMissileCars.GetEnumerator();
                units[5] = EmpireAirShips.GetEnumerator();
            }

            for (int i = 0; i < units.Length; ++i)
            {
                while (units[i].MoveNext())
                {
                    KeyValuePair<GameObject, float> value = units[i].Current;
                    Vector3 location = Camera.main.WorldToScreenPoint(value.Key.transform.position);//把对象的position转换成屏幕坐标
                    if (location.x > p1.x && location.x < p2.x && location.y > p1.y && location.y < p2.y
                    && location.z > Camera.main.nearClipPlane && location.z < Camera.main.farClipPlane)//z方向就用摄像机的设定值，看不见的也不需要选择了
                    {
                        value.Key.GetComponent<IPlayerCampUnitsBase>().SetSelected(true);
                    }
                }
                units[i].Dispose();
            }
            
        }
        #endregion
    }
}
