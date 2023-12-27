using System;
using UnityEngine;

public class PlayerBullet : Bullet
{
    [NonSerialized] public float chancheRicochet = 25.0f;
    
    public Action<Type> OnStrike;
    public Action<Type> OnRicochet;

    private int countStriked = 0;

    protected override void DamageTriggered(Collider other)
    {
        if (other.CompareTag(whoDamaged.ToString()))
        {
            other.GetComponent<Destroyable>()?.TakeDamage(damage);

            bool killed = other.GetComponent<Enemy>().Health.Value <= 0;

            if (killed)
            {
                Type typeEnemy = typeof(Nullable);
                if (other.TryGetComponent(out BlueEnemy blueEnemy))
                    typeEnemy = typeof(BlueEnemy);
                else if (other.TryGetComponent(out RedEnemy redEnemy))
                    typeEnemy = typeof(RedEnemy);

                if (countStriked == 0)
                    OnStrike?.Invoke(typeEnemy);
                else
                    OnRicochet?.Invoke(typeEnemy);

                countStriked++;
            }

            bool notRicochet = UnityEngine.Random.Range(0, 100) > chancheRicochet;

            if (notRicochet || countStriked > 1)
            {
                destroyable.DestroySelf();
            }
        }
        if (other.CompareTag(WhoDamagedType.Wall.ToString()))
        {
            destroyable.DestroySelf();
        }
    }
}
