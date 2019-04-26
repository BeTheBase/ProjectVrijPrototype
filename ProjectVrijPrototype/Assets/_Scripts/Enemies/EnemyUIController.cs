using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemyUIController : MonoBehaviour
{
    private BaseEnemy baseEnemy;
    public Slider HealthBar;

    private void Start()
    {
        baseEnemy = transform.parent.GetComponent<BaseEnemy>();
        HealthBar.maxValue = baseEnemy.MaxHealth;
        HealthBar.value = baseEnemy.Health;
    }

    // Update is called once per frame
    void Update()
    {
        HealthBar.value = baseEnemy.Health;
    }

    private void LateUpdate()
    {
        transform.LookAt(Camera.main.transform);
        transform.Rotate(0,180,0);
    }
}
