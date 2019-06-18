using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewUIManager : MonoBehaviour
{
    private GameManager gameManager;
    private NewEnemySpawner enemySpawner;

    public Text GoldText;
    public Text LivesText;
    public Text WaveText;
    public Text WaveTimerText;
    public Image TimerImage;

    public float CurrentTime;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        enemySpawner = NewEnemySpawner.Instance;

        GoldText.text = "Gold:" + gameManager.Gold;
        LivesText.text = "Lives:" + gameManager.Lives;
        WaveText.text = "Wave:" + NewEnemySpawner.waveIndex;
        WaveTimerText.text = "Time:" + gameManager.Lives;

    }

    // Update is called once per frame
    void LateUpdate()
    {
        GoldText.text = "Gold:" + gameManager.Gold;
        LivesText.text = "Lives:" + gameManager.Lives;
        WaveText.text = "Wave:" + NewEnemySpawner.waveIndex + "/" + enemySpawner.EnemySpawners[0].Waves.Length;
        if (CurrentTime >= 0)
        {
            CurrentTime -= Time.deltaTime;
        }
        WaveTimerText.text = "Time:" + Mathf.RoundToInt(CurrentTime);
        if (NewEnemySpawner.waveIndex < enemySpawner.EnemySpawners[0].Waves.Length)
        {
            TimerImage.fillAmount = CurrentTime / enemySpawner.EnemySpawners[0].Waves[NewEnemySpawner.waveIndex].NextWaveTime;
        }
    }
}


