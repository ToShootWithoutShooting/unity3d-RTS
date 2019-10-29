using Interface;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]//序列化，可以把类保存到本地文件,并编辑
public class GameObjectPool
{//存储某一资源池
    [SerializeField]
    public string name;
    [SerializeField]
    private GameObject prefab;
    [SerializeField]
    private int maxAmout = 0;//可以容纳的最大对象数量
    //public GameObject pool;

    [NonSerialized]//不需要序列化
    private List<GameObject> list = new List<GameObject>();

    public GameObject GetInst(Vector3 position, Quaternion rotation)//从资源池中获取一个实例
    {
        foreach (GameObject go in list)
        {
            if (go.activeSelf == false)//当前go如果没被使用
            {
                go.SetActive(true);//激活
                go.transform.position = position;
                go.transform.rotation = rotation;
                return go;
            }
        }

        if (list.Count >= maxAmout)//如果数量大于最大数量，销毁一个
        {
            UnityEngine.Object.Destroy(list[0]);
            list.RemoveAt(0);
        }

        GameObject temp = GameObject.Instantiate(prefab) as GameObject;
        list.Add(temp);//每实例化一个，就添加一个
        temp.transform.position = position;
        temp.transform.rotation = rotation;
        return temp; ;
    }


}
