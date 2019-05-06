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

    public void SetArcherTower()
    {
        index = 0;
        var towerCost = ReturnGoldCost(index);
        if (!ContinueProgramm(towerCost)) return;
        gameManager.Gold -= towerCost;
        Instantiate(towerDataManager.TowerDatas[index].Tower, transform.position - Vector3.up, Quaternion.identity);
        this.gameObject.SetActive(false);
    }

    public void SetBombTower()
    {
        index = 1;
        var towerCost = ReturnGoldCost(index);
        if (!ContinueProgramm(towerCost)) return;
        gameManager.Gold -= towerCost;
        Instantiate(towerDataManager.TowerDatas[index].Tower, transform.position - Vector3.up, Quaternion.identity);
        this.gameObject.SetActive(false);
    }

    public void SetMagicTower()
    {
        index = 2;
        var towerCost = ReturnGoldCost(index);
        if (!ContinueProgramm(towerCost)) return;
        gameManager.Gold -= towerCost;
        Instantiate(towerDataManager.TowerDatas[index].Tower, transform.position - Vector3.up, Quaternion.identity);
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
