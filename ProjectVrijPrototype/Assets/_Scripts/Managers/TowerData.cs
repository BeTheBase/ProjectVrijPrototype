using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TowerData 
{
    [SerializeField]
    public string name;
    [SerializeField]
    public GameObject Tower;
    [SerializeField]
    public int GoldCost;
    [SerializeField]
    public int UpgradeGoldCost;
}
