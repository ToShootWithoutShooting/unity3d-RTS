using GameEnum;
using GameManager;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameEvent
{
    public partial class EventManager : Singleton<EventManager>, IEventManager
    {
        /// <summary>
        /// 存储所有已经注册的UI类事件
        /// </summary>
        protected Dictionary<UIEventEnum, List<EventHandler<GameEventArgs>>> UIEventHandlerList = new Dictionary<UIEventEnum, List<EventHandler<GameEventArgs>>>();

        /// <summary>
        /// 检查事件是否注册
        /// </summary>
        /// <param name="id"></param>
        /// <param name="handler"></param>
        /// <returns></returns>
        public bool Check(UIEventEnum type, EventHandler<GameEventArgs> handler)
        {
            if (!UIEventHandlerList.ContainsKey(type))
            {
                return false;
            }
            else if (!UIEventHandlerList[type].Contains(handler))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 发送事件
        /// </summary>
        /// <param name="type">事件类型</param>
        /// <param name="sender">发送者</param>
        /// <param name="e">需要传递的信息</param>
        public void Fire(UIEventEnum type, GameObject sender = null, GameEventArgs e = null)
        {
            if (UIEventHandlerList.ContainsKey(type))
            {
                if (UIEventHandlerList[type].Count > 0)
                {
                    for (int i = 0; i < UIEventHandlerList[type].Count; i++)
                    {
                        UIEventHandlerList[type][i](sender, e);
                    }
                }
            }
        }



        /// <summary>
        /// 注册事件
        /// </summary>
        /// <param name="id"></param>
        /// <param name="handler"></param>
        public void Subscribe(UIEventEnum type, EventHandler<GameEventArgs> handler)
        {
            if (!UIEventHandlerList.ContainsKey(type))
            {
                UIEventHandlerList.Add(type, new List<EventHandler<GameEventArgs>>());
            }
            UIEventHandlerList[type].Add(handler);
        }

        /// <summary>
        /// 取消事件监听
        /// </summary>
        /// <param name="type"></param>
        /// <param name="handler"></param>
        public void UnSubscribe(UIEventEnum type, EventHandler<GameEventArgs> handler)
        {
            if (Check(type, handler))
            {
                UIEventHandlerList[type].Remove(handler);
            }
        }

    }
}
