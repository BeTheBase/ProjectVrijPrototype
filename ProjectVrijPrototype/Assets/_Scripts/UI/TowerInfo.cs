using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TowerInfo : MonoBehaviour
{
    public int Index;
    private TowerDataManager towerDataManager;
    public Text DamageText;
    public Text RangeText;
    public Text FireText;
    public Text GoldText;

    SelectTile selectTile;

    public void Awake()
    {
        towerDataManager = TowerDataManager.Instance;
        selectTile = SelectTile.Instance;
    }
    // Start is called before the first frame update
    void OnEnable()
    {
        SetStatsText();
        selectTile.currentTowerInfo = gameObject;
    }
    
    void SetStatsText()
    {
        DamageText.text = "" + towerDataManager.TowerDatas[Index].TowerDamages[0];
        FireText.text = towerDataManager.TowerDatas[Index].TowerFireRates[0] + "/s";
    }
}
