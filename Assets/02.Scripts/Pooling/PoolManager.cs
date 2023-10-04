using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PoolManager : Singleton<PoolManager>
{
    public PoolableMono[] poolingObjects;
    private Dictionary<string, Pool<PoolableMono>> poolDict = new Dictionary<string, Pool<PoolableMono>>();

    private void Awake()
    {
        for (int i = 0; i < poolingObjects.Length; i++)
        {
            CreatePool(poolingObjects[i], poolingObjects[i].poolingCount);
        }
    }

    private void CreatePool(PoolableMono prefab, int count)
    {
        Pool<PoolableMono> pool = new Pool<PoolableMono>(prefab, transform, count);

        poolDict.Add(prefab.GetType().Name, pool);
    }
    public PoolableMono Pop(string key)
    {
        if (!poolDict.ContainsKey(key)) // 해당 키 존재 X
        {
            Debug.LogError($"Prefab doesnt exist on pool (key:{key})"); 
            return null;
        }
        PoolableMono item = poolDict[key].Pop();
        item.Setting();
        return item;
    }
    public PoolableMono Pop<T>(Vector3 position, Transform parent) where T : PoolableMono
    {
        string key = typeof(T).Name;
        if (!poolDict.ContainsKey(key))
        {
            Debug.LogError($"Prefab doesnt exist on pool (key:{key})");
            return null;
        }
        PoolableMono item = poolDict[key].Pop();
        item.transform.parent = parent;
        item.transform.position = position;
        item.Setting();
        return item;
    }

    public void Push(PoolableMono obj)
    {
        poolDict[obj.GetType().Name].Push(obj, transform);
    }
}
