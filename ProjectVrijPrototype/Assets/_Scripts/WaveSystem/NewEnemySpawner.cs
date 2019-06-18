using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewEnemySpawner : MonoBehaviour
{
    public static NewEnemySpawner Instance;

    public List<EnemySpawnData> EnemySpawners;
    [HideInInspector]
    public int EnemiesAlive = 0;

    public static int waveIndex = 0;

    ObjectPooler objectPooler;
    NewUIManager uIManager;

    private void Awake()
    {
        Instance = this;
    }

    // Use this for initialization
    void Start()
    {
        //uIManager = NewUIManager.Instance;
        objectPooler = ObjectPooler.Instance;
    }

    public void NextWave()
    {
        StartCoroutine(SpawnWave());
    }

    IEnumerator SpawnWave()
    {
        yield return new WaitForSeconds(0.1f);

        for (int index = 0; index < EnemySpawners.Count; index++)
        {
            if (waveIndex < EnemySpawners[index].Waves.Length)
            {
                Wave wave = EnemySpawners[index].Waves[waveIndex];
                StartCoroutine(SpawnAfterTime(wave.NextWaveTime));

                waveIndex++;

                for (int enemyType = 0; enemyType < wave.enemyAmounts.Count; enemyType++)
                {
                    EnemySpawners[index].enemiesToSpawn += wave.enemyAmounts[enemyType];
                }

                for (int enemyType = 0; enemyType < wave.enemyPrefabs.Count; enemyType++)
                {
                    for (int i = 0; i < wave.enemyAmounts[enemyType]; i++)
                    {
                        Spawn(wave.enemyPrefabs[enemyType].name);
                        EnemySpawners[index].enemiesToSpawn--;
                        yield return new WaitForSeconds(wave.spawnRate);
                    }
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
        for (int index = 0; index < EnemySpawners.Count; index++)
        {
            if (EnemySpawners[index].Spawning == true)
            {
                EnemiesAlive++;
                Vector3 spawnPosition = EnemySpawners[index].StartPosition.position;
                GameObject Enemy = objectPooler.SpawnFromPool(enemy, spawnPosition, Quaternion.identity);
                print(Enemy);
                Enemy.GetComponent<BaseEnemy>().WavePath = EnemySpawners[index].EnemyPath;
                Enemy.transform.position = new Vector3(spawnPosition.x, Enemy.transform.localScale.y * 2f, spawnPosition.z);
            }
        }
    }
}
