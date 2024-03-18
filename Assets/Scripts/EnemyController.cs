using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;

public class EnemyController : MonoBehaviour
{

    [SerializeField] private Transform firePoint;
    [SerializeField] private AnimationClip clip;

    private BulletGenerator bulletGenerator;
    private ItemGenerator itemGenerator;
    private Collider2D col;
    private PlayableGraph graph;
    private Coroutine coroutine;
    private SpriteRenderer spriteRenderer;
    private Sprite sprite;

    private void Awake()
    {
        bulletGenerator = FindObjectOfType<BulletGenerator>();
        itemGenerator = FindObjectOfType<ItemGenerator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();

        sprite = spriteRenderer.sprite;
        PlayAnimation(gameObject, clip);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<BulletController>().pool.Release(collision.gameObject);
            col.enabled = false;
            StopCoroutine(coroutine);
            StartCoroutine(CoDamaged());
        }
    }

    IEnumerator CoDamaged()
    {
        graph.Play();
        while (true)
        {
            yield return new WaitForSeconds(1f);
            graph.Stop();
            itemGenerator.SpawnItem(transform);
            gameObject.SetActive(false);
        }
    }

    void OnEnable()
    {
        col.enabled = true;
        spriteRenderer.sprite = sprite;
        spriteRenderer.color = Color.white;
        coroutine = StartCoroutine(CoFire());
    }

    void OnDisable()
    {
        StopAllCoroutines();
    }

    IEnumerator CoFire()
    {
        while (true)
        {
            bulletGenerator.Shot(firePoint, BulletType.enemy);
            yield return new WaitForSeconds(0.125f);
        }
    }

    public void PlayAnimation(GameObject target, AnimationClip clip)
    {
        graph = PlayableGraph.Create();
        var animator = target.AddComponent<Animator>();
        var clipPlayable = AnimationClipPlayable.Create(graph, clip);
        var animOutput = AnimationPlayableOutput.Create(graph, "Explosion", animator);
        animOutput.SetSourcePlayable(clipPlayable);
    }

    private void OnDestroy()
    {
        if (graph.IsValid())
        {
            graph.Destroy();
        }
    }
}
