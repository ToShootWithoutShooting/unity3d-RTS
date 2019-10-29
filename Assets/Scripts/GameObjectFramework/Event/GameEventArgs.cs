using System;
using UnityEngine;

namespace GameEvent
{
    public class GameEventArgs : EventArgs
    {
        public GameObject dataGameobject; //传送的对象 
        public object data;
        public GameEventArgs()
        {

        }

        public GameEventArgs(object Data)
        {
            data = Data;
        }

        public GameEventArgs(GameObject gameObject)
        {
            dataGameobject = gameObject;
        }

        public virtual void Clear()
        {

        }
    }
}
