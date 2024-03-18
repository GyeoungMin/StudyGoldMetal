using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Pool;

public class ItemController : MonoBehaviour, IPoolable
{
    public IObjectPool<GameObject> pool { get; set; }

    private float speed = 3f;
    private Coroutine coroutine;
    private Vector2 dir;
    private void OnEnable()
    {
        dir = Random.insideUnitCircle;
        speed = Random.Range(3f, 6f);
        coroutine = StartCoroutine(CoMoveItem());
    }

    private void OnDisable()
    {
        StopCoroutine(coroutine);
    }

    IEnumerator CoMoveItem()
    {
        while (true)
        {
            transform.Translate(dir * speed * Time.deltaTime);
            float x = Mathf.Clamp(transform.position.x, -2.3f, 2.3f);
            float y = Mathf.Clamp(transform.position.y, -5.5f, 4.5f);
            transform.position = new Vector2(x, y);
            if (y <= -5.5f) { pool.Release(this.gameObject); }
            else if(y >= 4.5f) { dir.y *= -1f; }
            else if(Mathf.Abs(x) >= 2.3f) { dir.x *= -1f; }
            yield return null;
        }
    }

    public void Spawn(Transform tr)
    {
        transform.position = tr.position;
    }
}
