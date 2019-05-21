using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemyUIController : MonoBehaviour
{
    private BaseEnemy baseEnemy;
    public Slider HealthBar;
    public Slider ShieldBar;

    private void Start()
    {
        baseEnemy = transform.parent.GetComponent<BaseEnemy>();
        HealthBar.maxValue = baseEnemy.MaxHealth;
        HealthBar.value = baseEnemy.Health;
        if(ShieldBar != null)
        {
            ShieldBar.maxValue = baseEnemy.MaxShield;
            ShieldBar.value = baseEnemy.Shield;
        }
    }

    // Update is called once per frame
    void Update()
    {
        HealthBar.value = baseEnemy.Health;
        if (ShieldBar != null)
        {
            ShieldBar.value = baseEnemy.Shield;
        }
    }

    private void LateUpdate()
    {
        transform.LookAt(Camera.main.transform);
        transform.Rotate(0,180,0);
    }
}
