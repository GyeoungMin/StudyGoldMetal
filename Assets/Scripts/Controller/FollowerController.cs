using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerController : MonoBehaviour
{
    private Transform target; // ����ٴ� ��� ������Ʈ
    public float smoothTime = 0.3f; // �ε巯�� �̵��� ���� �ð�

    private Coroutine coroutine;
    private Vector3 velocity = Vector3.zero;

    private void Start()
    {
        target = FindObjectOfType<PlayerController>().transform;
        coroutine = StartCoroutine(CoFire());
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, target.position) > 0.5f)
        {
            Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }
    }

    IEnumerator CoFire()
    {
        while (true)
        {
            FindObjectOfType<BulletGenerator>().Shot(transform, BulletType.follower, 0);
            yield return new WaitForSeconds(0.25f);
        }
    }
}
