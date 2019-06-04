using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    void Update()
    {
        FindObjectOfType<AudioManager>().Play("theme1");
    }
}
