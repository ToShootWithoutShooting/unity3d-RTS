using UnityEngine;
using System.Collections.Generic;
using GameManager;
using System;
using GameEnum;

namespace GameEvent
{
    public partial class EventManager : Singleton<EventManager>, IEventManager {
        
        /// <summary>
        /// 存储所有已经注册的事件
        /// </summary>
        protected Dictionary<GameEventEnum, List<EventHandler<GameEventArgs>>> EventHandlerList = new Dictionary<GameEventEnum, List<EventHandler<GameEventArgs>>>();

        /// <summary>
        /// 检查事件是否注册
        /// </summary>
        /// <param name="id"></param>
        /// <param name="handler"></param>
        /// <returns></returns>
        public bool Check(GameEventEnum type, EventHandler<GameEventArgs> handler)
        {
            if (!EventHandlerList.ContainsKey(type))
            {
                return false;
            }
            else if(!EventHandlerList[type].Contains(handler))
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
        public void Fire(GameEventEnum type, GameObject sender = null, GameEventArgs e = null)
        {
            if (EventHandlerList.ContainsKey(type))
            {
                if (EventHandlerList[type].Count > 0)
                {
                    for (int i = 0; i < EventHandlerList[type].Count; i++)
                    {
                        EventHandlerList[type][i](sender, e);
                    }
                }
            }
        }


        
        /// <summary>
        /// 注册事件
        /// </summary>
        /// <param name="id"></param>
        /// <param name="handler"></param>
        public void Subscribe(GameEventEnum type, EventHandler<GameEventArgs> handler)
        {
            if (!EventHandlerList.ContainsKey(type))
            {
                EventHandlerList.Add(type, new List<EventHandler<GameEventArgs>>());
            }
            EventHandlerList[type].Add(handler);
        }

        /// <summary>
        /// 取消事件监听
        /// </summary>
        /// <param name="type"></param>
        /// <param name="handler"></param>
        public void UnSubscribe(GameEventEnum type, EventHandler<GameEventArgs> handler)
        {
            if(Check(type, handler))
            {
                EventHandlerList[type].Remove(handler);
            }
        }

    }
}