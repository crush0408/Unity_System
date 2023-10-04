using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool<T> where T : PoolableMono
{
    private Stack<T> pool = new Stack<T>();
    private T prefab; // 오리지널 저장
    private Transform parent;

    public Pool(T prefab, Transform parent, int count)
    {
        this.prefab = prefab;
        this.parent = parent;

        for (int i = 0; i < count; i++)
        {
            T obj = GameObject.Instantiate(prefab, parent);
            obj.gameObject.name = obj.gameObject.name.Replace("(Clone)", "");  //클론이라는 이름 제거해줘야 다시 쓸 수 있음.
            obj.gameObject.SetActive(false);
            pool.Push(obj);
        }
    }
    public T Pop()
    {
        T obj = null;
        if (pool.Count < 1)
        {
            obj = GameObject.Instantiate(this.prefab, this.parent);
            obj.gameObject.name = obj.gameObject.name.Replace("(Clone)", "");
        }
        else
        {
            obj = pool.Pop();
            obj.gameObject.SetActive(true);
        }
        return obj;
    }

    public void Push(T obj, Transform parent)
    {
        obj.gameObject.SetActive(false);
        obj.transform.parent = parent;
        pool.Push(obj);
    }
}

public abstract class PoolableMono : MonoBehaviour
{
    public int poolingCount = 0;
    public abstract void Setting();
}