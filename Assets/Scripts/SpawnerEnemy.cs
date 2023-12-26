using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerEnemy : MonoBehaviour
{
    [SerializeField] private Transform[] enemyPrefabs;

    [SerializeField] private float startInterval = 10.0f;
    [SerializeField] private float decrementInterval = 2.0f;
    [SerializeField] private float minInterval = 6.0f;
    [SerializeField] private int maxEnemies = 30;

    public Action<List<GameObject>> OnSpawnStack;
    public List<GameObject> enemies = new List<GameObject>();

    private float _timerSpawn = 0.0f;
    private float currentInterval = 0.0f;

    private void Start()
    {
        currentInterval = startInterval + decrementInterval;
        _timerSpawn = currentInterval;
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

            stackEnemies.Add(instantiateEnemy);
            enemies.Add(instantiateEnemy);
        }

        OnSpawnStack(stackEnemies);
    } 
}
