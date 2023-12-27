using UnityEngine;

public class PlayerBullet : Bullet
{
    protected override void DamageTriggered(Collider other)
    {
        if (other.CompareTag(whoDamaged.ToString()))
        {
            other.GetComponent<Destroyable>()?.TakeDamage(damage);
            destroyable.DestroySelf();
        }
        if (other.CompareTag(WhoDamagedType.Wall.ToString()))
        {
            destroyable.DestroySelf();
        }
    }
}
