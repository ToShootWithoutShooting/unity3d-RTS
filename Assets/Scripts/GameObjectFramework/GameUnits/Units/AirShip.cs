using Interface;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace RTS
{
    public class AirShip : GameBattleUnit, IGameUnitBattleFun
    {

        private float _distance;


        public override void SetAttack_Object(GameObject target, float distance)
        {
            base.SetAttack_Object(target, distance);
            attack_Object = target;
            _distance = distance;
            if (!IsFire && !IsMove)
            {
                StopAllCoroutines();
                transform.DOKill();
                StartCoroutine(StartToAttack(_distance));
            }

        }


        public override void MoveToTarget(Vector3 targetPos, bool IsEnemy = false, float distance = 0, float stoppingDistance = 0)
        {
            StopAllCoroutines();
            IsMove = true;
            IsFire = false;
            if (IsEnemy)
                stoppingDistance -= 10;
            this.targetPos = targetPos;
            this.targetPos.y = transform.position.y;
            transform.LookAt(this.targetPos);
            transform.DOMove(this.targetPos, Vector3.Distance(transform.position, this.targetPos) / 20f).OnUpdate(() =>
              {
                  transform.LookAt(this.targetPos);
                  if (Vector3.Distance(transform.position, this.targetPos) <= stoppingDistance + 3)
                  {
                      IsMove = false;
                      if (IsEnemy)
                          StartCoroutine(StartToAttack(StopToAttackDistance));
                      else if(attack_Object)
                      {
                          StopAllCoroutines();
                          StartCoroutine(StartToAttack(_distance));
                      }
                      transform.DOKill();
                  }
              });
            
        }


        public IEnumerator StartToAttack(float distance)
        {
            IsFire = true;
            while (attack_Object != null && attack_Object.activeSelf && Vector3.Distance(transform.position, attack_Object.transform.position)
            < (distance + 10))
            {
                transform.LookAt(attack_Object.transform.position);
                yield return new WaitForSeconds(attack_Speed);
                for (int i = 0; i < shoot.Length; ++i)
                {
                    shoot[i].LookAt(attack_Object.transform.position);
                    PoolManager.Instance.GetInstance(shell_Type, shoot[i].position, shoot[i].rotation).GetComponent<Shell>().Init(attack_Object, shell_Damage);
                }
                transform.LookAt(attack_Object.transform.position);
            }
            IsFire = false;

        }
        

    }
}
