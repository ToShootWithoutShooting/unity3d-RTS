using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBuildColliderDetector : MonoBehaviour {

    public GameObject FatherNode { get; set; }

    private void Start()
    {
        gameObject.AddComponent<HighlightableObject>().ConstantOn(Color.red);
    }

    // Update is called once per frame
    void Update () {
        transform.position = FatherNode.transform.position;
	}
}
