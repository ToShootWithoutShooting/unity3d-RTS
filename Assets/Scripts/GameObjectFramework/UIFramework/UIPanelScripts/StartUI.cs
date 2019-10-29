using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FairyGUI;
using UnityEngine.SceneManagement;


namespace GameManager.UI
{
    public class StartUI : Window
    {
        private GButton btnStart;
        private GButton btnQuite;

        public StartUI()
        {
            contentPane = UIPackage.CreateObject("UI", "StartUI").asCom;

            
        }

        protected override void OnInit()
        {
            btnStart = contentPane.GetChild("btnStart").asButton;
            btnQuite = contentPane.GetChild("btnQuite").asButton;

            btnStart.onClick.Add(() => { SceneManager.LoadScene("MapSetting"); });
            //btnStart.onClick.Add(() => { SceneManager.LoadScene("test"); });
            btnStart.onClick.Add(() => { Application.Quit(); });

        }


    }
}