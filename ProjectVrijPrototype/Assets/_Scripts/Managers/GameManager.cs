using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void Start()
    {
        FindObjectOfType<AudioManager>().Play("theme1");
    }
}
