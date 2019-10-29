using UnityEngine;
using GameManager.UI;
using FairyGUI;
using GameEnum;

namespace GameUI
{
    public class UIRoot : MonoBehaviour
    {
        void Awake()
        {
            UIPackage.AddPackage("UIPanel/UI");  //添加包
            UIObjectFactory.SetPackageItemExtension("ui://UI/ProductListItem", typeof(ProductListItem));
            UIManager.Instance.LoadUIPanel(UIPanelEnum.StartUI);
            //UIManager.Instance.LoadUIPanel(UIPanelEnum.MainUI);
        }


    }
}
