﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeTowerManager : MonoBehaviour
{
    public static bool CheckFillBarStatus = true;

    public Text UpgradeCostTextField;
    public Text DeleteMoneyBackTextField;
    public Slider LoadingSlider;
    public float LoadingSpeed;

    private GameObject tower;
    private float goldCost;
    private TowerData upgradeTowerData;
    private Transform upgradeChildButton;
    private Transform deleteChildButton;
    private Transform getChildUI = null;
    private GameManager gameManager;

    private void Awake()
    {
        upgradeChildButton = transform.GetChild(0);
        deleteChildButton = transform.GetChild(1);
        gameObject.SetActive(false);
    }

    private void Start()
    {
        gameManager = GameManager.Instance; 
    }

    private void OnEnable()
    {
        tower = this.transform.parent.gameObject;
        if (TowerDataManager.Instance.TowerDatas != null)
        {
            upgradeTowerData = TowerDataManager.Instance.TowerDatas.Find(t => t.Tower.gameObject.name.Equals(tower.name.Replace("(Clone)", "")));
        }
        if (upgradeTowerData != null)
            goldCost = upgradeTowerData.UpgradeGoldCosts[tower.GetComponent<Turret>().TowerLevel];
        else
            goldCost = 90;
        UpgradeCostTextField.text = "Upgrade Cost:" + goldCost;
        DeleteMoneyBackTextField.text = "Delete/Money Back:" + goldCost / 2;

        for (int childIndex = 0; childIndex < tower.transform.childCount; childIndex++)
        {
            getChildUI = tower.transform.GetChild(childIndex);
        }
        if (tower.transform.childCount == 2 || tower.transform.childCount <= 0)
        {
            return;
        }
        
        if (tower.transform.GetChild(2).gameObject.activeSelf)
            upgradeChildButton.gameObject.SetActive(false);
        else
            upgradeChildButton.gameObject.SetActive(true);
    }

    public void UpgradeTower()
    {
        gameManager.Gold -= (int)goldCost;
        var towerTurretScript =  tower.GetComponent<Turret>();
        towerTurretScript.TowerLevel += 1;
        var towerLevel = towerTurretScript.TowerLevel;
        towerTurretScript.Damage = upgradeTowerData.TowerDamages[towerLevel];
        towerTurretScript.Range = upgradeTowerData.TowerRanges[towerLevel];
        towerTurretScript.FireRate = upgradeTowerData.TowerFireRates[towerLevel];
        if (tower.transform.childCount == 2 && towerLevel >= 2)
            tower.GetComponent<SandStormTornedo>().CanSwirl = true;
        StartCoroutine(WaitForLoadingBar());
    }

    public void DeleteTower()
    {
        gameManager.Gold += (int)goldCost / 2;
        tower.SetActive(false);
        tower.transform.parent.GetComponent<BoxCollider>().enabled = true;
        this.gameObject.SetActive(false);
    }

    private IEnumerator WaitForLoadingBar()
    {
        CheckFillBarStatus = false;
        yield return StartCoroutine(FillLoadingBar());
    }

    private IEnumerator FillLoadingBar()
    {
        tower.GetComponent<Turret>().enabled = false;


        /*
        switch(tower.transform.childCount)
        {
            case 1:
                getChildUI = tower.transform.GetChild(0);
                break;
            case 2:
                getChildUI = tower.transform.GetChild(1);
                break;
            case 3:
                getChildUI = tower.transform.GetChild(2);
                break;
                
        }*/



        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(false);

        float duration = LoadingSpeed; 
        float normalizedTime = 0;
        while (normalizedTime <= 1f)
        {
            LoadingSlider.value = normalizedTime;
            normalizedTime += Time.deltaTime / duration;
            yield return null;
        }
        CheckFillBarStatus = true;

        if (tower.transform.childCount > 2)
        {
            if (tower.transform.GetChild(1).gameObject.activeSelf)
                tower.transform.GetChild(2).gameObject.SetActive(true);
            else
                tower.transform.GetChild(1).gameObject.SetActive(true);
        }

        if (getChildUI.gameObject.activeSelf)
        {
            LoadingSlider.value = 0;
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(1).gameObject.SetActive(true);
            getChildUI.gameObject.SetActive(false);
        }

        tower.GetComponent<Turret>().enabled = true;

    }
}
