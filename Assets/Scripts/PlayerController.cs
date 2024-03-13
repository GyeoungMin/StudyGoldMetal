using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 3f;
    [SerializeField] BulletGenerator bulletGenerator;

    private Animator playerAnimator;

    private float dirH;
    private float dirV;
    private float fire;

    private float lastFireTime;
    private float bulletBetTime = 0.125f;
    private float cullHp = 10f;
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        Transform firePoint = transform.Find("FirePoint");
        PlayerInput();

        Move();

        if (fire == 1 && Time.time >= lastFireTime + bulletBetTime)
        {
            lastFireTime = Time.time;
            bulletGenerator.Shot(firePoint, BulletType.player);
        }
        playerAnimator.SetFloat("Horizontal", dirH);
    }

    void PlayerInput()
    {
        dirH = Input.GetAxisRaw("Horizontal");
        dirV = Input.GetAxisRaw("Vertical");
        fire = Input.GetAxisRaw("Fire1");
    }

    void Move()
    {
        Vector2 dir = new Vector2(dirH, dirV).normalized;
        transform.Translate(dir * speed * Time.deltaTime);
        float h = Mathf.Clamp(transform.position.x, -2.3f, 2.3f);
        float v = Mathf.Clamp(transform.position.y, -4.5f, 4.5f);
        transform.position = new Vector2(h, v);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damaged(2);
    }

    void Damaged(float damage)
    {
        cullHp -= damage;
        playerAnimator.SetTrigger("isDie");
        //Destroy(gameObject);
    }
}
