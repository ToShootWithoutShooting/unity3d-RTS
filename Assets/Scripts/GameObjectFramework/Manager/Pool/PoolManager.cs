using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager {
    private static PoolManager _instance;//实现单例模式
    public static PoolManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new PoolManager();
            }
            return _instance;
        }       
    }
    
    private static string poolConfigPathPrefix = "Assets/Resources/";
    private const string poolConfigPathMiddle = "gameobjectpool";
    private const string poolConfigPathPostfix = ".asset";

    public static string PoolConfigPath
    {
        get
        {
            return poolConfigPathPrefix + poolConfigPathMiddle + poolConfigPathPostfix;
        }
    }

    private Dictionary<string, GameObjectPool> poolDict;//创造字典
    
    private PoolManager()//构造方法
    {
        GameObjectPoolList poolList = Resources.Load<GameObjectPoolList>(poolConfigPathMiddle);

        poolDict = new Dictionary<string, GameObjectPool>();//字典存储所有的资源池

        foreach(GameObjectPool pool in poolList.poolList)
        {
            poolDict.Add(pool.name, pool); 
        }
    }

    public void Init()//初始化
    {

    }

    public GameObject GetInstance(string poolName, Vector3 position, Quaternion rotation)//传入需要获取资源池的名字以及需要出现的位置，返回该资源
    {
        GameObjectPool pool;
        if(poolDict.TryGetValue(poolName,out pool))//如果资源池存在
        {
            return pool.GetInst(position, rotation);
        }
        Debug.LogWarning("PoolName is not exits");//弹出警告
        return null;
    }

}
