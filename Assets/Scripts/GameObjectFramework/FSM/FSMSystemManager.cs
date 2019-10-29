using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace FSM
{
    //状态机系统管理,添加删除状态,管理状态的转换。
    public class FSMSystemManager
    {
        Dictionary<StateID, FSMState> statesList;
        GameObject FatherUnit;
        NavMeshAgent _navMeshAgent;
        public FSMState CurrentState { get; set; }    //当前对象所处的状态
        public FSMSystemManager(GameObject gameObject,NavMeshAgent navMeshAgent)
        {
            FatherUnit = gameObject;
            statesList = new Dictionary<StateID, FSMState>();
            _navMeshAgent = navMeshAgent;
        }

        public void AddState(FSMState state)
        {
            if (state == null)
            {
                Debug.LogError("The FSMState is null");
                return;
            }
            if (statesList.ContainsKey(state.State_ID))
            {
                Debug.LogError("The state is has alread Exit");
                return;
            }
            state.unit_Gameobject = FatherUnit;
            statesList.Add(state.State_ID, state);
            state._navMeshAgent = _navMeshAgent;
        }

        public void RemoveState(FSMState state)
        {
            if (state == null)
            {
                Debug.LogError("The FSMState is null");
                return;
            }
            if (!statesList.ContainsKey(state.State_ID))
            {
                Debug.LogError("The state is no Exit");
                return;
            }
            statesList.Remove(state.State_ID);
        }

        public void ControlStateTransition(StateID id, object data = null)
        {
            if (id == StateID.NoState || id == CurrentState.State_ID || CurrentState.State_ID == id)//如果id为0不做转换
            {
                return;
            }

            FSMState state;
            statesList.TryGetValue(id, out state);//得到需要转换的状态
            CurrentState.BeforLeaving();
            CurrentState = state;//当前状态转换成需要转换的状态
            CurrentState.BeforEntering(data);
        }

    }
}
