using UnityEngine;

public class BlueEnemy : Enemy 
{
    [SerializeField] private SpawnerBullet spawnerBullet;
    [SerializeField] private float startHealth = 100;

    private void Start()
    {
        Constructor(startHealth);

        spawnerBullet = spawnerBullet ? spawnerBullet : GetComponent<SpawnerBullet>();

        spawnerBullet.EnabledIntervalFire = true;
    }   
}
