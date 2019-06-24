using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectTowerManager : MonoBehaviour
{
    public static bool CheckTowerStatus = true;

    public string TowerCostString = "Tower Cost:";

    public Text[] TowerCostTextFields;

    public static SelectTowerManager Instance;
    private GameManager gameManager;
    private TowerDataManager towerDataManager;
    private GameObject parentGameObject;
    public GameObject currentCheckmark;
    public GameObject currentTowerInfo;
    public Button currentButton;
    ObjectPooler objectPooler;

    private void Awake()
    {
        objectPooler = ObjectPooler.Instance;
        Instance = this;
    }

    private void Start()
    {
        gameManager = GameManager.Instance;
        towerDataManager = TowerDataManager.Instance;
        gameObject.SetActive(false);
        for(int i = 0; i < TowerCostTextFields.Length; i++)
        {
            TowerCostTextFields[i].text = "" + towerDataManager.TowerDatas[i].GoldCost[0];
        }
    }

    public void PlaceTower(int index)
    {
        int towerCost = ReturnGoldCost(index);
        if (!ContinueProgramm(towerCost))
        {
            GameObject text = objectPooler.SpawnFromPool("NotEnough", transform.position, transform.rotation);
            text.transform.parent = transform;
            return;
        }
        parentGameObject = transform.parent.gameObject;
        if(parentGameObject.transform.childCount < 2 || !parentGameObject.transform.GetChild(1).gameObject.activeSelf)
        {      
            parentGameObject.GetComponent<CapsuleCollider>().enabled = false;
            gameManager.Gold -= towerCost;
            GameObject towerPlaceHolder = Instantiate(towerDataManager.TowerDatas[index].Tower, transform.position - Vector3.up, Quaternion.identity);
            towerPlaceHolder.transform.SetParent(parentGameObject.transform);
            var towerTurretScript = towerPlaceHolder.GetComponent<Turret>();
            towerTurretScript.Damage = towerDataManager.TowerDatas[index].TowerDamages[0];
            towerTurretScript.Range = towerDataManager.TowerDatas[index].TowerRanges[0];
            towerTurretScript.FireRate = towerDataManager.TowerDatas[index].TowerFireRates[0];
        }
        gameObject.SetActive(false);
    }

    private int ReturnGoldCost(int _index)
    {
        return towerDataManager.TowerDatas[_index].GoldCost[0];
    }

    private bool ContinueProgramm(int _cost)
    {
        if (gameManager.Gold < _cost)
        {
            return false;
        }
        else
            return true;
    }

    public void Debugging() => Debug.Log("Test" +
        "");



}
