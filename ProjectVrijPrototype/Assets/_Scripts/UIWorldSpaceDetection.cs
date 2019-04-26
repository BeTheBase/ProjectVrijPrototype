using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UIWorldSpaceDetection : MonoBehaviour
{
    [SerializeField]
    private UnityEvent unityEvent;

    void Update()
    {
        RaycastWorldUI();
    }

    void RaycastWorldUI()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PointerEventData pointerData = new PointerEventData(EventSystem.current);

            pointerData.position = Input.mousePosition;

            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerData, results);

            if (results.Count > 0)
            {
                //WorldUI is my layer name
                if (results[0].gameObject.layer == LayerMask.NameToLayer("WorldUI"))
                {
                    string dbg = "Root Element: {0} \n GrandChild Element: {1}";
                    Debug.Log(string.Format(dbg, results[results.Count - 1].gameObject.name, results[0].gameObject.name));
                    Button _button = results[results.Count - 1].gameObject.GetComponent<Button>();
                    _button.unityEvent.Invoke();

                    //Debug.Log("Root Element: "+results[results.Count-1].gameObject.name);
                    //Debug.Log("GrandChild Element: "+results[0].gameObject.name);
                    results.Clear();
                }
            }
        }
    }
}
