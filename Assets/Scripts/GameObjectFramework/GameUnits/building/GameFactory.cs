using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameEnum;
using Interface;
using GameEvent;
using GameManager.UI;

namespace RTS
{
    /// <summary> 
    /// 联盟建筑基类
    /// </summary>
    public class GameFactory : GameUnitsBase, IProductingUnits
    {
        private Transform ProductPoint;
        private const int Gross = 5;
        private int productionTime = 0;  //生产单位总剩余时间
        private const int unitProductionTime = 40;  //生产一个单位所需时间
        private Queue<GameUnitName> productionQueue;  //正在生产队列

        private UnitUIData unitUIData;


        #region 初始化
        protected override void Init()
        {
            base.Init();
            ProductPoint = transform.Find("ProductPoint");
            productionQueue = new Queue<GameUnitName>();

        }
        public int GetProductionTime()
        {
            return productionTime;
        }
        public Queue<GameUnitName> GetProductionQueue()
        {
            return productionQueue;
        }

        public int GetRemainingCount()
        {
            return Gross - productionQueue.Count;
        }
        #endregion


        #region 生产单位
        public virtual void ProductUnits(string name)
        {
            GameUnitName gameUnitName = (GameUnitName)System.Enum.Parse(typeof(GameUnitName), name);
            if (productionQueue.Count >= Gross)
                return;
            productionTime += unitProductionTime;
            productionQueue.Enqueue(gameUnitName);
            if (productionQueue.Count == 1)
                StartCoroutine(ProductingUnits());
        }

        IEnumerator ProductingUnits()
        {
            EventManager.Instance.Fire(UIEventEnum.StartProducting, gameObject, new StartProductEvent(productionQueue.ToArray()[0].ToString(), 40));
            while (productionTime >= 0)
            {
                yield return new WaitForSeconds(0.1f);
                productionTime -= 1;
                if (productionTime % 40 == 0)
                {
                    if (IsPlayerCamp)
                        EventManager.Instance.Fire(UIEventEnum.CompletedProucting, gameObject, new StartProductEvent(productionQueue.ToArray()[0].ToString(), 0));
                    float x = Random.Range(20f, -20f);
                    float z = Random.Range(5f, -5f);
                    Vector3 pos = new Vector3(ProductPoint.position.x + x, ProductPoint.position.y, ProductPoint.position.z + z);
                    GameObject unit = PoolManager.Instance.GetInstance(productionQueue.Dequeue().ToString(), pos, ProductPoint.rotation);
                    
                    if (productionQueue.Count >= 1 && IsPlayerCamp)
                        EventManager.Instance.Fire(UIEventEnum.StartProducting, gameObject, new StartProductEvent(productionQueue.ToArray()[0].ToString(), 40));
                }
            }
            productionTime = 0;
            yield break;
        }

        protected override void OnDispose()
        {
            base.OnDispose();
            GameObject explosion;
            if (Object_HP <= 10)
                explosion = PoolManager.Instance.GetInstance("BuildingExplosion", transform.position, transform.rotation);

        }   
            
        #endregion

    }
}