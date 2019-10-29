using System.Collections;
using UnityEngine;

namespace RTS
{
    public class TankTurret : MonoBehaviour
    {
        public float delayFireTiem = 2;
        public float damage = 20;
        public Transform shoot;
        public string shellName;

        public void Init(string shellName)
        {
            this.shellName = shellName;
        }

        public void StartFire(GameObject enemy)
        {
            StopFire();
            StartCoroutine(AttackEnemy(enemy));
        }

        public void StopFire()
        {
            StopAllCoroutines();
        }

        public IEnumerator AttackEnemy(GameObject enemy)
        {
            while (enemy != null && enemy.activeSelf != false)
            {
                transform.LookAt(enemy.transform.position);
                yield return new WaitForSeconds(delayFireTiem);
                Fire(enemy);
            }
        }

        protected virtual void Fire(GameObject enemy)
        {
            GameObject shell = PoolManager.Instance.GetInstance(shellName, shoot.transform.position, shoot.transform.rotation);//从对象池中获取shell实例 
            shell.GetComponent<Shell>().Init(enemy,damage);
        }

    }
}
