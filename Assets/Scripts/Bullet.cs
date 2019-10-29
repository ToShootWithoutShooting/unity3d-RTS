using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public GameObject aim;
    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (aim != null)
        {
            transform.LookAt(aim.transform.position);           
            if (Vector3.Distance(transform.position, aim.transform.position) <= 5)
            {
                gameObject.SetActive(false);
            }
        }
        transform.Translate(Vector3.forward * 4f);
        if (aim == null||aim.activeSelf==false)
            gameObject.SetActive(false);
    }

}
