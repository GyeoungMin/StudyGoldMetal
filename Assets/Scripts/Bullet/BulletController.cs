using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.U2D;
using UnityEngine.UIElements;

public class BulletController : MonoBehaviour, IPoolable
{
    public IObjectPool<GameObject> pool { get; set; }
    //[SerializeField] private Sprite[] sprites;

    public float bulletSpeed = 3f;

    private Coroutine coroutine;
    
    private float dir;
    //private SpriteRenderer sprite;

    private void OnEnable()
    {
        //sprite = GetComponent<SpriteRenderer>();
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
    public void Setting(Transform firePoint, float dir)
    {
        //switch (firePoint.gameObject.tag)
        //{
        //    case "Player": sprite.sprite = sprites[5]; gameObject.tag = "Player"; break;
        //    case "Enemy": sprite.sprite = sprites[0]; gameObject.tag = "Enemy"; break;
        //}
        transform.position = firePoint.position;
        transform.rotation = firePoint.rotation;
        this.dir = dir;
    }
}
