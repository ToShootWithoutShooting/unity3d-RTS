using System.Collections;
using UnityEngine;
using GameManager;
using GameEnum;

/// <summary>
/// 游戏场景启动器
/// </summary>
public class GameSceenStarter : MonoBehaviour
{



    private void Awake()
    {
        GameMapManager.Instance.PlayerCamp = CampEnum.Alliance;

    }




}
