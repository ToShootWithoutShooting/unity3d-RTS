using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactiveAtferTime : MonoBehaviour
{
    public float delayTime = 3;

    private void OnEnable()
    {
        Invoke("Deactive", delayTime);//delayTime秒后禁用
    }

    void Deactive()
    {
        gameObject.SetActive(false);
    }


}
