using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    [SerializeField] private SpawnerBullet spawnerBullet;

    private void Start()
    {
        spawnerBullet = spawnerBullet ? spawnerBullet : GetComponent<SpawnerBullet>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Fire();
        }
    }

    public void Fire()
    {
        spawnerBullet.Fire();
    }
}
