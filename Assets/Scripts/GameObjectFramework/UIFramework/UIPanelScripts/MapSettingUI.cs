using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FairyGUI;
using UnityEngine.SceneManagement;
using GameEnum;

namespace GameManager.UI
{
    public class MapSettingUI : Window
    {
        private GList listDifficulty;
        private GButton btnStartGame;
        private GButton btnCancel;

        public MapSettingUI()
        {
            contentPane = UIPackage.CreateObject("UI", "MapSettingUI").asCom;
        }

        protected override void OnInit()
        {
            listDifficulty = contentPane.GetChild("listDifficulty").asList;
            btnStartGame = contentPane.GetChild("btnStartGame").asButton;
            btnCancel = contentPane.GetChild("btnCancel").asButton;

            listDifficulty.selectedIndex = 0;
            listDifficulty.GetChildAt(0).asButton.GetChild("txtName").text = "简单";
            listDifficulty.GetChildAt(1).asButton.GetChild("txtName").text = "正常";
            listDifficulty.GetChildAt(2).asButton.GetChild("txtName").text = "困难";

            listDifficulty.onClickItem.Add(()=> { GameMapManager.Instance.gameDifficulty = (GameDifficulty)listDifficulty.selectedIndex; });
            btnStartGame.onClick.Add(() => { SceneManager.LoadScene("test"); });
            btnCancel.onClick.Add(() => { SceneManager.LoadScene("Start"); });

            
        }

        public override void Show()
        {
            base.Show();
            listDifficulty.selectedIndex = 0;
            btnCancel.selected = false;

        }

    }
}