using GameData;
using GameEnum;
using System.Collections.Generic;

namespace GameManager
{
    public class GameDataManager : Singleton<GameDataManager>
    {
        public GameCampData AllianceCampData;
        public GameCampData EmpireCampData;
        public GameCampData playerCampData { get; private set; }

        public DelegateGroup.MineNumChange mineNumChange;
        public DelegateGroup.Production_Judgment Production_Judgment;

        public Dictionary<string, int> ConstructUnitList = new Dictionary<string, int>();

        public override void Init()
        {
            base.Init();
            AllianceCampData = new GameCampData();
            EmpireCampData = new GameCampData();
            if (GameMapManager.Instance.PlayerCamp == CampEnum.Alliance)
                playerCampData = AllianceCampData;
            else
                playerCampData = EmpireCampData;
        }

        public void ChangeCampScore(int value, CampEnum campEnum)
        {
            if (campEnum == CampEnum.Alliance)
                AllianceCampData.Camp_Score += value;
            else
                EmpireCampData.Camp_Score += value;

        }

        public void ChangeGoldCount(int value, CampEnum campEnum, bool IsPlayerCamp)
        {
            if (campEnum == CampEnum.Alliance)
                AllianceCampData.Gold_Count += value;
            else
                EmpireCampData.Gold_Count += value;

        }

        public void ChangeMineCount(int value, CampEnum campEnum, bool IsPlayerCamp)
        {
            if (campEnum == CampEnum.Alliance)
                AllianceCampData.Mine_Count += value;
            else
                EmpireCampData.Mine_Count += value;
            if (IsPlayerCamp)
                mineNumChange(playerCampData.Mine_Count);
            else
                Production_Judgment();

        }



    }
}
