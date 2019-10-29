using GameEnum;
namespace Interface
{
    /// <summary>
    /// AllianceBase使用的接口
    /// </summary>
    public interface IGameUnitDataFun
    {
        void SetGameUnitHP(float Damage);

        void AddToGameObjectManager();

        void RemoveToGameObjectManager();

        CampEnum GetUnitCamp();

        UnitTypeEnum GetGameUnitType();

        float GetUnitRaduis();

        bool GetIsPlayerCamp();
    }
}
