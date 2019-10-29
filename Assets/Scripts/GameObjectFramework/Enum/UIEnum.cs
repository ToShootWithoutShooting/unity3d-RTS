namespace GameEnum
{
    /// <summary>
    /// ui面板枚举
    /// </summary>
    public enum UIPanelEnum
    {
        StartUI,   //开始界面
        MapSettingUI,
        MainUI,  //游戏主界面
        GameEndUI,

    }

    public enum UIDataEnum
    {

    }

    public enum UIEventEnum
    {
        OpenUIPanel,    //<UI>打开UI界面S
        StartProducting,    //开始生产
        CompletedProucting,    //完成生产
        ShowUnitIntroducedUI,   //显示单位介绍UI
        CloseUnitIntroducedUI,   //关闭单位介绍UI
    }
}
