using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectTile : MonoBehaviour
{
    public GameObject SelectedGameObject;
    public GameObject UISelectTowerPrefab;
    public GameObject UISelectUpgradePrefab;

    private Camera _mainCamera;

    // Use this for initialization
    public void Awake()
    {
        _mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            SetHudActive();
        }
    }

    //make the hud for object manipulation active and parent it on the right object
    private void SetHudActive()
    {
        SelectedGameObject = SelectObject();
        if (SelectedGameObject == null)
        {
            UISelectTowerPrefab.SetActive(false);
            UISelectUpgradePrefab.SetActive(false);
            return;
        }

        if (SelectedGameObject.tag == "TowerTile")
        {
            UISelectTowerPrefab.SetActive(true);
            UISelectTowerPrefab.transform.SetParent(SelectedGameObject.transform);
            UISelectTowerPrefab.transform.position = SelectedGameObject.transform.position + new Vector3(0, 1f, 0);
        }
        else if(SelectedGameObject.tag == "Tower")
        {
            UISelectUpgradePrefab.SetActive(true);
            UISelectUpgradePrefab.transform.SetParent(SelectedGameObject.transform);
            UISelectUpgradePrefab.transform.position = SelectedGameObject.transform.position + new Vector3(0, 1f, 0);
        }
        //UIPrefab.transform.localScale = new Vector3(1, 1, 1);
    }

    //return the object that is selected by mouse using a raycast
    public GameObject SelectObject()
    {
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
            if (hit.transform.tag == "TowerTile")
                return hit.transform.gameObject;
            else if (hit.transform.tag == "Tower")
                return hit.transform.gameObject;
            else
                return null;
        else
            return null;
    }
}
