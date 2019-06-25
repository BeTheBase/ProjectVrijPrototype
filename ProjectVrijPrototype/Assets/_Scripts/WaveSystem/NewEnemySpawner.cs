using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewEnemySpawner : MonoBehaviour
{
    public static NewEnemySpawner Instance;
    public GameObject Popup;
    public float PopupTime = 5f;
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
        uIManager = NewUIManager.Instance;
        objectPooler = ObjectPooler.Instance;
        waveIndex = 0;
    }

    public void NextWave()
    {
        Popup.SetActive(true);
        StartCoroutine(Deactivate(PopupTime));
        StartCoroutine(SpawnWave());
    }

    IEnumerator SpawnWave()
    {
        yield return new WaitForSeconds(0.1f);

        StartCoroutine(SpawnAfterTime(EnemySpawners[0].Waves[0].NextWaveTime));

        if (waveIndex < EnemySpawners[0].Waves.Length)
            {
                Wave wave = EnemySpawners[0].Waves[waveIndex];

                waveIndex++;

                for (int enemyType = 0; enemyType < wave.enemyAmounts.Count; enemyType++)
                {
                    EnemySpawners[0].enemiesToSpawn += wave.enemyAmounts[enemyType];
                }

                for (int enemyType = 0; enemyType < wave.enemyPrefabs.Count; enemyType++)
                {
                    for (int i = 0; i < wave.enemyAmounts[enemyType]; i++)
                    {
                        Spawn(wave.enemyPrefabs[enemyType].name);
                        EnemySpawners[0].enemiesToSpawn--;
                        yield return new WaitForSeconds(wave.spawnRate);
                    }
                }
          }

        if (waveIndex < EnemySpawners[1].Waves.Length)
        {
            Wave wave = EnemySpawners[1].Waves[waveIndex];

            for (int enemyType = 0; enemyType < wave.enemyAmounts.Count; enemyType++)
            {
                EnemySpawners[1].enemiesToSpawn += wave.enemyAmounts[enemyType];
            }

            for (int enemyType = 0; enemyType < wave.enemyPrefabs.Count; enemyType++)
            {
                for (int i = 0; i < wave.enemyAmounts[enemyType]; i++)
                {
                    Spawn(wave.enemyPrefabs[enemyType].name);
                    EnemySpawners[1].enemiesToSpawn--;
                    yield return new WaitForSeconds(wave.spawnRate);
                }
            }
        }        
    }

    IEnumerator Deactivate(float time)
    {
        Popup.transform.GetChild(1).GetComponent<Image>().CrossFadeAlpha(0, time, true);
        Popup.transform.GetChild(2).GetComponent<Image>().CrossFadeAlpha(0, time, true);
        Popup.transform.GetChild(2).GetChild(0).GetComponent<Text>().CrossFadeAlpha(0, time, true);
        yield return new WaitForSeconds(time);

        Popup.SetActive(false);
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
                Enemy.GetComponent<BaseEnemy>().WavePath = EnemySpawners[index].EnemyPath;
                Enemy.transform.position = new Vector3(spawnPosition.x, Enemy.transform.localScale.y * 2f, spawnPosition.z);
            }
        }
    }
}
