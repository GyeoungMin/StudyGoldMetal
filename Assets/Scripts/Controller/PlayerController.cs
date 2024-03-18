using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 5f;

    private BulletGenerator bulletGenerator;
    private Animator playerAnimator;

    private float dirH;
    private float dirV;
    private float fire;
    private float boom;
    
    private int boomCount;
    private int powerLevel;

    private float fireCooldownTime = 0.125f;
    private bool isFireCooldown = false;
    private bool isBoomCooldown = false;
    //private float cullHp = 10f;

    void Awake()
    {
        playerAnimator = GetComponent<Animator>();
        bulletGenerator = FindObjectOfType<BulletGenerator>();
        boomCount = 3;
        powerLevel = 0;
    }
    private void OnEnable()
    {
        boomCount = 3;
        powerLevel = 0;
    }

    void FixedUpdate()
    {
        Transform firePoint = transform.Find("FirePoint");
        PlayerInput();

        Move();

        if (fire == 1 && !isFireCooldown)
        {
            Fire(firePoint);
            StartCoroutine(CoFireCooldown());
        }

        if (boom == 1 && !isBoomCooldown)
        {
            UseBoom();
            StartCoroutine(CoBoomCooldown());
        }
    }

    void PlayerInput()
    {
        dirH = Input.GetAxisRaw("Horizontal");
        dirV = Input.GetAxisRaw("Vertical");
        fire = Input.GetAxisRaw("Fire1");
        boom = Input.GetAxisRaw("Boom");
    }

    void Move()
    {
        Vector2 dir = new Vector2(dirH, dirV).normalized;
        transform.Translate(dir * speed * Time.deltaTime);

        float h = Mathf.Clamp(transform.position.x, -2.3f, 2.3f);
        float v = Mathf.Clamp(transform.position.y, -4.5f, 4.5f);
        transform.position = new Vector2(h, v);

        playerAnimator.SetFloat("Horizontal", dirH);
    }

    void Fire(Transform firePoint)
    {
        bulletGenerator.Shot(firePoint, BulletType.player, powerLevel);
    }
    IEnumerator CoFireCooldown()
    {
        isFireCooldown = true;
        yield return new WaitForSeconds(fireCooldownTime);
        isFireCooldown = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Damaged();
        }
        if (collision.CompareTag("Item"))
        {
            collision.GetComponent<IItem>().Get();
            collision.GetComponent<ItemController>().pool.Release(collision.gameObject);
        }
    }

    void Damaged()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        playerAnimator.SetTrigger("isDie");
        Destroy(gameObject, 1f);
    }

    void UseBoom()
    {
        if (boomCount > 0)
        {
            boomCount--;
        }
        FindObjectOfType<UIManager>().UIBooms(boomCount);
    }

    public void AddBoom()
    {
        if (boomCount < 3)
        {
            boomCount++;
        }
        FindObjectOfType<UIManager>().UIBooms(boomCount);
    }

    IEnumerator CoBoomCooldown()
    {
        isBoomCooldown = true;
        yield return new WaitForSeconds(3f);
        isBoomCooldown = false;
    }

    public void PowerUp()
    {
        powerLevel++;
    }
}
