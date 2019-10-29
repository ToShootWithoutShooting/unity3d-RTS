using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Experimental.UIElements.StyleSheets;

public class TrollDrawLine : MonoBehaviour//选中的对象显示圆圈
{

    public int pointCount = 10;
    public float radius = 10f;
    private float angle;
    private List<Vector3> points = new List<Vector3>();
    private new LineRenderer renderer;
    public bool rendering = false;  //用于标识是否显示  
    // Use this for initialization  
    void Start()
    {
        angle = 360f / pointCount;
        renderer = GetComponent<LineRenderer>();
        if (!renderer)
        {
            Debug.LogError("LineRender is NULL!");
        }
    }

    void CalculationPoints()
    {
        Vector3 v = transform.position + transform.forward * radius;
        points.Add(v);
        Quaternion r = transform.rotation;
        for (int i = 1; i < pointCount; i++)
        {
            Quaternion q = Quaternion.Euler(r.eulerAngles.x, r.eulerAngles.y - (angle * i), r.eulerAngles.z);
            v = transform.position + (q * Vector3.forward) * radius;
            v.y += 2;
            points.Add(v);
        }
    }
    void DrowPoints()
    {
        for (int i = 0; i < points.Count; i++)
        {
            renderer.SetPosition(i, points[i]);  //把所有点添加到positions里  
        }
        if (points.Count > 0)   //这里要说明一下，因为圆是闭合的曲线，最后的终点也就是起点，  
            renderer.SetPosition(pointCount, points[0]);
    }
    void ClearPoints()
    {
        points.Clear();  ///清除所有点  
    }
    // Update is called once per frame  
    void Update()
    {       
        if (rendering)
        {
            renderer.positionCount = pointCount + 1;  ///这里是设置圆的点数，加1是因为加了一个终点（起点）  
            CalculationPoints();
            DrowPoints();
        }
        else
        {
            renderer.positionCount = 0;//不显示时设置<span style="font-size:9pt;line-height:1.8em;">圆的点数为0</span><br data-filtered="filtered">  
        }
        ClearPoints();
    }
}