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

    public void Awake()
    {
        towerDataManager = TowerDataManager.Instance;
    }
    // Start is called before the first frame update
    void OnEnable()
    {
        SetStatsText();
    }
    
    void SetStatsText()
    {
        print(towerDataManager);
        DamageText.text = "Damage\n" + towerDataManager.TowerDatas[Index].TowerDamages[0];
        RangeText.text = "Range\n" + towerDataManager.TowerDatas[Index].TowerDamages[0];
        FireText.text = "Fire Rate\n" + towerDataManager.TowerDatas[Index].TowerDamages[0];

    }
}
