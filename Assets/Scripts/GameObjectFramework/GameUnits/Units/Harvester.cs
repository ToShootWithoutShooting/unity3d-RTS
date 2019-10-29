using GameEvent;
using Interface;
using UnityEngine;
using FSM;
using GameManager;
using System.Collections;
using UnityEngine.AI;
using RTS.AI;
using DG.Tweening;

namespace RTS
{
    public enum HarvesterState
    {
        idle,
        mine,
        build
    }
    public class Harvester : GameUnitsBase, IHarvester
    {
        private FSMSystemManager _FSMSystemManager;
        private MiningState miningState;
        private IdleState idleState;
        private MoveState moveState;
        public bool IsOnBuilding = false; 
        NavMeshAgent _navMeshAgent;

        public HarvesterState harvesterState;
        private Vector3 minePos;
        private Vector3 CenterPos;

        private string productint_Unit;

        private GameObject FactoryBuild;
        GameObject UIBuildColliderDetector;
        GameObject Building;

        public int Cost;
        #region 初始化
        void Start()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            InitFSM();
            if (!IsPlayerCamp)
            {

                int index = Random.Range(0, GameMapManager.Instance.AICenter.GetComponent<CommandCenterAI>().MinePos.Length);
                minePos = GameMapManager.Instance.AICenter.GetComponent<CommandCenterAI>().MinePos[index];
                Mining_AI();
            }
        }

        void InitFSM()
        {
            _FSMSystemManager = new FSMSystemManager(gameObject, _navMeshAgent);
            if (IsPlayerCamp)
                CenterPos = GameMapManager.Instance.PlayerCenter.transform.position;
            else
                CenterPos = GameMapManager.Instance.AICenter.transform.position;
            miningState = new MiningState(CenterPos, Object_Camp, IsPlayerCamp); ;
            idleState = new IdleState();
            moveState = new MoveState();
            _FSMSystemManager.AddState(miningState);
            _FSMSystemManager.AddState(idleState);
            _FSMSystemManager.AddState(moveState);
            _FSMSystemManager.CurrentState = idleState;
            harvesterState = HarvesterState.idle;
        }

        protected override void OnActivate()
        {
            base.OnActivate();
            AddToGameObjectManager();
            harvesterState = HarvesterState.idle;
            //_FSMSystemManager.ControlStateTransition(StateID.Idle);
            IsOnBuilding = false;
            StopAllCoroutines();

        }   
            
        public override void SetSelectEffect()
        {
            base.SetSelectEffect();
            if (isSelected)
                CrowdBehaviorManager.Instance.AddToCrowdArray(Object_UnitName, Object_Radius);
        }

        #endregion

        #region 玩家控制单位身体代码
        public override void ClickRightMouseEvent(object sender, GameEventArgs e)
        {
            base.ClickRightMouseEvent(sender, e);
            if (!IsSelected || IsOnBuilding)
                return;
            MouseClickEventArgs m = e as MouseClickEventArgs;
            if (m.dataGameobject.layer == 11)
            {
                _navMeshAgent.enabled = false;
                _FSMSystemManager.ControlStateTransition(StateID.Move);
                Vector3 pos = CrowdBehaviorManager.Instance.GetBattle_FormationPos();
                Debug.Log(pos);
                moveState.SetMoveTargetPos(m.ClickPosition);
            }
            else if (m.dataGameobject.tag == "Mine")
            {
                _navMeshAgent.enabled = false;
                miningState.MinePosition = m.dataGameobject.transform.position;
                _FSMSystemManager.ControlStateTransition(StateID.Mining);
                harvesterState = HarvesterState.mine;
            }   
        }
        #endregion

        #region AI
        
        public void Mining_AI()
        {
            _navMeshAgent.enabled = false;
            miningState.MinePosition = minePos;
            _FSMSystemManager.ControlStateTransition(StateID.Mining);
            harvesterState = HarvesterState.mine;
        }

        public void ProductBuilding_AI(string name, Vector3 pos)
        {
            productint_Unit = name;
            GameObject building = PoolManager.Instance.GetInstance(Object_Camp.ToString() + name,
                pos, new Quaternion(0, 180, 0, 0));
            building.SetActive(false);
            harvesterState = HarvesterState.build;
            if (_FSMSystemManager.CurrentState.State_ID != StateID.Move)
                _FSMSystemManager.ControlStateTransition(StateID.Move);
            moveState.SetMoveTargetPos(pos, 0, () =>
            {
                StartCoroutine(MoveToBuilding(building));
            });

        }   
        
        #endregion
            
        #region 生产建筑
        public void ProductBuilding(string name,int cost)
        {
            Cost = cost;
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.y = 0;
            Building = PoolManager.Instance.GetInstance(name, pos, new Quaternion(0, 0, 0, 0));
            StartCoroutine(ProductingBuilding(Building));

        }


        IEnumerator ProductingBuilding(GameObject building)
        {
            building.GetComponent<IPlayerCampUnitsBase>().SetSelected(true);
            float z = Camera.main.WorldToScreenPoint(building.transform.position).z;   //先获得世界坐标转屏幕坐标的z值
            GameMapManager.Instance.IsCanBuilding = true;
            building.GetComponent<HighlightableObject>().ConstantOn(Color.red);
            UIBuildColliderDetector = PoolManager.Instance.GetInstance("UIBuildColliderDetector", 
                building.transform.position, new Quaternion(0, 0, 0, 0));
            
            UIBuildColliderDetector.GetComponent<UIBuildColliderDetector>().FatherNode = building;
            while (true)
            {
                Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, z));
                pos.y = 0;
                building.transform.position = pos;
                Vector3 buildPos;
                if (Input.GetMouseButtonDown(0))
                {
                    yield return new WaitForSeconds(0.1f);
                    if (GameMapManager.Instance.IsCanBuilding)
                    {
                        UIBuildColliderDetector.SetActive(false);
                        buildPos = building.transform.position;
                        building.GetComponent<IPlayerCampUnitsBase>().SetSelected(false);
                        building.SetActive(false);
                        GameDataManager.Instance.ChangeMineCount(-Cost, Object_Camp, IsPlayerCamp);
                        if (_FSMSystemManager.CurrentState.State_ID != StateID.Move)
                            _FSMSystemManager.ControlStateTransition(StateID.Move);
                        harvesterState = HarvesterState.build;
                        moveState.SetMoveTargetPos(buildPos, 0, () =>
                        {
                            StartCoroutine(MoveToBuilding(building));
                        });
                        yield break;
                    }
                    else
                    {
                        GameMapManager.Instance.IsCanBuilding = true;
                    }
                }
                else if (Input.GetMouseButtonDown(1))
                {
                    UIBuildColliderDetector.SetActive(false);
                    GameMapManager.Instance.IsCanBuilding = true;
                    building.GetComponent<IPlayerCampUnitsBase>().SetSelected(false);
                    building.SetActive(false);
                    yield break;
                }
                yield return null;
            }
        }

        IEnumerator MoveToBuilding(GameObject building)
        {

            IsOnBuilding = true;
            FactoryBuild = PoolManager.Instance.GetInstance("FactoryBuild", 
                building.transform.position, building.transform.rotation);
            yield return new WaitForSeconds(5f);
            FactoryBuild.SetActive(false);
            building.SetActive(true);
            building.GetComponent<Collider>().enabled = true;
            if (null != building.GetComponent<IGameUnitDataFun>())
                building.GetComponent<IGameUnitDataFun>().AddToGameObjectManager();
            moveState.SetMoveTargetPos(new Vector3(transform.position.x + 60, transform.position.y, transform.position.z));

            IsOnBuilding = false;
            yield return new WaitForSeconds(3.5f);
            building.GetComponent<NavMeshObstacle>().enabled = true;
            if (!IsPlayerCamp)
            {
                Mining_AI();
                AISystemManager.Instance.Producting_Units.Remove(productint_Unit);
            }
        }

        #endregion

        private void Update()
        {
            _FSMSystemManager.CurrentState.DoUpdate();
        }

        protected override void OnDispose()
        {
            base.OnDispose();
            StopAllCoroutines();
            transform.DOKill();
            if (FactoryBuild != null)
                FactoryBuild.SetActive(false);
            if(UIBuildColliderDetector!=null)
                UIBuildColliderDetector.SetActive(false);
            if(Building!=null)
                Building.SetActive(false);
        }

    }

}
