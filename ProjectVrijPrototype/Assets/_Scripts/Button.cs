using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Button : MonoBehaviour
{
    internal object onClick;
    [SerializeField]
    private Image button;

    public UnityEvent unityEvent;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject == button.gameObject)
                {
                    Button button = hit.transform.gameObject.GetComponent<Button>();
                    button.unityEvent.Invoke();
                }
                else
                    return;
            }
        }
    }
}

