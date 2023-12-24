using UnityEngine;

public class EnergyBullet : Bullet
{
    protected override void DamageTriggered(Collider other)
    {
        other.GetComponent<DestroyablePlayer>()?.DecreaseEnergy(damage);
    }
}
