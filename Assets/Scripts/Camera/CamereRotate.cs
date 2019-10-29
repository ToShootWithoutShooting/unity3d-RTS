using GameEnum;
using GameManager.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamereRotate : MonoBehaviour {

    public Transform rotateCenter;

    private void Awake()
    {
        UIManager.Instance.LoadUIPanel(UIPanelEnum.MapSettingUI);
    }

    // Use this for initialization
    void Start () {
        

	}   
	    
	// Update is called once per frame
	void Update () {

        transform.RotateAround(rotateCenter.transform.position, Vector3.up, 0.3f);
        if (transform.eulerAngles.y > 270)
        {
            transform.eulerAngles = new Vector3(15, 180, 0);
            transform.position = new Vector3(1350, 52, 500);
        }


    }
}
