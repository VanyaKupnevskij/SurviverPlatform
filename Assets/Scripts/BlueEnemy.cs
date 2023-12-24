using System.Collections;
using UnityEngine;

public class BlueEnemy : Enemy 
{
    [SerializeField] private SpawnerBullet spawnerBullet;
    [SerializeField, Range(0.1f, 10.0f)] private float intervalFire = 3.0f;

    private float timerFire = 0.0f;

    private void Start()
    {
        destroyable.OnTakeDamage += Health.TakeDamage;

        spawnerBullet = spawnerBullet ?? GetComponent<SpawnerBullet>();
    }

    private void Update()
    {
        if (timerFire >= intervalFire)
        {
            spawnerBullet.Fire();
            timerFire = 0.0f;
        }
        else
        {
            timerFire += Time.deltaTime;
        }
    }
}
