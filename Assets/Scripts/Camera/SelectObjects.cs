using UnityEngine;
using GameManager;

public class SelectObjects : MonoBehaviour
{
    private Color rectColor = Color.green;
    public float transparency=0.1f;
    private Vector3 start = Vector3.zero;//记下鼠标按下位置

    // private Material rectMat = null;//画线的材质 不设定系统会用当前材质画线 结果不可控
    public Material rectMat = null;//这里使用Sprite下的defaultshader的材质即可

    private bool drawRectangle = false;//是否开始画线标志

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            drawRectangle = true;//如果鼠标左键按下 设置开始画线标志
            start = Input.mousePosition;//记录按下位置
        }
        else if (Input.GetMouseButtonUp(0))
        {
            drawRectangle = false;//如果鼠标左键放开 结束画线
            GameObjectsManager.Instance.ObjectsInRange(start, Input.mousePosition);
        }
    }

    void OnPostRender()
    {//画线这种操作推荐在OnPostRender（）里进行 而不是直接放在Update，所以需要标志来开启
        if (drawRectangle)
        {
            Vector3 end = Input.mousePosition;//鼠标当前位置
            GL.PushMatrix();//保存摄像机变换矩阵,把投影视图矩阵和模型视图矩阵压入堆栈保存
            if (!rectMat)
                return;
            rectMat.SetPass(0);//为渲染激活给定的pass。
            GL.LoadPixelMatrix();//设置用屏幕坐标绘图
            GL.Begin(GL.QUADS);//开始绘制矩形
            GL.Color(new Color(rectColor.r, rectColor.g, rectColor.b, transparency));//设置颜色和透明度，方框内部透明
            //绘制顶点
            GL.Vertex3(start.x, start.y, 0);
            GL.Vertex3(end.x, start.y, 0);
            GL.Vertex3(end.x, end.y, 0);
            GL.Vertex3(start.x, end.y, 0);
            GL.End();
            GL.Begin(GL.LINES);//开始绘制线
            GL.Color(rectColor);//设置方框的边框颜色 边框不透明
            GL.Vertex3(start.x, start.y, 0);
            GL.Vertex3(end.x, start.y, 0);
            GL.Vertex3(end.x, start.y, 0);
            GL.Vertex3(end.x, end.y, 0);
            GL.Vertex3(end.x, end.y, 0);
            GL.Vertex3(start.x, end.y, 0);
            GL.Vertex3(start.x, end.y, 0);
            GL.Vertex3(start.x, start.y, 0);
            GL.End();
            GL.PopMatrix();//恢复摄像机投影矩阵
        }
    }

}