using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave{
    [Header("Enemy Prefabs")]
    public List<GameObject> enemyPrefabs;
    [Header("Enemy Amounts")]
    public List<int> enemyAmounts;
    public float spawnRate;
}
