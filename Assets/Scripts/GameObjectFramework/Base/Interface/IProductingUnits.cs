using GameEnum;
using System.Collections.Generic;

namespace Interface
{
    public interface IProductingUnits
    {
        //void ProductBuilding(string name);

        void ProductUnits(string name);

        int GetProductionTime();

        int GetRemainingCount();

        Queue<GameUnitName> GetProductionQueue();
    }
}
