using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPoolManager
{
    public static readonly ObjectPoolManager Instance = new ObjectPoolManager();

    public IObjectPool<GameObject> pool {  get; private set; }

    private GameObject parent;
    private GameObject prefab;

    //»ý¼ºÀÚ
    private ObjectPoolManager() { }

    public void CreatePoolItems(GameObject parent,GameObject prefab, int poolSize)
    {
        this.parent = parent;
        this.prefab = prefab;

        pool = new ObjectPool<GameObject>(CreatePoolItem, OnGetPoolItem, OnReleasePoolItem, OnDestroyPoolItem, true, poolSize, poolSize * 5);

        for (int i = 0; i < poolSize; i++) {
            var item = CreatePoolItem();
            item.GetComponent<BulletController>().pool.Release(item);
        }

    }

    private GameObject CreatePoolItem()
    {
        var item = GameObject.Instantiate(prefab);
        item.GetComponent<BulletController>().pool = pool;
        item.SetActive(false);
        item.transform.SetParent(parent.transform,false);
        return item;
    }

    private void OnGetPoolItem(GameObject item)
    {
        item.SetActive(true);
    }
    private void OnReleasePoolItem(GameObject item)
    {
        item.SetActive(false);
    }
    private void OnDestroyPoolItem(GameObject item)
    {
        GameObject.Destroy(item.gameObject);
    }
}
