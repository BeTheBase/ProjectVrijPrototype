using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeSandStorm : MonoBehaviour
{
    public int GoldCost = 300;
    public List<int> UpgradeCosts;
    public GameObject SandStorm;
    public Text UpgradeText;
    private GameManager gameManager;
    private int Level = 0;

    private void Start()
    {
        gameManager = GameManager.Instance;
    }

    public void Buy()
    {
        if (Level >= 1)
        {
            Upgrade();
            return;
        }
        if (gameManager.Gold < GoldCost) return;
        //GameObject sandStorm = Instantiate(SandStorm, transform.position, Quaternion.identity);
        gameManager.Gold -= GoldCost;
        SandStorm.SetActive(true);
        UpgradeText.text = "Cost:  " + UpgradeCosts[Level];
        Level += 1;
        gameObject.SetActive(false);
        
    }

    public void Upgrade()
    {
        Level += 1;
        if (gameManager.Gold <= GoldCost) return;
        UpgradeText.text = "Cost:  " + UpgradeCosts[Level];
        SandStormTornedo sandStormScript = SandStorm.GetComponent<SandStormTornedo>();
        sandStormScript.DelayTime -= Level;
        gameObject.SetActive(false);

    }
}
