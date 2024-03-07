using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BulletController : MonoBehaviour
{
    public IObjectPool<GameObject> pool;

    public float bulletSpeed = 3f;

    private Coroutine coroutine;

    private void OnEnable()
    {
        coroutine = StartCoroutine(CoMoveBullet()); 
    }

    private void OnDisable()
    {
        StopCoroutine(coroutine);
    }

    IEnumerator CoMoveBullet()
    {
        while (true)
        {
            transform.Translate(transform.up * bulletSpeed *  Time.deltaTime);
            yield return null;
        }
    }
}
