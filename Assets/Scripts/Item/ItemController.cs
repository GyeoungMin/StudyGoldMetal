using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Pool;

public class ItemController : MonoBehaviour, IPoolable
{
    public IObjectPool<GameObject> pool { get; set; }
    private Coroutine coroutine;
    private float speed = 3f;
    private Vector2 dir;
    private void OnEnable()
    {
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
            transform.Translate(transform.forward * speed * Time.deltaTime);
            float x = Mathf.Clamp(transform.position.x, -2.3f, 2.3f);
            float y = Mathf.Clamp(transform.position.y, -5.5f, 4.5f);
            transform.position = new Vector2(x, y);
            if (y <= -5f) { pool.Release(gameObject); }
            else if(y >= 4.5f) { transform.rotation = Quaternion.LookRotation(new Vector2(dir.x,-dir.y)); }
            else if(Mathf.Abs(x) >= 2.3f) { transform.rotation = Quaternion.LookRotation(new Vector2(-dir.x, dir.y)); }
            yield return null;
        }
    }

    public void Spawn(Transform tr)
    {
        dir = Random.insideUnitCircle;
        transform.rotation = Quaternion.LookRotation(dir);
        transform.position = tr.position;
    }
}
