using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using RTS.AI;
using GameManager;
using GameManager.UI;
using GameEnum;

namespace RTS
{
    public class CommandCenter : GameFactory
    {
        protected override void Init()
        {
            base.Init();
            AddToGameObjectManager();
            if (!IsPlayerCamp)
            {
                gameObject.AddComponent<CommandCenterAI>();
            }

            GameObject obj = PoolManager.Instance.GetInstance("AllianceHarvester", transform.position, transform.rotation);
            obj.SetActive(false);
        }

        protected override void OnDispose()
        {
            base.OnDispose();
            GameMapManager.Instance.GameOver(IsPlayerCamp);

        }

    }

    

}