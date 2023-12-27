using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private EnemyManager enemyManager;
    [SerializeField, Range(0, 100)] private float chanceGetHealthByRicochet = 30;
    [SerializeField, Range(0, 100)] private float increaseEnegryByBlueEnemy = 50;
    [SerializeField, Range(0, 100)] private float increaseEnegryByRedEnemy = 15;

    public Health Health { get; set; } = new Health(100);
    public Energy Energy { get; set; } = new Energy(50);

    public SpawnerBulletPlayer spawnerBullet;
    public PlayerMove playerMove;
    public DestroyablePlayer playerDestroyable;

    private void Start()
    {
        enemyManager = enemyManager ? enemyManager : FindObjectOfType<EnemyManager>();
        spawnerBullet = spawnerBullet ? spawnerBullet : GetComponent<SpawnerBulletPlayer>();
        playerMove = playerMove ? playerMove : FindObjectOfType<PlayerMove>();
        playerDestroyable = playerDestroyable ? playerDestroyable : GetComponent<DestroyablePlayer>();

        playerDestroyable.OnTakeDamage += Health.TakeDamage;
        playerDestroyable.OnDecreaseEnergy += Energy.Decrease;
        Health.OnDead += HandleDead;
        Health.OnChange += HandleChangeHP;
        spawnerBullet.OnStrike += HandleStrike;
        spawnerBullet.OnRicochet += HandleRicochet;
    } 

    private void HandleDead()
    {
        playerMove.canControl = false;
    }

    private void HandleChangeHP(float currentHP)
    {
        if (currentHP > 0)
            playerMove.canControl = true;
    }

    private void HandleStrike(Type typeEnemy)
    {
        if (typeEnemy == typeof(BlueEnemy))
        {
            Energy.Increase(increaseEnegryByBlueEnemy);
        } 
        else if (typeEnemy == typeof(RedEnemy))
        {
            Energy.Increase(increaseEnegryByRedEnemy);
        }
    }

    private void HandleRicochet(Type typeEnemy)
    {
        bool increseHealth = UnityEngine.Random.Range(0, 100) <= chanceGetHealthByRicochet;

        if (increseHealth)
            Health.Increase(50);
        else
            Energy.Increase(10);
    }

    public void Ulta()
    {
        if (Energy.Ulta())
        {
            enemyManager.DestroyAllEnemy();
        }
    }
}
