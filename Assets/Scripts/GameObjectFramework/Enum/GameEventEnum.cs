namespace GameEnum
{
    /// <summary>
    /// 游戏事件枚举
    /// </summary>
    public enum GameEventEnum
    {
        ProductRobotOrder,  //生产机器人指令
        ProductTankOrder,  //生产坦克指令
        ProductPlaneOrder, //生产飞机指令
        ProductHarvesterOrder, //生产矿车指令

        AI_AttackOrder,  //<AI>进攻指令
        AI_StopAttackOrder,  //<AI>停止进攻指令
        AI_BackToBaseOrder,  //<AI>返回基地指令
        AI_MoveToAimOrder,  //<AI>移动到目的地指令
        AI_BattleUnitToGenerate,    //<AI>生成作战单位

        PlayerPressLeftMouseEvent, //<玩家>按下鼠标左键
        PlayerPressRightMouseEvent, //<玩家>按下鼠标右键

        ChangeAttackTarget,  //<作战单位>切换攻击单位

        AttackEnemyBase,  //<电脑方>攻击敌方基地
        MoveToTarget,   //<电脑方>移动到目标位置

        StartProducting,    //开始建造

        Click_MiniMap,    //点击小地图
    }
}