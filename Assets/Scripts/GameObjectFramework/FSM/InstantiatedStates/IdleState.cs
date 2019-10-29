using DG.Tweening;

namespace FSM
{
    public class IdleState : FSMState
    {
        public IdleState()
        {
            State_ID = StateID.Idle;
        }

        public override void BeforEntering(object data = null)
        {
            unit_Gameobject.transform.DOKill();
        }

    }
}
