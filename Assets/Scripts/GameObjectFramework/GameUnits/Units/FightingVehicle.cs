using GameEnum;
using GameEvent;
using GameManager;
using Interface;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTS
{
    public class FightingVehicle : GameBattleUnit, IGameUnitBattleFun
    {
        public Transform turret;
        
        public override void SetAttack_Object(GameObject target, float distance)
        {
            base.SetAttack_Object(target, distance);
            attack_Object = target;
            if (!IsFire)
            {
                StopAllCoroutines();
                StartCoroutine(StartToAttack(distance));
            }
        }

        public IEnumerator StartToAttack(float distance)
        {
            IsFire = true;
            while (attack_Object != null && attack_Object.activeSelf && Vector3.Distance(transform.position, attack_Object.transform.position)
                < distance)
            {
                if (attack_Object == null || attack_Object.activeSelf == false)
                {
                    attack_Object = null;
                    yield break;
                }
                turret.LookAt(attack_Object.transform.position);
                yield return new WaitForSeconds(attack_Speed);
                turret.LookAt(attack_Object.transform.position);
                for (int i = 0; i < shoot.Length; ++i)
                {
                    PoolManager.Instance.GetInstance(shell_Type, shoot[i].position, shoot[i].rotation).GetComponent<Shell>().Init(attack_Object, shell_Damage);
                }
            }
            turret.rotation = new Quaternion(0, 0, 0, 0);
            IsFire = false;
        }
        
    }
}
