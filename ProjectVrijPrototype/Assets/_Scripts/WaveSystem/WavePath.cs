using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavePath : MonoBehaviour
{
    public List<Transform> EnemyMovePoints;
    public static WavePath Instance;

    private void Awake()
    {
        Instance = this;
    }
}
