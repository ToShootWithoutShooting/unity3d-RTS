namespace GameEnum
{
    public class EnumChange<T>
    {
        public static T StringToEnum(string _value)
        {
            //把要转化的枚举用泛型来代替
            return (T)System.Enum.Parse(typeof(T), _value);
        }
    }
    
    public enum CampEnum
    {
        Alliance, // 联盟
        Empire, //帝国
    }

    public enum GameDifficulty
    {
        easy,
        normal,
        hard,
    }

    public enum GameUnitName
    {
        AllianceHarvester,  //联盟矿车
        AllianceTank,  //联盟坦克
        AllianceSolider,  //联盟士兵
        AllianceHummer,  //联盟悍马
        AllianceMissileCar,  //联盟导弹车

        AllianceCenter,  //联盟指挥中心
        AllianceBarrack,  //联盟兵营
        AllianceOrdnanceFactory,  //联盟军工厂
        AllianceAirport,
        AllianceAirShip,

        EmpireHarvester,  //帝国矿车
        EmpireTank,  //帝国坦克
        EmpireSolider,  //帝国士兵
        EmpireHummer,  //帝国悍马
        EmpireMissileCar,  //帝国导弹车
        EmpireAirport,
        EmpireAirShip,

        EmpireCenter,  //帝国指挥中心
        EmpireBarrack,  //帝国兵营
        EmpireOrdnanceFactory,  //帝国军工厂
    }
    public enum UnitTypeEnum
    {
        CommandCenter,  //主基地
        Barrack,    //兵营
        OrdnanceFactory,  //军工厂
        Airport,    //飞机场
        Tower,  //堡垒
        Harvester,  //矿车
        Solider,   //士兵
        ArmoredVehicle,    //装甲车
        Tank,   //坦克
        MissileCar,   //导弹车
        AirShip,    //飞船
    }

    public enum CollisionDetectionType
    {
        AllianceBuilding,  //
        AllianceHarvester,
        AllianceSolider,    //
        AllianceArmoredVehicle,
        AllianceTank,
        AllianceMissileCar,
        AllianceAirShip,
        EmpireBuilding,
        EmpireHarvester,
        EmpireSolider,
        EmpireArmoredVehicle,
        EmpireTank,
        EmpireMissileCar,
        EmpireAirShip,
    }

    public enum ShellTypeEnum
    {
        bullet,
        shell,
    }
}
