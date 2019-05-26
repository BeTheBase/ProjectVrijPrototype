using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SetTowerButton : MonoBehaviour
{
    SelectTowerManager selectTowerManager;
    Button towerButton;

    private void Awake()
    {
        selectTowerManager = GetComponentInParent<SelectTowerManager>();
        towerButton = GetComponent<Button>();
    }

    public void Confirmation()
    {
        //Disable previous checkmark
        if(selectTowerManager.currentCheckmark != null)
        {
            print("hi");
            selectTowerManager.currentCheckmark.SetActive(false);
        }
        if (selectTowerManager.currentButton != null)
        {
            selectTowerManager.currentButton.enabled = true;
        }
        selectTowerManager.currentCheckmark = transform.GetChild(0).gameObject;
        selectTowerManager.currentCheckmark.SetActive(true);
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
    }
}
