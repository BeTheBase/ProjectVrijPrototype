using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class SelectTile : MonoBehaviour
{
    public static SelectTile Instance;
    public GameObject SelectedGameObject;
    public GameObject UISelectTowerPrefab;
    public GameObject UISelectUpgradePrefab;
    private Camera _mainCamera;
    private GameObject currentTowerUI;
    public GameObject currentTowerInfo;
    private List<SetTowerButton> setTowerButtons;


    // Use this for initialization
    public void Awake()
    {
        Instance = this;
        _mainCamera = Camera.main;
    }

    private void Start()
    {
        setTowerButtons = new List<SetTowerButton>();
        for(int i = 0; i < UISelectTowerPrefab.transform.childCount; i++)
        {
            print(i);
            setTowerButtons.Add(UISelectTowerPrefab.transform.GetChild(i).GetComponent<SetTowerButton>());
        }
        print(setTowerButtons.Count);
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
            if (!UpgradeTowerManager.CheckFillBarStatus) return;
            UISelectTowerPrefab.SetActive(false);
            UISelectUpgradePrefab.SetActive(false);
            if(currentTowerUI != null)
            {
                currentTowerUI.SetActive(false);
            }
            if (currentTowerInfo != null)
            {
                currentTowerInfo.SetActive(false);
            }
            foreach (SetTowerButton button in setTowerButtons)
            {
                if(button != null)
                    button.ResetButton();
            }
            return;
        }

        if (SelectedGameObject.tag == "TowerTile")
        {
            FindObjectOfType<AudioManager>().Play("TowerPlacement");

            if (UISelectTowerPrefab == null) return;
            UISelectTowerPrefab.SetActive(true);
            UISelectTowerPrefab.transform.SetParent(SelectedGameObject.transform);
            UISelectTowerPrefab.transform.position = SelectedGameObject.transform.position + new Vector3(0, 1f, 0);
        }
        else if(SelectedGameObject.tag == "Tower")
        {
            FindObjectOfType<AudioManager>().Play("TowerPlacement");
    
            UISelectTowerPrefab.SetActive(false);
            if (UISelectUpgradePrefab == null) return;
            if (!UpgradeTowerManager.CheckFillBarStatus) return;
            
            UISelectUpgradePrefab.transform.SetParent(SelectedGameObject.transform);
            UISelectUpgradePrefab.transform.position = SelectedGameObject.transform.position + new Vector3(0, 3f, 0);
            UISelectUpgradePrefab.SetActive(true);

            //TowerUI
            if (currentTowerUI != null)
            {
                currentTowerUI.SetActive(false);
            }
            currentTowerUI = SelectedGameObject.GetComponent<Turret>().TowerUI;
            currentTowerUI.SetActive(true);
        }
        if(SelectedGameObject.name == "Empty")
        {
            if(currentTowerUI != null)
            {
                currentTowerUI.SetActive(false);
            }
            Destroy(SelectedGameObject);
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
            {
                return hit.transform.gameObject;

            }
            else if (hit.transform.tag == "Tower")
            {
                return hit.transform.gameObject;
            }
            else if(EventSystem.current.IsPointerOverGameObject())
            {
                GameObject Empty = new GameObject("Empty");
                return Empty;
            }
            else
            {
                return null;
            }
        else
        {
            return null;
        }
    }
}
