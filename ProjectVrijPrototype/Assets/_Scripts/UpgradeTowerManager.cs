using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeTowerManager : MonoBehaviour
{
    public int GoldCost;

    private GameObject tower;

    private void Start()
    {
        tower = this.transform.parent.gameObject;
        
    }

    public void UpgradeTower()
    {
        tower = this.transform.parent.gameObject;

        if (tower.transform.GetChild(1).gameObject.activeSelf)
            tower.transform.GetChild(2).gameObject.SetActive(true);
        else
            tower.transform.GetChild(1).gameObject.SetActive(true);
    }

    public void DeleteTower()
    {
        tower = this.transform.parent.gameObject;
        //Destroy(tower.gameObject);
    }
}
