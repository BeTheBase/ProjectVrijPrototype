using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectTowerManager : MonoBehaviour
{
    public static bool CheckTowerStatus = true;

    public string TowerCostString = "Tower Cost:";

    public Text[] TowerCostTextFields;

    private GameManager gameManager;
    private TowerDataManager towerDataManager;
    private GameObject parentGameObject;

    private int index = 0;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    private void Start()
    {
        gameManager = GameManager.Instance;
        towerDataManager = TowerDataManager.Instance;

        if (towerDataManager.TowerDatas != null)
        {
            for (int index = 0; index <= TowerCostTextFields.Length -1; index++)
            {
                TowerCostTextFields[index].text = TowerCostString + towerDataManager.TowerDatas[index].GoldCost;
            }
        }
    }

    public void PlaceTower(int index)
    {
        var towerCost = ReturnGoldCost(index);
        if (!ContinueProgramm(towerCost)) return;
        parentGameObject = transform.parent.gameObject;
        parentGameObject.GetComponent<BoxCollider>().enabled = false;
        gameManager.Gold -= towerCost;
        GameObject towerPlaceHolder = Instantiate(towerDataManager.TowerDatas[index].Tower, transform.position - Vector3.up, Quaternion.identity);
        towerPlaceHolder.transform.SetParent(parentGameObject.transform);
        var towerTurretScript = towerPlaceHolder.GetComponent<Turret>();
        towerTurretScript.Damage = towerDataManager.TowerDatas[index].TowerDamages[0];
        towerTurretScript.Range = towerDataManager.TowerDatas[index].TowerRanges[0];
        towerTurretScript.FireRate = towerDataManager.TowerDatas[index].TowerFireRates[0];
        this.gameObject.SetActive(false);
    }

    private int ReturnGoldCost(int _index)
    {
        return towerDataManager.TowerDatas[_index].GoldCost;
    }

    private bool ContinueProgramm(int _cost)
    {
        if (gameManager.Gold < _cost)
            return false;
        else
            return true;
    }

    public void Debugging() => Debug.Log("Test" +
        "");



}
