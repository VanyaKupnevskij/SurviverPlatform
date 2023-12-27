using UnityEngine;

public class BlueEnemy : Enemy 
{
    [SerializeField] private SpawnerBullet spawnerBullet;

    private void Start()
    {
        Constructor();

        spawnerBullet = spawnerBullet ? spawnerBullet : GetComponent<SpawnerBullet>();

        spawnerBullet.EnabledIntervalFire = true;
    }   
}
