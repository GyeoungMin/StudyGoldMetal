using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerBullet : BulletController, IBullet
{
    public float damage { get; set; }

    public void SetDamage()
    {
        damage = 0.5f;
    }
}
