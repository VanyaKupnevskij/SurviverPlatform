using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    [SerializeField] private SpawnerBulletPlayer spawnerBullet;
    [SerializeField] private EnemyManager enemyManager;
    [SerializeField] private Player player;

    private void Start()
    {
        spawnerBullet = spawnerBullet ? spawnerBullet : GetComponent<SpawnerBulletPlayer>();
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
            float chance = 100 - (player.Health.Value / player.Health.maxValue * 100) + 20;
            chance = chance > 100 ? 100 : chance;
            spawnerBullet.FireWithChance(closestEnemy.transform.position - transform.position, chance);
        }
        else
        {
            spawnerBullet.Fire();
        }
    }
}
