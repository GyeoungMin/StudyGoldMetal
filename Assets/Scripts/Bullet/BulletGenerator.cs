using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public enum BulletType { player, enemy, follower }

public class BulletGenerator : MonoBehaviour
{
    [SerializeField] List<GameObject> playerBulletPrefabs;
    [SerializeField] List<GameObject> playerBulletGos;

    [SerializeField] GameObject enemyBulletPrefab;
    [SerializeField] GameObject enemyBulletGo;

    [SerializeField] GameObject followerBulletPrefab;
    [SerializeField] GameObject followerBulletGo;

    private List<IObjectPool<GameObject>> playerBulletPools;
    private IObjectPool<GameObject> enemyBulletPool;
    private IObjectPool<GameObject> followerBulletPool;

    void Awake()
    {
        playerBulletPools = new List<IObjectPool<GameObject>>(playerBulletPrefabs.Count);
        for (int i = 0; i < playerBulletPrefabs.Count; i++)
        {
            var prefab = playerBulletPrefabs[i];
            var go = playerBulletGos[i];
            var poolingObject = ObjectPoolManager.Instance.CreatePool<IPoolable>(go, prefab, 20);
            playerBulletPools.Add(poolingObject);
        }
        //enemyBulletPool = ObjectPoolManager.Instance.CreatePool<IPoolable>(enemyBulletGo, enemyBulletPrefab, 20);
        enemyBulletPool = ObjectPoolManager2.Instance.CreateObjectPool<IPoolable>(enemyBulletGo, enemyBulletPrefab, 20);
        followerBulletPool = ObjectPoolManager.Instance.CreatePool<IPoolable>(followerBulletGo, followerBulletPrefab, 20);

    }

    public void Shot(Transform firePoint, BulletType type, int power)
    {
        GameObject bullet = null;
        float dir = 1f;
        switch (type)
        {
            case BulletType.player: bullet = playerBulletPools[power].Get(); dir = 1f; break;
            case BulletType.enemy: bullet = enemyBulletPool.Get(); dir = -1f; break;
            case BulletType.follower: bullet = followerBulletPool.Get(); dir = 1f; break;
        }

        bullet.GetComponent<BulletController>().Setting(firePoint, dir);
    }
}
