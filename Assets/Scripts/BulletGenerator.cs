using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public enum BulletType { player, enemy }

public class BulletGenerator : MonoBehaviour
{
    [SerializeField] GameObject playerBulletPrefab;
    [SerializeField] GameObject enemyBulletPrefab;
    [SerializeField] GameObject playerBulletGo;
    [SerializeField] GameObject enemyBulletGo;

    IObjectPool<GameObject> playerBulletPool;
    IObjectPool<GameObject> enemyBulletPool;

    void Awake()
    {
        playerBulletPool = ObjectPoolManager.Instance.CreatePool<IPoolable>(playerBulletGo, playerBulletPrefab, 20);
        enemyBulletPool = ObjectPoolManager.Instance.CreatePool<IPoolable>(enemyBulletGo, enemyBulletPrefab, 20);
    }

    public void Shot(Transform firePoint, BulletType type)
    {
        GameObject bullet = null;
        float dir = 1f;
        switch (type)
        {
            case BulletType.player: bullet = playerBulletPool.Get(); dir = 1f; break;
            case BulletType.enemy: bullet = enemyBulletPool.Get(); dir = -1f; break;
        }

        //GameObject bullet = playerBulletPool.Get();
        bullet.GetComponent<BulletController>().Setting(firePoint, dir);
    }
}
