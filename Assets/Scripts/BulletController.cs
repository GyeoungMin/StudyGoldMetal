using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.U2D;
using UnityEngine.UIElements;

public class BulletController : MonoBehaviour, IPoolable
{
    public IObjectPool<GameObject> pool { get; set; }

    public float bulletSpeed = 3f;

    private Coroutine coroutine;
    
    [SerializeField] private Sprite[] sprites;
    private float dir = 1f;
    private SpriteRenderer sprite;

    private void OnEnable()
    {
        sprite = GetComponent<SpriteRenderer>();
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
            transform.Translate(dir * transform.up * bulletSpeed *  Time.deltaTime);
            if (transform.position.y >= 5.5 || transform.position.y <= -5.5)
            {
                pool.Release(gameObject);
            }
            yield return null;
        }
    }
    public void SetInit(Transform firePoint)
    {
        switch (firePoint.gameObject.tag)
        {
            case "Player": dir = 1f; sprite.sprite = sprites[5]; break;
            case "Enemy": dir = -1f; sprite.sprite = sprites[0]; break;
        }
        transform.position = firePoint.position;
        transform.rotation = firePoint.rotation;
    }
}
