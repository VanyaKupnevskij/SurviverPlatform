using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    [SerializeField] private SpawnerBullet spawnerBullet;
    [SerializeField] private EnemyManager enemyManager;

    private void Start()
    {
        spawnerBullet = spawnerBullet ? spawnerBullet : GetComponent<SpawnerBullet>();
        enemyManager = enemyManager ? enemyManager : FindObjectOfType<EnemyManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) ||
            Input.GetMouseButtonDown(1))
        {
            Fire();
        }
    }

    public void Fire()
    {
        GameObject closestEnemy = enemyManager.GetClosestEnemyTo(transform.position);

        if (closestEnemy != null)
        {
            spawnerBullet.Fire(closestEnemy.transform.position - transform.position);
        }
        else
        {
            spawnerBullet.Fire();
        }
    }
}
