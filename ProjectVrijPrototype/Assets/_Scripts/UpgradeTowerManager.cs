using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeTowerManager : MonoBehaviour
{
    public List<UpgradeTowerData> TowerUpgradeData;
    public Text UpgradeCostTextField;
    public Slider LoadingSlider;
    public float LoadingSpeed;

    private GameObject tower;
    private float goldCost;
    private UpgradeTowerData upgradeTowerData;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        tower = this.transform.parent.gameObject;
        if (TowerUpgradeData != null)
        {
            upgradeTowerData = TowerUpgradeData.Find(t => t.Tower.gameObject.name.Equals(tower.name.Replace("(Clone)", "")));
        }
        if (upgradeTowerData != null)
            goldCost = upgradeTowerData.GoldCost;
        else
            goldCost = 90;
        UpgradeCostTextField.text = "Upgrade Cost:" + goldCost; 
    }

    public void UpgradeTower()
    {
        StartCoroutine(FillLoadingBar());       
    }

    public void DeleteTower()
    {
        tower.SetActive(false);
        this.gameObject.SetActive(false);
    }

    public IEnumerator FillLoadingBar()
    {
        tower.transform.GetChild(3).gameObject.transform.GetChild(0).gameObject.SetActive(false);
        tower.transform.GetChild(3).gameObject.transform.GetChild(1).gameObject.SetActive(false);

        float duration = LoadingSpeed; // 3 seconds you can change this 
                             //to whatever you want
        float normalizedTime = 0;
        while (normalizedTime <= 1f)
        {
            LoadingSlider.value = normalizedTime;
            normalizedTime += Time.deltaTime / duration;
            yield return null;
        }
        //yield return new WaitForSeconds(duration -1);
        if (tower.transform.GetChild(1).gameObject.activeSelf)
            tower.transform.GetChild(2).gameObject.SetActive(true);
        else
            tower.transform.GetChild(1).gameObject.SetActive(true);

        if (tower.transform.GetChild(3).gameObject.activeSelf)
        {
            LoadingSlider.value = 0;
            tower.transform.GetChild(3).gameObject.transform.GetChild(0).gameObject.SetActive(true);
            tower.transform.GetChild(3).gameObject.transform.GetChild(1).gameObject.SetActive(true);
            tower.transform.GetChild(3).gameObject.SetActive(false);
        }
    }
}
