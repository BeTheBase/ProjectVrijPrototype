using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectTowerManager : MonoBehaviour
{
    public GameObject ArcherTowerPrefab;
    public GameObject BombTowerPrefab;
    public GameObject MagicTowerPrefab;

    private void Start()
    {
        
    }

    public void SetArcherTower()
    {
        Instantiate(ArcherTowerPrefab, transform.position - Vector3.up, Quaternion.identity, transform.parent);
        this.gameObject.SetActive(false);
    }

    public void SetBombTower()
    {
        Instantiate(BombTowerPrefab, transform.position - Vector3.up, Quaternion.identity, transform.parent);
        this.gameObject.SetActive(false);
    }

    public void SetMagicTower()
    {
        Instantiate(MagicTowerPrefab, transform.position - Vector3.up, Quaternion.identity, transform.parent);
        this.gameObject.SetActive(false);
    }

    public void Debugging() => Debug.Log("Test" +
        "");



}
