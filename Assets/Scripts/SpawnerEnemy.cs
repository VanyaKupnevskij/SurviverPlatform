using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnerEnemy : MonoBehaviour
{
    [SerializeField] private Transform[] enemyPrefabs;

    [SerializeField, Range(0, 30)] private float startInterval = 10.0f;
    [SerializeField, Range(0, 10)] private float decrementInterval = 2.0f;
    [SerializeField, Range(0.3f, 30)] private float minInterval = 6.0f;
    [SerializeField, Range(0, 200)] private int maxEnemies = 30;
    [SerializeField, Range(0, 20)] private float radiusSpawn = 5;

    public List<GameObject> enemies = new List<GameObject>(); 

    public Action<List<GameObject>> OnSpawnStack;

    private float _timerSpawn = 0.0f;
    private float currentInterval = 0.0f;

    private void Start()
    {
        currentInterval = startInterval + decrementInterval;
        _timerSpawn = currentInterval;

        OnSpawnStack += (list) => { };
    }

    private void Update()
    {
        TimerTick();
    }

    private void TimerTick()
    {
        if (_timerSpawn >= currentInterval)
        {
            DecrementInterval();
            Spawn();
            _timerSpawn = 0.0f;
        }
        else
        {
            _timerSpawn += Time.deltaTime;
        }
    }

    private void DecrementInterval()
    {
        if (currentInterval <= minInterval)
            currentInterval = minInterval;
        else
            currentInterval -= decrementInterval;
    }

    public void Spawn()
    {
        List<GameObject> stackEnemies = new List<GameObject>();

        foreach (Transform enemy in enemyPrefabs)
        {
            if (enemies.Count >= maxEnemies) break;

            GameObject instantiateEnemy = Instantiate(enemy).gameObject;
            Vector3 randPos = GetRandomPoint();
            instantiateEnemy.transform.position = randPos;

            stackEnemies.Add(instantiateEnemy);
            enemies.Add(instantiateEnemy);
        }

        OnSpawnStack(stackEnemies);
    }

    private Vector3 GetRandomPoint()
    {
        Vector3 resultPoint = Vector3.zero;

        const int Limit_Search = 30;
        int limitCounter = 0;

        do
        {
            resultPoint = UnityEngine.Random.insideUnitSphere * radiusSpawn;
        } while (TryGetClosestNavmeshPoint(resultPoint, out resultPoint) == false &&
                    limitCounter++ < Limit_Search);

        return resultPoint;
    }

    private bool TryGetClosestNavmeshPoint(Vector3 point, out Vector3 closestPoint)
    {
        closestPoint = Vector3.zero;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(point, out hit, 1.0f, NavMesh.AllAreas))
        {
            closestPoint = hit.position;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Despawn(GameObject who)
    {
        Destroy(who);
        enemies.Remove(who);
    }

    public void DespawnAll()
    {
        foreach (var enemy in enemies)
        {
            Destroy(enemy);
        }

        enemies.Clear();
    }
}
