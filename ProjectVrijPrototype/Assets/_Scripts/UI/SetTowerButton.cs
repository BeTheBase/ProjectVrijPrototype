using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SetTowerButton : MonoBehaviour
{
    SelectTowerManager selectTowerManager;
    Button towerButton;
    public GameObject TowerBlue;
    public GameObject CurrentTowerBlue;
    private SelectTile selectTile;

    private void Awake()
    {
        selectTowerManager = GetComponentInParent<SelectTowerManager>();
        towerButton = GetComponent<Button>();
    }

    public void Confirmation()
    {
        selectTile = SelectTile.Instance;

        if(selectTile.currentTowerBlue != null)
        {
            selectTile.currentTowerBlue.SetActive(false);
        }
        selectTile.currentTowerBlue = TowerBlue;
        TowerBlue.transform.position = new Vector3(transform.parent.position.x, transform.parent.position.y -1f, transform.parent.position.z);
        TowerBlue.SetActive(true);
        print(selectTile);
        selectTile.currentTowerBlue = TowerBlue;


        FindObjectOfType<AudioManager>().Play("TowerPlacement");
        //Disable previous checkmark
        if(selectTowerManager.currentCheckmark != null)
        {
            selectTowerManager.currentCheckmark.SetActive(false);
        }
        if (selectTowerManager.currentButton != null)
        {
            selectTowerManager.currentButton.enabled = true;
        }
        if(selectTowerManager.currentTowerInfo != null)
        {
            selectTowerManager.currentTowerInfo.SetActive(false);
        }
        selectTowerManager.currentCheckmark = transform.Find("CheckMark").gameObject;
        selectTowerManager.currentTowerInfo = transform.Find("TowerStats").gameObject;
        selectTowerManager.currentCheckmark.SetActive(true);
        selectTowerManager.currentTowerInfo.SetActive(true);
        selectTowerManager.currentButton = towerButton;
        selectTowerManager.currentButton.enabled = false;
    }

    public void ResetButton()
    {
        if(towerButton != null)
        {
            towerButton.enabled = true;
        }
        if (selectTowerManager.currentCheckmark != null)
        {
            selectTowerManager.currentCheckmark.SetActive(false);
        }
        if(CurrentTowerBlue != null)
        {
            CurrentTowerBlue.SetActive(false);
        }
    }
}
