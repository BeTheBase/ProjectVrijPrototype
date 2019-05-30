using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemySpawnData
{
    public Wave[] Waves;

    public bool Spawning = true;

    [HideInInspector]
    public int enemiesToSpawn;

    public Transform StartPosition;

    public WavePath EnemyPath;
}
