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
    [Header("Stats for each tower level")]
    [SerializeField]
    public List<float> UpgradeGoldCosts;
    [SerializeField]
    public List<float> TowerRanges;
    [SerializeField]
    public List<float> TowerDamages;
    [SerializeField]
    public List<float> TowerFireRates;
}
