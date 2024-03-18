using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPoolManager
{
    public static readonly ObjectPoolManager Instance = new ObjectPoolManager();

    public IObjectPool<GameObject> pool;
    public IObjectPool<GameObject> playerPool { get; private set; }
    public IObjectPool<GameObject> enemyPool { get; private set; }
    private GameObject parent;
    private GameObject prefab;

    //»ý¼ºÀÚ
    private ObjectPoolManager() { }

    public IObjectPool<GameObject> CreatePool<T>(GameObject parent, GameObject prefab, int poolSize) where T : IPoolable
    {
        this.parent = parent;
        this.prefab = prefab;

        pool = new ObjectPool<GameObject>(CreatePoolItem<T>, OnGetPool, OnReleasePool, OnDestroyPool, true, poolSize, poolSize * 5);

        for (int i = 0; i < poolSize; i++)
        {
            var item = CreatePoolItem<T>();
            item.GetComponent<T>().pool.Release(item);
        }

        return pool;
    }

    private GameObject CreatePoolItem<T>() where T : IPoolable
    {
        var item = GameObject.Instantiate(prefab);
        item.SetActive(false);
        item.transform.SetParent(parent.transform, false);
        item.GetComponent<T>().pool = pool;

        return item;
    }

    private void OnGetPool(GameObject item)
    {
        item.SetActive(true);
    }
    private void OnReleasePool(GameObject item)
    {
        item.SetActive(false);
    }
    private void OnDestroyPool(GameObject item)
    {
        GameObject.Destroy(item.gameObject);
    }
}
