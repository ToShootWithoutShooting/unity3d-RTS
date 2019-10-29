using GameEnum;
using GameEvent;
using GameManager;
using Interface;
using System.Collections;
using UnityEngine;

namespace RTS
{
    /// <summary>
    /// 联盟士兵脚本
    /// </summary>
    public class Solider : GameBattleUnit, IGameUnitBattleFun
    {
        private Animation _animation;
        public string Idle_Animation;
        public string Move_Animation;
        public string Fire_Animation;
        private float _distance;

        
        protected override void Init()
        {
            base.Init();
            _animation = GetComponent<Animation>();
            _animation.Play(Idle_Animation);
            
        }
        

        public override void SetAttack_Object(GameObject target, float distance)
        {
            base.SetAttack_Object(target, distance);
            attack_Object = target;
            _distance = distance;
            if (!IsFire && !IsMove)
            {
                StopAllCoroutines();
                StartCoroutine(StartToAttack(distance));
            }

        }

        public override void DisposeAfterMove()
        {
            base.DisposeAfterMove();
            if (!isMove)
                _animation.Play(Idle_Animation);
        }

        public override void MoveToTarget(Vector3 targetPos, bool IsEnemy = false, float distance = 0, float stoppingDistance = 0)
        {
            base.MoveToTarget(targetPos, IsEnemy, distance, stoppingDistance);
            StopAllCoroutines();
            StartCoroutine(Move(MovePos));

        }   
            
           
        public IEnumerator Move(Vector3 targetPos)
        {
            _animation.Play(Move_Animation);
            IsFire = false;
            IsMove = true;
            float distance = navMeshAgent.stoppingDistance;
            if (distance == 0)
                distance += 3;
            while (Vector3.Distance(transform.position, targetPos) >= distance)
            {
                yield return new WaitForSeconds(0.1f);
            }
            navMeshAgent.enabled = false;
            _animation.Play(Idle_Animation);
            IsMove = false;
            if(attack_Object)
            {
                StopAllCoroutines();
                StartCoroutine(StartToAttack(_distance));
            }
        }

        public IEnumerator StartToAttack(float distance)
        {
            IsFire = true;
            _animation.Play(Fire_Animation);
            while (attack_Object != null && attack_Object.activeSelf && Vector3.Distance(transform.position, attack_Object.transform.position)
                < distance)
            {
                transform.LookAt(attack_Object.transform.position);
                yield return new WaitForSeconds(attack_Speed);
                for (int i = 0; i < shoot.Length; ++i)
                {
                    PoolManager.Instance.GetInstance(shell_Type, shoot[i].position, shoot[i].rotation).GetComponent<Shell>().Init(attack_Object, shell_Damage);
                }
                transform.LookAt(attack_Object.transform.position);
            }
            IsFire = false;
            _animation.Play(Idle_Animation);
        }

    }
}
