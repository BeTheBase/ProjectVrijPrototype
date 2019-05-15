using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{

    public static UIManager Instance;
    private GameManager gameManager;
    private EnemySpawner enemySpawner;

    public Text GoldText;
    public Text LivesText;
    public Text WaveText;
    public Text WaveTimerText;

    public float CurrentTime;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        enemySpawner = EnemySpawner.Instance;

        GoldText.text = "Gold:" + gameManager.Gold;
        LivesText.text = "Lives:" + gameManager.Lives;
        WaveText.text = "Wave:" + EnemySpawner.waveIndex;
        WaveTimerText.text = "Time:" + gameManager.Lives;

    }

    // Update is called once per frame
    void LateUpdate()
    {
        GoldText.text = "Gold:" + gameManager.Gold;
        LivesText.text = "Lives:" + gameManager.Lives;
        WaveText.text = "Wave:" + EnemySpawner.waveIndex + "/" + enemySpawner.Waves.Length;
        if(CurrentTime >= 0)
        {
            CurrentTime -= Time.deltaTime;
        }
        WaveTimerText.text = "Time:" + Mathf.RoundToInt(CurrentTime);
    }
}
