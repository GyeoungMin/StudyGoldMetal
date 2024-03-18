using UnityEngine;

public class PlayerBullet0 : MonoBehaviour, IBullet
{
    public float damage { get; set; }

    public void SetDamage()
    {
        damage = 1f;
    }
}
