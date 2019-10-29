
using System.Collections;

namespace Interface
{
    public interface IGameUnitBattleFun
    {
        //IEnumerator StartToAttack(, float distance);

        void SetAttack_Object(UnityEngine.GameObject target, float distance);

        void NoEnemyDetected();

    }
}
