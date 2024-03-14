using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract class BulletFactory : MonoBehaviour
{
    IBullet createOperation()
    {
        IBullet bullet = createBullet();
        bullet.Setting();
        return bullet;
    }

    abstract protected IBullet createBullet();
}

