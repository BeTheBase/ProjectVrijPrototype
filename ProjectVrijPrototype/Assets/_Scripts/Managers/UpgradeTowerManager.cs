using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeTowerManager : MonoBehaviour
{
    public static bool CheckFillBarStatus = true;

    public Text UpgradeCostTextField;
    public Slider LoadingSlider;
    public float LoadingSpeed;

    private GameObject tower;
    private float goldCost;
    private TowerData upgradeTowerData;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        tower = this.transform.parent.gameObject;
        if (TowerDataManager.Instance.TowerDatas != null)
        {
            upgradeTowerData = TowerDataManager.Instance.TowerDatas.Find(t => t.Tower.gameObject.name.Equals(tower.name.Replace("(Clone)", "")));
        }
        if (upgradeTowerData != null)
            goldCost = upgradeTowerData.UpgradeGoldCost;
        else
            goldCost = 90;
        UpgradeCostTextField.text = "Upgrade Cost:" + goldCost;

        if (tower.transform.GetChild(2).gameObject.activeSelf)
            tower.transform.GetChild(3).gameObject.transform.GetChild(0).gameObject.SetActive(false);
        else
            tower.transform.GetChild(3).gameObject.transform.GetChild(0).gameObject.SetActive(true);
    }

    public void UpgradeTower()
    {
        StartCoroutine(WaitForLoadingBar());       
    }

    public void DeleteTower()
    {
        tower.SetActive(false);
        this.gameObject.SetActive(false);
    }

    private IEnumerator WaitForLoadingBar()
    {
        CheckFillBarStatus = false;
        yield return StartCoroutine(FillLoadingBar());
    }

    private IEnumerator FillLoadingBar()
    {
        tower.transform.GetChild(3).gameObject.transform.GetChild(0).gameObject.SetActive(false);
        tower.transform.GetChild(3).gameObject.transform.GetChild(1).gameObject.SetActive(false);

        float duration = LoadingSpeed; 
        float normalizedTime = 0;
        while (normalizedTime <= 1f)
        {
            LoadingSlider.value = normalizedTime;
            normalizedTime += Time.deltaTime / duration;
            yield return null;
        }
        CheckFillBarStatus = true;

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
