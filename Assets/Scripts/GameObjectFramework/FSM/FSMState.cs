using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


namespace FSM
{
    public enum Transition//转换条件
    {
        NoTransition,   //没有转换

    }

    public enum StateID//状态ID，每个状态有一个id
    {
        NoState,   //没有状态
        Idle,   //
        Move,   //移动
        Attack,    //攻击
        Mining,     
    }
    public abstract class FSMState
    {
        public NavMeshAgent _navMeshAgent;
        public GameObject unit_Gameobject;
        public StateID State_ID { get; protected set; }
        protected Dictionary<Transition, StateID> transitionToStates = new Dictionary<Transition, StateID>();//存储转换条件和对应转换的状态
        public void AddTransition(Transition transition, StateID id)
        {
            if (transition == Transition.NoTransition || State_ID == StateID.NoState)//如果状态或者id为空，弹出警告
            {
                Debug.LogError("Transition or ID is null");//弹出错误
                return;
            }
            if (transitionToStates.ContainsKey(transition))//判断是否已包含t状态
            {
                Debug.LogError("this transition is already has");
                return;
            }
            transitionToStates.Add(transition, id);//符合条件添加状态和对应ID
        }

        public void DeleteTransition(Transition transition)//删除状态
        {
            if (transitionToStates.ContainsKey(transition) == false)
            {
                Debug.LogWarning("the Transition is no exit");
            }
            transitionToStates.Remove(transition);//删除状态
        }

        public StateID GetOutputState(Transition transition)//根据传过来的转换条件是否可以发生转换
        {
            if (transitionToStates.ContainsKey(transition))//如果该状态存在
            {
                return transitionToStates[transition];
            }
            return StateID.NoState;
        }

        public virtual void BeforEntering(object data = null) { }//进入之前的操作

        public virtual void BeforLeaving(object data = null) { }//离开之前的操作

        public virtual void DoUpdate() { }

    }
}
