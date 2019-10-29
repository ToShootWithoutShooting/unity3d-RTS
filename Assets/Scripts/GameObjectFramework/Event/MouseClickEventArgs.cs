using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameEvent
{
    /// <summary>
    /// 鼠标点击事件传递信息
    /// </summary>
    public class MouseClickEventArgs : GameEventArgs
    {
        public Vector3 ClickPosition;     //点击位置
        public MouseClickEventArgs()
        {

        }
        
        public MouseClickEventArgs(GameObject gameObject, Vector3 position)
        {
            if (null != gameObject)
            {
                this.dataGameobject = gameObject;
            }
            ClickPosition = position;
        }


    }
}
