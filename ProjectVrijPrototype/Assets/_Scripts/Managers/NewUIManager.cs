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
    public Image TimerImage;
    public static NewUIManager Instance;

    public float CurrentTime;

    private void Awake()
    {
        Instance = this;

    }

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        enemySpawner = NewEnemySpawner.Instance;

        GoldText.text = "" + gameManager.Gold;
        LivesText.text = "" + gameManager.Lives;
        WaveText.text = "" + NewEnemySpawner.waveIndex;

        CurrentTime = 0;

    }

    // Update is called once per frame
    void LateUpdate()
    {
        GoldText.text = "" + gameManager.Gold;
        LivesText.text = "" + gameManager.Lives;
        WaveText.text = "" + NewEnemySpawner.waveIndex + "/" + enemySpawner.EnemySpawners[0].Waves.Length;
        if (CurrentTime > 0)
        {
            CurrentTime -= Time.deltaTime;
        }
        if(NewEnemySpawner.waveIndex >= 1)
        {
            if (NewEnemySpawner.waveIndex < enemySpawner.EnemySpawners[0].Waves.Length)
            {
                TimerImage.fillAmount = CurrentTime / enemySpawner.EnemySpawners[0].Waves[NewEnemySpawner.waveIndex - 1].NextWaveTime;
            }
        }
    }
}


