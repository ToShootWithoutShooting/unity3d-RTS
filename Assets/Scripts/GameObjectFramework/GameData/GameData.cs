using GameManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameData
{
    public class GameCampData 
    {
        public int Camp_Score;
        public int Gold_Count;
        public int Mine_Count;

        public GameCampData()
        {
            Camp_Score = 0;
            Gold_Count = 1000;
            Mine_Count = 1000;
        }


    }
}
