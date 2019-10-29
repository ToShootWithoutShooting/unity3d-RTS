using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using GameEnum;
using System;

namespace GameManager
{
    /// <summary>
    /// 集群行为管理
    /// </summary>
    public class CrowdBehaviorManager : Singleton<CrowdBehaviorManager>
    {
        public List<GameUnitName> CrowdArray;
        List<float> CrowdArrayRadius;
        List<Vector3> targetPosArray;
        const float radius = 30;

        public override void Init()
        {
            base.Init();
            CrowdArray = new List<GameUnitName>();
            CrowdArrayRadius = new List<float>();
            targetPosArray = new List<Vector3>();
        }

        public void AddToCrowdArray(GameUnitName gameUnitName, float radius)
        {
            for (int i = 0; i < CrowdArray.Count; ++i)
            {
                if (CrowdArray[i] == gameUnitName)
                {
                    CrowdArray.Insert(i, gameUnitName);
                    CrowdArrayRadius.Insert(i, radius);
                    return;
                }
            }
            CrowdArray.Add(gameUnitName);
            CrowdArrayRadius.Add(radius);

        }

        public void Set_FormationPos(Vector3 targetPos)
        {
            int count = CrowdArray.Count;
            if (count == 0)
                return;
            double line_d = Math.Sqrt(count);
            int line = Convert.ToInt32(line_d);
            if (line_d > line)
                line += 1;
            Vector3 FirstPos;
            int n = (line - 1) / 2;
            FirstPos = new Vector3(targetPos.x + n * radius, targetPos.y, targetPos.z + n * radius);

            for (int i = 0; i < count; i++)
            {
                int x = i % line;
                int y = i / line;
                Vector3 pos = new Vector3(FirstPos.x - x * radius, FirstPos.y, FirstPos.z - y * radius);
                targetPosArray.Add(pos);

            }
            
        }

        public Vector3 GetBattle_FormationPos()
        {
            Vector3 pos = targetPosArray[0];
            targetPosArray.RemoveAt(0);
            return pos;
        }

        public void ClearCrowdArray()
        {
            CrowdArray.Clear();
            CrowdArrayRadius.Clear();
            targetPosArray.Clear();
        }


    }
}
