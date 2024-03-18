using UnityEngine;

public class PlayerBullet1 : MonoBehaviour, IBullet
{
    public float damage { get; set; }

    public void SetDamage()
    {
        damage = 2f;
    }
}
