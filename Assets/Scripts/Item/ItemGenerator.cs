using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ItemGenerator : MonoBehaviour
{
    [SerializeField] private GameObject boomItemPrefab;
    [SerializeField] private GameObject powerItemPrefab;
    [SerializeField] private GameObject coinItemPrefab;
    private IObjectPool<GameObject> boomPool { get; set; }
    private IObjectPool<GameObject> powerPool { get; set; }
    private IObjectPool<GameObject> coinPool { get; set; }

    void Awake()
    {
        boomPool = ObjectPoolManager.Instance.CreatePool<IPoolable>(gameObject, boomItemPrefab, 3);
        powerPool = ObjectPoolManager.Instance.CreatePool<IPoolable>(gameObject, powerItemPrefab, 3);
        coinPool = ObjectPoolManager.Instance.CreatePool<IPoolable>(gameObject, coinItemPrefab, 3);
    }

    public void GetItem(Transform tr)
    {
        int random = Random.Range(0, 3);
        GameObject item = null;
        switch (random)
        {
            case 0: item = boomPool.Get(); break;
            case 1: item = powerPool.Get(); break;
            case 2: item = coinPool.Get(); break;
        }
        item.GetComponent<ItemController>().Spawn(tr);
    }
}
