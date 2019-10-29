using UnityEngine;
using DG.Tweening;
using GameManager;
using GameEnum;

namespace FSM
{
    //采矿状态
    public class MiningState : FSMState
    {
        private Vector3 mine_Position;
        private bool IsPlayerCamp;
        private CampEnum _camp;
        
        public Vector3 MinePosition
        {
            get { return mine_Position; }
            set
            {
                mine_Position = value; IsStart = true;
                UpAndDownMining();
            }
        }
        protected Vector3 CenterPosition;
        bool IsPickedMine;  //是否已采完矿
        bool IsStart;   //是否开始采矿
        public MiningState(Vector3 centerPosition, CampEnum camp, bool isPlayerCamp)
        {
            State_ID = StateID.Mining;
            CenterPosition = centerPosition;
            IsPickedMine = false;
            IsStart = false;
            _camp = camp;
            IsPlayerCamp = isPlayerCamp;
        }

        public override void BeforEntering(object data)
        {
            IsStart = true;
            UpAndDownMining();
        }

        public override void BeforLeaving(object data)
        {
            IsStart = false;
        }

        public void UpAndDownMining()
        {
            if (!IsStart)
                return;
            unit_Gameobject.transform.DOKill();
            if (IsPickedMine)
            {
                unit_Gameobject.transform.DOLookAt(CenterPosition, 1).OnComplete(() =>
                {
                    unit_Gameobject.transform.DOMove(CenterPosition, Vector3.Distance(unit_Gameobject.transform.position, CenterPosition) / 20).OnComplete(
                        () =>
                        {
                            GameDataManager.Instance.ChangeMineCount(500, _camp, IsPlayerCamp);
                            IsPickedMine = false; UpAndDownMining();
                        });
                });
            }
            else
            {
                unit_Gameobject.transform.DOLookAt(mine_Position, 0.1f).OnComplete(() =>
                {
                    unit_Gameobject.transform.DOMove(mine_Position, Vector3.Distance(unit_Gameobject.transform.position, mine_Position) / 20).OnComplete(
                        () =>
                        {
                            IsPickedMine = true; UpAndDownMining();
                        });
                });
            }
        }

    }
}