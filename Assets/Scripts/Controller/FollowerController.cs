using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerController : MonoBehaviour
{
    private Transform target; // 따라다닐 대상 오브젝트
    public float smoothTime = 0.3f; // 부드러운 이동에 사용될 시간

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
