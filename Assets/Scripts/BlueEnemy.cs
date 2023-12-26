using UnityEngine;

public class BlueEnemy : Enemy 
{
    [SerializeField] private SpawnerBullet spawnerBullet;

    private void Start()
    {
        spawnerBullet = spawnerBullet ? spawnerBullet : GetComponent<SpawnerBullet>();

        destroyable.OnTakeDamage += Health.TakeDamage;
        destroyable.OnDestorySelf += Health.Dead;

        spawnerBullet.EnabledIntervalFire = true;
    }   
}
