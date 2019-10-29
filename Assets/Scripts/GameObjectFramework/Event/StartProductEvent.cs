using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameEvent
{
    public class StartProductEvent : GameEventArgs
    {
        public string _unitName;
        public int ProductTiem;
        public StartProductEvent(string name,int time)
        {
            _unitName = name;
            ProductTiem = time;
        }

    }
}
