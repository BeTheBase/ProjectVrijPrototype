using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    public void Continue()
    {
        gameObject.SetActive(false);
    }
    public void Options()
    {
        //Code for options here...
    }
    public void Quit()
    {
        SceneManager.LoadScene(1);
    }
}
