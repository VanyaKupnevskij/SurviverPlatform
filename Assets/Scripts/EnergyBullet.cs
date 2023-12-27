using UnityEngine;

public class EnergyBullet : Bullet
{
    protected override void DamageTriggered(Collider other)
    {
        if (other.CompareTag(whoDamaged.ToString()))
        {
            other.GetComponent<DestroyablePlayer>()?.DecreaseEnergy(damage);
            destroyable.DestroySelf();
        }
    }
}
