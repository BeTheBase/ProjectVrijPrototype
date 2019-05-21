using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class MenuManager : MonoBehaviour
{
    public GameObject LoadingWheel;

    public void Play()
    {
        StartCoroutine(LoadingScreen());
    }

    IEnumerator LoadingScreen()
    {
        EventSystem.current.enabled = false;
        LoadingWheel.SetActive(true);
        yield return new WaitForSeconds(3);

        while (!SceneManager.LoadSceneAsync(1).isDone)
        {
            print("f");

            yield return new WaitForSeconds(0.1f);
        }
    }
}
