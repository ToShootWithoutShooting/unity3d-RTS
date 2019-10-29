using UnityEngine;

public class GLDraw : MonoBehaviour

{



    public static bool isShowGridLine = true;

    Material lineMaterial;

    private void Awake()

    {

        if (!lineMaterial)

        {

            //lineMaterial = new Material(Shader.Find("Particles/Alpha Blended"));

            //lineMaterial.hideFlags = HideFlags.HideAndDontSave;

            //lineMaterial.shader.hideFlags = HideFlags.HideAndDontSave;

        }

    }

}

