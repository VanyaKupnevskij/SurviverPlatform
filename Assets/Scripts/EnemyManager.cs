using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private Transform voidTransform;
    [SerializeField] private Transform player;
    [SerializeField] private float radiusEnemiesZone = 2.5f;
    [SerializeField] private float radiusPlatform = 5.0f;
    [SerializeField] private float radiusVoidTargeted = 1.0f;

    private List<Transform> enemies = new List<Transform>();
    private List<MoveTo> moviesToPlayer = new List<MoveTo>();
    private List<Circle> enemiesZone = new List<Circle>();

    private void Start()
    {
        var enemiesObject = GameObject.FindGameObjectsWithTag("Enemy");
        var moveisToPlayerObject = FindObjectsByType<MoveTo>(FindObjectsSortMode.None);

        foreach (var enemy in enemiesObject)
        {
            if (enemy.TryGetComponent<SpawnerBullet>(out SpawnerBullet spawnerBullet))
            {
                spawnerBullet.OnInstantiateBullet += HandleInstantiateBullet;
            }
        }

        enemies.AddRange(enemiesObject.Select(enemy => enemy.transform));
        moviesToPlayer.AddRange(moveisToPlayerObject.Where(moveTo => moveTo.whoFollow == WhoFollow.Player));

        moviesToPlayer.ForEach(enemy => enemy.Target = player);
    }

    public void SetVoidTarget(Vector3 voidPoint)
    {
        foreach (var enemy in moviesToPlayer)
        {
            if (Vector3.SqrMagnitude(enemy.transform.position - voidPoint) < radiusVoidTargeted * radiusVoidTargeted)
            {
                enemy.SetFollowVoid(Instantiate(voidTransform, voidPoint, Quaternion.identity));
            }
        }
    }

    public Vector3 RandomSavePoint()
    {
        Vector3 result = Vector3.zero;
        int limitCounter = 0;
        const int Limit_Search = 30;
        List<Circle> enemiesZone = new List<Circle>();

        enemiesZone = ScanEnemiesZone(enemies);

        do
        {
            result = Random.insideUnitSphere * radiusPlatform;
        } while (IsSaveZone(new Vector2(result.x, result.z), 
                            new Circle(radiusPlatform, Vector2.zero), 
                            enemiesZone) == false &&
                limitCounter++ < Limit_Search);

        return result;
    }

    private List<Circle> ScanEnemiesZone(List<Transform> enemies)
    {
        enemiesZone.Clear();

        enemiesZone.AddRange(
                enemies.Select(enemy => 
                    new Circle(radiusEnemiesZone, enemy.position.x, enemy.position.z)));

        return enemiesZone;
    }

    private bool IsSaveZone(Vector2 point, Circle saveZone, List<Circle> dangerZones)
    {
        bool outsideSaveZone = ((point.x - saveZone.center.x) * (point.x - saveZone.center.x) + (point.y - saveZone.center.y) * (point.y - saveZone.center.y)) > (saveZone.radius * saveZone.radius);
        if (outsideSaveZone) return false;

        float deltaX = 0.0f;
        float deltaY = 0.0f;
        float radius = 0.0f;
        foreach (Circle zone in dangerZones)
        {
            deltaX = point.x - zone.center.x;
            deltaY = point.y - zone.center.y;
            radius = zone.radius;

            deltaX *= deltaX;
            deltaY *= deltaY;
            radius *= radius;
            if ((deltaX + deltaY) <= radius) return false;
        }

        return true;
    }

    private void HandleInstantiateBullet(GameObject bullet)
    {
        MoveTo moveTo = bullet.GetComponent<MoveTo>();
        moveTo.Target = player;
        moviesToPlayer.Add(moveTo);
    }
}
