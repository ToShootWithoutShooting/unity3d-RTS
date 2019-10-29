using GameEnum;
using GameEvent;
using UnityEngine;
using UnityEngine.AI;
using GameManager;
using Interface;

namespace RTS
{
    public abstract class GameBattleUnit : GameUnitsBase, IGameUnitBattleFun
    {
        protected NavMeshAgent navMeshAgent;
        protected int EnemyLayer;
        protected int StopToAttackDistance;
        protected GameMapManager.Attack_Data attack_Data;
        protected string shell_Type;
        public float shell_Damage;
        public float attack_Speed;
        protected EnemyDetection enemyDetection;
        public GameObject attack_Object;
        public bool IsFire = false;
        public Transform[] shoot;
        public bool IsFindEnemy;
        public bool IsAttack = false;
        public bool isMove;
        public bool IsMove { get { return isMove; } set { isMove = value; DisposeAfterMove(); } }
        public Vector3 MovePos;

        public virtual void DisposeAfterMove() { }

        protected override void Init()
        {
            base.Init();
            navMeshAgent = GetComponent<NavMeshAgent>();
            if (Object_Camp == CampEnum.Alliance)
                EnemyLayer = 9;
            else
                EnemyLayer = 8;
            attack_Data = GameMapManager.Instance.GetAttackData(Object_UnitName.ToString());
            StopToAttackDistance = attack_Data.maxDetectable_Range;
            shell_Type = attack_Data.shell_Type;
            shell_Damage = attack_Data.shell_Damage;
            attack_Speed = attack_Data.attack_Speed;
            if (transform.Find("Detector") != null)
            {
                enemyDetection = transform.Find("Detector").GetComponent<EnemyDetection>();
                enemyDetection.Init(attack_Data, Object_Radius, GetComponent<IGameUnitBattleFun>());
            }
            
        }

        #region AI

        public Vector3 targetPos;

        protected override void OnActivate()
        {
            base.OnActivate();
            if (!IsPlayerCamp)
                EventManager.Instance.Fire(GameEventEnum.AI_BattleUnitToGenerate);
            AddToGameObjectManager();
        }

        protected override void InitAI()
        {
            base.InitAI();
            if (!EventManager.Instance.Check(GameEventEnum.AI_AttackOrder, AI_AttackByOrder))
            {
                EventManager.Instance.Subscribe(GameEventEnum.AI_AttackOrder, AI_AttackByOrder);
            }
            IsAttack = false;
        }


        void AI_AttackByOrder(object sender, GameEventArgs e)
        {
            IsAttack = true;
            targetPos = GameMapManager.Instance.PlayerCenter.transform.position;
            if (IsFindEnemy)
                return;
            float distance = Vector3.Distance(gameObject.transform.position, GameMapManager.Instance.PlayerCenter.transform.position);
            if (distance > StopToAttackDistance - 20)
            {
                MoveToTarget(GameMapManager.Instance.PlayerCenter.transform.position, true, distance, StopToAttackDistance);
                navMeshAgent.stoppingDistance = StopToAttackDistance - 20;
            }
            else
                navMeshAgent.enabled = false;

        }
        
        #endregion
        
        public virtual void SetAttack_Object(GameObject target, float distance)
        {
            if (target == attack_Object)
                return;
            if (Is_AI)
            {
                navMeshAgent.enabled = false;
                IsFire = false;
                IsMove = false;
            }
            IsFindEnemy = true;
        }

        public virtual void NoEnemyDetected()
        {
            if (!IsFindEnemy)
                return;
            if (Is_AI && IsAttack)
            {
                float distance = Vector3.Distance(gameObject.transform.position, targetPos);
                if (distance > StopToAttackDistance - 20)
                {
                    MoveToTarget(targetPos, true, distance, StopToAttackDistance);
                    navMeshAgent.stoppingDistance = StopToAttackDistance - 20;
                }
                else
                    navMeshAgent.enabled = false;
            }
            IsFindEnemy = false;
        }


        public override void SetSelectEffect()
        {
            base.SetSelectEffect();
            if (isSelected)
                CrowdBehaviorManager.Instance.AddToCrowdArray(Object_UnitName, Object_Radius);
        }

        #region 控制单位身体代码
        public override void ClickRightMouseEvent(object sender, GameEventArgs e)
        {
            if (!IsSelected || !IsPlayerCamp)
                return;
            MouseClickEventArgs m = e as MouseClickEventArgs;
            if (m.dataGameobject.layer == 11)
            {
                MoveToTarget(m.ClickPosition);
            }
            else if (m.dataGameobject.layer == EnemyLayer)
            {
                enemyDetection.SetPlayerSelectEnemy(m.dataGameobject);
                float distance = Vector3.Distance(gameObject.transform.position, m.dataGameobject.transform.position);
                if (distance > StopToAttackDistance)
                    MoveToTarget(m.dataGameobject.transform.position, true, distance, StopToAttackDistance);
                else
                {
                    navMeshAgent.enabled = false;
                    IsMove = false;
                }
            }
        }

        #endregion
        
        public virtual void MoveToTarget(Vector3 targetPos, bool IsEnemy = false, float distance = 0, float stoppingDistance = 0)
        {
            MovePos = targetPos;
            navMeshAgent.enabled = false;
            navMeshAgent.enabled = true;
            if (IsEnemy)
            {
                stoppingDistance += (distance - stoppingDistance) * 0.2f;
            }
            if (!IsEnemy && IsPlayerCamp && Object_UnitType != UnitTypeEnum.AirShip)
            {
                MovePos = CrowdBehaviorManager.Instance.GetBattle_FormationPos();
            }
            navMeshAgent.enabled = true;
            navMeshAgent.SetDestination(MovePos);
            navMeshAgent.stoppingDistance = stoppingDistance;
            
        }   
            
        protected override void UnBindEvent()
        {
            base.UnBindEvent();
            if (EventManager.Instance.Check(GameEventEnum.AI_AttackOrder, AI_AttackByOrder))
            {
                EventManager.Instance.UnSubscribe(GameEventEnum.AI_AttackOrder, AI_AttackByOrder);
            }
        }

        protected override void OnDispose()
        {
            IsMove = false;
            IsFindEnemy = false;
            IsFire = false;
            IsAttack = false;
            attack_Object = null;
            base.OnDispose();
            if (Is_AI)
            {
                AISystemManager.Instance.ReduceUnitCount(Object_UnitType.ToString());
            }
        }
        

    }
}