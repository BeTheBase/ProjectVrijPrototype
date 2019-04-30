using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectTowerManager : MonoBehaviour
{
    public static bool CheckTowerStatus = true;

    public string TowerCostString = "Tower Cost:";

    public Text[] TowerCostTextFields;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    private void Start()
    {
        if (TowerDataManager.Instance.TowerDatas != null)
        {
            for (int index = 0; index <= TowerCostTextFields.Length -1; index++)
            {
                TowerCostTextFields[index].text = TowerCostString + TowerDataManager.Instance.TowerDatas[index].GoldCost;
            }
        }
    }

    public void SetArcherTower()
    {
        Instantiate(TowerDataManager.Instance.TowerDatas[0].Tower, transform.position - Vector3.up, Quaternion.identity);
        this.gameObject.SetActive(false);
    }

    public void SetBombTower()
    {
        Instantiate(TowerDataManager.Instance.TowerDatas[1].Tower, transform.position - Vector3.up, Quaternion.identity);
        this.gameObject.SetActive(false);
    }

    public void SetMagicTower()
    {
        Instantiate(TowerDataManager.Instance.TowerDatas[2].Tower, transform.position - Vector3.up, Quaternion.identity);
        this.gameObject.SetActive(false);
    }

    public void Debugging() => Debug.Log("Test" +
        "");



}
