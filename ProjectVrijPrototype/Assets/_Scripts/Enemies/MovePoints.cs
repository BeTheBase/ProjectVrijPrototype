using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePoints : MonoBehaviour
{
    public List<Transform> EnemyMovePoints;
    public static MovePoints Instance;

    private void Awake()
    {
        Instance = this;
    }
}
