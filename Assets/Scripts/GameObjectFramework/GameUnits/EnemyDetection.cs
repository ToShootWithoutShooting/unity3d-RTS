using System.Collections.Generic;
using UnityEngine;
using GameManager;
using GameEnum;
using Interface;

/// <summary>
/// 探测敌人
/// </summary>
public class EnemyDetection : MonoBehaviour
{

    public int maxDetectable_Range;
    public CollisionDetectionType[] _attackPriority;
    public GameObject detection_Target;
    private float radius;
    private float enemy_Radius;
    public GameObject PlayerSelectEnemy;
    public float selectEnemy_Radius;
    IGameUnitBattleFun _gameUnitBattleFun;
    bool IsStartDetecting;


    public virtual void Init(GameMapManager.Attack_Data attack_Data, float radius, IGameUnitBattleFun gameUnitBattleFun)
    {
        maxDetectable_Range = attack_Data.maxDetectable_Range;
        string[] attack_Priority = attack_Data.attack_Priority.Split(',');
        _attackPriority = new CollisionDetectionType[attack_Priority.Length];
        for (int i = 0; i < attack_Priority.Length; ++i)
        {
            _attackPriority[i] = EnumChange<CollisionDetectionType>.StringToEnum(attack_Priority[i]);
        }
        this.radius = radius;
        _gameUnitBattleFun = gameUnitBattleFun;
    }


    public void SetPlayerSelectEnemy(GameObject gameObject)
    {
        PlayerSelectEnemy = gameObject;
        selectEnemy_Radius = PlayerSelectEnemy.GetComponent<IGameUnitDataFun>().GetUnitRaduis();
        IsStartDetecting = true;
    }


    private void Update()
    {
        if (PlayerSelectEnemy != null && PlayerSelectEnemy.activeSelf == false)
            PlayerSelectEnemy = null;
        if (PlayerSelectEnemy != null && PlayerSelectEnemy.activeSelf && Vector3.Distance(transform.position, PlayerSelectEnemy.transform.position)
            <= (maxDetectable_Range + selectEnemy_Radius + radius))
        {
            if (IsStartDetecting)
            {
                _gameUnitBattleFun.SetAttack_Object(PlayerSelectEnemy, maxDetectable_Range + selectEnemy_Radius + radius);
                IsStartDetecting = false;
                detection_Target = null;
            }
            return;
        }
        if (detection_Target == null || !detection_Target.activeSelf || Vector3.Distance(transform.position, detection_Target.transform.position)
        > (maxDetectable_Range + enemy_Radius + radius))
        {
            for (int i = 0; i < _attackPriority.Length; ++i)
            {

                detection_Target = GameObjectsManager.Instance.GetCollisionUnit(transform.position, maxDetectable_Range + radius, _attackPriority[i]);
                if (detection_Target != null)
                {
                    enemy_Radius = detection_Target.GetComponent<IGameUnitDataFun>().GetUnitRaduis();
                    _gameUnitBattleFun.SetAttack_Object(detection_Target, maxDetectable_Range + enemy_Radius + radius);
                    IsStartDetecting = true;
                    return;
                }
            }
        }
        if (detection_Target == null && PlayerSelectEnemy == null)
            _gameUnitBattleFun.NoEnemyDetected();


    }
    

}
