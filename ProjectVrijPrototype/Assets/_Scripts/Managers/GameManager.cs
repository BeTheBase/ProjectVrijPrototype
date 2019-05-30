using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Transform EndPosition;

    public GameObject WaveSpawner;
    public GameObject EnemySpawner;

    public int Gold;
    public int Lives;

    private void Awake()
    {
        Instance = this;
    }

    private void LateUpdate()
    {
        if(Lives <= 0)
            SceneManager.LoadSceneAsync(0);
    }
}
