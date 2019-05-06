using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerDataManager : MonoBehaviour
{
    public List<TowerData> TowerDatas;
    public static TowerDataManager Instance;

    private void Awake()
    {
        Instance = this;
    }
}
