using UnityEngine;
using UnityEngine.AI;

public class TeleportFromOutside : MonoBehaviour
{
    public PlayerMove playerMove;
    public EnemyManager enemyManager;

    private Vector3 startPlayerPosition;

    private void Start()
    {
        playerMove = playerMove ? playerMove : FindObjectOfType<PlayerMove>();
        enemyManager = enemyManager ? enemyManager : FindObjectOfType<EnemyManager>();

        playerMove.OnGoOutside += HandleGoOutside;

        startPlayerPosition = playerMove.transform.position;
    }

    private void HandleGoOutside(Vector3 playerPos)
    {
        enemyManager.SetVoidTarget(playerPos);

        Vector3 randomPoint = enemyManager.RandomSavePoint();
        randomPoint.y = 0.3f;

        playerMove.TeleportTo(GetClosestNavmeshPoint(randomPoint));
    }

    private Vector3 GetClosestNavmeshPoint(Vector3 point)
    {
        NavMeshHit hit;
        if (NavMesh.SamplePosition(point, out hit, 1.0f, NavMesh.AllAreas))
        {
            return hit.position;
        } 
        else
        {
            return startPlayerPosition;
        }
    }

}
