using UnityEngine;
using UnityEngine.Pool;

public class ObjectPoolManager2
{
    public static readonly ObjectPoolManager2 Instance = new ObjectPoolManager2();

    public IObjectPool<GameObject> pool {  get; private set; }

    private GameObject parentGo;
    private GameObject prefabGo;

    private ObjectPoolManager2() { }

    public IObjectPool<GameObject> CreateObjectPool<T>(GameObject parent, GameObject prefab, int size) where T : IPoolable
    {
        parentGo = parent;
        prefabGo = prefab;

        pool = new ObjectPool<GameObject>(CreatePoolObject<T>, OnGetPool, OnReleasePool, OnDestroyPool, true, size, size * 10);

        for (int i = 0; i < size; i++)
        {
            GameObject obj = CreatePoolObject<T>();
            obj.GetComponent<T>().pool.Release(obj);
        }

        return pool;
    }

    private GameObject CreatePoolObject<T>() where T : IPoolable
    {
        GameObject obj = GameObject.Instantiate(prefabGo);
        obj.SetActive(false);
        obj.transform.SetParent(parentGo.transform, false);
        obj.GetComponent<T>().pool = pool;

        return obj;
    }

    private void OnGetPool(GameObject obj)
    {
        obj.SetActive(true);
    }
    private void OnReleasePool(GameObject obj)
    {
        obj.SetActive(false);
    }
    private void OnDestroyPool(GameObject obj)
    {
        GameObject.Destroy(obj);
    }
}
