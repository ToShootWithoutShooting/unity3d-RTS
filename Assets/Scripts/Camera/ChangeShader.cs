using UnityEngine;
using System.Collections;

public class ChangeShader : MonoBehaviour
{

    private Material material;
    private Shader originShader;
    public Shader rimShader;

    void Start()
    {
        material = GetComponentInChildren<SkinnedMeshRenderer>().material;
        originShader = material.shader;
    }

    public void Select()
    {
        material.shader = rimShader;
    }

    public void DisSelect()
    {
        material.shader = originShader;
    }
}