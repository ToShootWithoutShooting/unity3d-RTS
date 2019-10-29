using FairyGUI;
using UnityEngine.SceneManagement;
using GameManager.UI;

public class GameEndUI : Window
{
    private GTextField txtTitle;
    private GButton btnCancel;
    private bool isWin;

    public GameEndUI()
    {
        contentPane = UIPackage.CreateObject("UI", "GameEndUI").asCom;
        Center(true);
    }

    protected override void OnInit()
    {
        txtTitle = contentPane.GetChild("txtTitle").asTextField;
        btnCancel = contentPane.GetChild("btnCancel").asButton;

        btnCancel.onClick.Add(() => { SceneManager.LoadScene("Start"); UIManager.Instance.CloseUI(GameEnum.UIPanelEnum.GameEndUI); });

    }

    public override void Show()
    {
        base.Show();
        isWin = !(bool)data;
        if (isWin)
            txtTitle.text = "游戏胜利";
        else
            txtTitle.text = "游戏失败";
    }


}
