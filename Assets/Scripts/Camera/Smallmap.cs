using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Smallmap : MonoBehaviour,IPointerClickHandler
{
    public new Transform camera;
    public void OnPointerClick(PointerEventData eventData)
    {
        print(1);
        Vector2 tempVector = new Vector2(eventData.pointerCurrentRaycast.screenPosition.x / Screen.width, eventData.pointerCurrentRaycast.screenPosition.y / Screen.height);
        Vector2 raypoint = new Vector2((tempVector.x - (460 / 1920.0f)) / (1000 / 1920.0f), (tempVector.y - (40 / 1080.0f)) / (1000 / 1080.0f));
        Ray ray = Camera.main.ViewportPointToRay(raypoint);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            //设置玩家移动目标为点击地点
            camera.position = new Vector3(hit.point.x, camera.position.y, hit.point.z);
        }
        //LineRender指向点击位置
        //lineDraw.SetPositions(new Vector3[2] { Vector3.zero, hit.point });
    }


}