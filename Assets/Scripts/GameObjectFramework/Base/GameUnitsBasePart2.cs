using GameEnum;
using GameEvent;
using Interface;
using UnityEngine;

namespace RTS
{
    /// <summary>
    /// 联盟游戏基类，继承自游戏阵营单位基类
    /// </summary>
    public abstract partial class GameUnitsBase : GameEntityBase, IPlayerCampUnitsBase
    {
        #region 初始化
        protected override void BindEvent()
        {
            base.BindEvent();
            if (!EventManager.Instance.Check(GameEventEnum.PlayerPressLeftMouseEvent, ClickLeftMouseEvent))
                EventManager.Instance.Subscribe(GameEventEnum.PlayerPressLeftMouseEvent, ClickLeftMouseEvent);
            if (!EventManager.Instance.Check(GameEventEnum.PlayerPressRightMouseEvent, ClickRightMouseEvent))
                EventManager.Instance.Subscribe(GameEventEnum.PlayerPressRightMouseEvent, ClickRightMouseEvent);
        }
        protected override void UnBindEvent()
        {
            base.UnBindEvent();
            EventManager.Instance.UnSubscribe(GameEventEnum.PlayerPressLeftMouseEvent, ClickLeftMouseEvent);
            EventManager.Instance.UnSubscribe(GameEventEnum.PlayerPressRightMouseEvent, ClickRightMouseEvent);
        }
        #endregion


        #region 玩家操控
        public void SetSelected(bool Selected)
        {
            IsSelected = Selected;
        }

        public virtual void SetSelectEffect()
        {
            if (null != trollDrawLine)
                trollDrawLine.rendering = isSelected;
            if (highlightable == null)
                return;
            if (isSelected)
                highlightable.ConstantOn(Color.red);
            else
                highlightable.ConstantOff();
            
        }

        public virtual void ClickLeftMouseEvent(object sender, GameEventArgs e)
        {
            if (!IsPlayerCamp)
                return;
            MouseClickEventArgs m = e as MouseClickEventArgs;

            if (gameObject == m.dataGameobject)//如果点击的是该游戏对象
                IsSelected = !(IsSelected);
            else
                IsSelected = false;

        }
        public virtual void ClickRightMouseEvent(object sender, GameEventArgs e) { }
        #endregion

        #region AI操控

        #endregion
    }
}
