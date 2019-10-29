using GameEnum;
using GameEvent;
using GameManager;
using System.Collections.Generic;


public class AISystemManager : Singleton<AISystemManager>
{
    public List<string> Producting_Units = new List<string>();   //存储正在建筑的单位
    public Dictionary<string, float> units_Count = new Dictionary<string, float>();     //存储拥有单位数量

    public int battleUnits_Count;
    public int BattleUnits_Count {
        get { return battleUnits_Count; }
        set
        {
            battleUnits_Count = value;
            //if (battleUnits_Count > 10)
            //    EventManager.Instance.Fire(GameEventEnum.AI_AttackOrder);
        }
    }


    public override void Init()
    {
        base.Init();
        units_Count.Add("Harvester",0f);
        units_Count.Add("Solider", 0f);
        units_Count.Add("Tank", 0f);
        units_Count.Add("AirShip", 0f);
        units_Count.Add("Barrack", 0f);
        units_Count.Add("OrdnanceFactory", 0f);
        units_Count.Add("Airport", 0f);
    }

    public void AddUnitCount(string unit_Name)
    {
        units_Count[unit_Name]++;
        BattleUnits_Count++;
    }

    public void ReduceUnitCount(string unit_Name)
    {
        units_Count[unit_Name]--;
        BattleUnits_Count--;
    }

}   
