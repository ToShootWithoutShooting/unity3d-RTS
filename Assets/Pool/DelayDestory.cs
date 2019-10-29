using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayDestory : MonoBehaviour {

    public float delayTime;

    private void OnEnable()
    {
        Invoke("DelayDisable", delayTime);
    }

    private void DelayDisable()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

}
