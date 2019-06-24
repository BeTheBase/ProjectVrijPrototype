using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UpScript : MonoBehaviour
{
    RectTransform rect;
    public float Speed;
    Text text;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        text = GetComponent<Text>();
    }

    private void OnEnable()
    {
        rect = GetComponent<RectTransform>();
        transform.localPosition = Vector3.zero;
        text.CrossFadeColor(new Color32(231, 56, 56, 255), 0, false, true);
        StartCoroutine(ChangeColor());
    }

    private void OnDisable()
    {
        gameObject.SetActive(false);
    }

    IEnumerator ChangeColor()
    {
        yield return new WaitForSeconds(0.0f);
        while (text.color.a > 0)
        {
            rect.localPosition = new Vector3(rect.localPosition.x, rect.localPosition.y + Speed, rect.localPosition.z);
            GetComponent<Text>().CrossFadeColor(new Color32(231, 56, 56, 0), 0.9f, false, true);
            yield return new WaitForEndOfFrame();
            
        }
        gameObject.SetActive(false);
    }
}
