using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletGenerator : MonoBehaviour
{
    [SerializeField] GameObject BulletPrefab;

    void Awake()
    {
        ObjectPoolManager.Instance.CreatePool<IPoolable>(gameObject, BulletPrefab, 100);
    }

    public void Shot(Transform firePoint)
    {
        GameObject bullet = ObjectPoolManager.Instance.pool.Get();
        bullet.GetComponent<BulletController>().SetInit(firePoint);
    }
}
