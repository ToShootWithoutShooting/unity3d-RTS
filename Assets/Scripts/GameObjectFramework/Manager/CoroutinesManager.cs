using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameManager
{
    public class CoroutinesManager : MonoBehaviour
    {

        public void _StartCoroutine(IEnumerator enumerator)
        {
            StopAllCoroutines();
            StartCoroutine(enumerator);
        }

    }
}
