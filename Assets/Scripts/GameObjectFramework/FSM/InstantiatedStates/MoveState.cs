using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

namespace FSM
{
    public delegate void ActionCallback();
    public class MoveState : FSMState
    {
        private Vector3 targetPos;
        ActionCallback _actionCallback;
        bool IsArrived = false;
        public MoveState()
        {
            State_ID = StateID.Move;
        }


        public void SetMoveTargetPos(Vector3 TargetPos, float distance = 0, ActionCallback actionCallback = null)
        {
            unit_Gameobject.transform.DOKill();
            _navMeshAgent.enabled = true;
            targetPos = TargetPos;
            _navMeshAgent.SetDestination(TargetPos);
            if (distance == 0)
                distance += 3;
            _navMeshAgent.stoppingDistance = distance;
            _actionCallback = actionCallback;
            IsArrived = false;
        }

        public override void DoUpdate()
        {
            if (Vector3.Distance(unit_Gameobject.transform.position, targetPos) <= _navMeshAgent.stoppingDistance)
            {
                if (IsArrived)
                    return;
                _navMeshAgent.enabled = false;
                if (_actionCallback == null)
                    return;
                _actionCallback();
                IsArrived = true;
            }
        }

    }
}