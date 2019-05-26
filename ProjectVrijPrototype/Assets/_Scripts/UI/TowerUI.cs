using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TowerUI : MonoBehaviour
{
    public Turret turret;
    public Text DamageText;
    public Text RangeText;
    public Text FireText;

    // Start is called before the first frame update
    void OnEnable()
    {
        SetStatsText();
    }
    
    void SetStatsText()
    {
        DamageText.text = "Damage\n" + turret.Damage;
        RangeText.text = "Range\n" + turret.Range;
        FireText.text = "Fire Rate\n" + turret.FireRate;

    }
}
