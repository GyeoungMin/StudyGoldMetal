using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletGenerator : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;

    void Awake()
    {
        ObjectPoolManager.Instance.CreatePoolItems(gameObject, bulletPrefab, 50);
    }

    public void Shot(Transform firePoint)
    {
        GameObject bullet = ObjectPoolManager.Instance.pool.Get();
        bullet.transform.rotation = firePoint.rotation;
        bullet.transform.position = firePoint.position;
    }
}
