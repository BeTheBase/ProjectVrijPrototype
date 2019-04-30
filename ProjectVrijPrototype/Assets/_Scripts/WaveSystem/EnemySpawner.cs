using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemySpawner : MonoBehaviour {

    public static EnemySpawner Instance;

    public int EnemiesAlive = 0;

    public Wave[] waves;

    public static bool Spawning = true;

    public int enemiesToSpawn;

    public static int waveIndex = 0;

    ObjectPooler objectPooler;
    public List<Vector3> spawnPoints;

    private void Awake()
    {
        Instance = this;
    }

    // Use this for initialization
    void Start ()
    {
        objectPooler = ObjectPooler.Instance;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && waveIndex < waves.Length && enemiesToSpawn == 0 && EnemiesAlive == 0)
        {
            print("Wave: " + waveIndex);
            StartCoroutine(SpawnWave());
        }
    }

    IEnumerator SpawnWave()
    {
        yield return new WaitForSeconds(1);
        Wave wave = waves[waveIndex];

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

    void Spawn(string enemy)
    {
        if (Spawning == true)
        {
            EnemiesAlive++;
            Vector3 spawnPosition = spawnPoints[Random.Range(0, spawnPoints.Count)];
            objectPooler.SpawnFromPool(enemy, spawnPosition, Quaternion.identity);
        }
    }
}
