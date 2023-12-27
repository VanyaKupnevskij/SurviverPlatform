using System;
using UnityEngine;

public class SpawnerBulletPlayer : SpawnerBullet
{
    public Action<Type> OnStrike;
    public Action<Type> OnRicochet;

    public GameObject FireWithChance(Vector3 direction, float chanceRicochet)
    {
        GameObject bullet = Fire(direction);

        if (bullet.TryGetComponent(out PlayerBullet playerBullet))
        {
            playerBullet.chancheRicochet = chanceRicochet;
            playerBullet.OnStrike += HandleStrike;
            playerBullet.OnRicochet += HandleRicochet;
        }

        return bullet;
    }

    private void HandleStrike(Type typeEnemy)
    {
        OnStrike?.Invoke(typeEnemy);
    }

    private void HandleRicochet(Type typeEnemy)
    {
        OnRicochet?.Invoke(typeEnemy);
    }
}
