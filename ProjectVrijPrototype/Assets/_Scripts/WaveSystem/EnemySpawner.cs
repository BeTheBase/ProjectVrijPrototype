﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemySpawner : MonoBehaviour {

    public static EnemySpawner Instance;

    public int EnemiesAlive = 0;

    public Wave[] waves;

    public static bool Spawning = true;

    [HideInInspector]
    public int enemiesToSpawn;

    public static int waveIndex = 0;

    ObjectPooler objectPooler;
    UIManager uIManager;

    public List<Transform> spawnPoints;

    private void Awake()
    {
        Instance = this;
    }

    // Use this for initialization
    void Start ()
    {
        uIManager = UIManager.Instance;
        objectPooler = ObjectPooler.Instance;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && waveIndex < waves.Length)
        {
            print("Wave: " + waveIndex);
            StartCoroutine(SpawnWave());
        }
    }

    IEnumerator SpawnWave()
    {
        yield return new WaitForSeconds(0.1f);

        if(waveIndex < waves.Length)
        {
            Wave wave = waves[waveIndex];
            StartCoroutine(SpawnAfterTime(wave.NextWaveTime));
        
            waveIndex++;

            for (int enemyType = 0; enemyType < wave.enemyAmounts.Count; enemyType++)
            {
                enemiesToSpawn += wave.enemyAmounts[enemyType];
            }

            for (int enemyType = 0; enemyType < wave.enemyPrefabs.Count; enemyType++)
            { 
                for (int i = 0; i < wave.enemyAmounts[enemyType]; i++)
                {
                    Spawn(wave.enemyPrefabs[enemyType].name);
                    enemiesToSpawn--;
                    yield return new WaitForSeconds(wave.spawnRate / spawnPoints.Count);
                }
            }
        }
    }

    IEnumerator SpawnAfterTime(float NextWaveTime)
    {
        uIManager.CurrentTime = NextWaveTime;
        yield return new WaitForSeconds(NextWaveTime);
        StartCoroutine(SpawnWave());
    }

    void Spawn(string enemy)
    {
        if (Spawning == true)
        {
            EnemiesAlive++;
            Vector3 spawnPosition = spawnPoints[Random.Range(0, spawnPoints.Count)].position;
            objectPooler.SpawnFromPool(enemy, spawnPosition, Quaternion.identity);
        }
    }
}
