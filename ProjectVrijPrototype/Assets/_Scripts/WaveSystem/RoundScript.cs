using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RoundScript : MonoBehaviour {

    public static int enemiesAlive = 0;

    public int round;

    public Text roundText;

	// Use this for initialization
	void Start ()
    {
        round = 1;
        roundText.text = "Round:" + round;
    }

    public void NextRound()
    {
        round++;
        roundText.text = "Round:" + round;
    }

    public IEnumerator RoundTimer()
    {
        yield return new WaitForSeconds(5);
        NextRound();
    }
}
