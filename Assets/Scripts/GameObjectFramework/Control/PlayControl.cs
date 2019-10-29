using UnityEngine;
using GameEnum;
using GameEvent;
using GameManager;
using GameManager.UI;

namespace GameControl
{
    /// <summary>
    /// 玩家相关操作
    /// </summary>
    public class PlayControl : MonoBehaviour
    {
        private GameEventArgs gameEventArgs;

        private void Awake()
        {
            UIManager.Instance.LoadUIPanel(UIPanelEnum.MainUI);
            GameMapManager.Instance.Init_MainScene();
        }

        void Update()
        {
            //按下鼠标左键
            if (Input.GetMouseButtonDown(0))
            {
                //EventManager.Instance.Fire(GameEventEnum.AI_AttackOrder);
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit = new RaycastHit();
                int layMask = 1 << 12;
                layMask = ~layMask;   //将layMask按位求补，检测除第10层外的碰撞
                if (Physics.Raycast(ray, out hit, 9999, layMask))
                {
                    CrowdBehaviorManager.Instance.ClearCrowdArray();
                    gameEventArgs = new MouseClickEventArgs(hit.collider.gameObject, hit.point);
                    EventManager.Instance.Fire(GameEventEnum.PlayerPressLeftMouseEvent, gameObject, gameEventArgs);
                }

            }
            //按下鼠标右键
            else if (Input.GetMouseButtonDown(1))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit = new RaycastHit();
                int layMask = 1 << 12;
                layMask = ~layMask;   //将layMask按位求补，检测除第10层外的碰撞
                if (Physics.Raycast(ray, out hit, 9999, layMask))
                {
                    if (hit.collider.gameObject.layer == 11 && CrowdBehaviorManager.Instance.CrowdArray.Count > 0)
                    {
                        Debug.Log(CrowdBehaviorManager.Instance.CrowdArray.Count);
                        CrowdBehaviorManager.Instance.Set_FormationPos(hit.point);
                        PoolManager.Instance.GetInstance("ClickEffect", hit.point, new Quaternion(0, 0, 0, 0));
                    }

                    gameEventArgs = new MouseClickEventArgs(hit.collider.gameObject, hit.point);
                    EventManager.Instance.Fire(GameEventEnum.PlayerPressRightMouseEvent, gameObject, gameEventArgs);
                }
            }


        }

    }
}