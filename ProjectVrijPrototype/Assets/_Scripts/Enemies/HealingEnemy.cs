using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingEnemy : BaseEnemy
{
    public float Range = 5;
    public float HealCooldown;
    public float HealAmount;
    private float healCooldownTimer;
    public List<GameObject> Targets;

    public override void Update()
    {
        base.Update();

        GameObject[] Enemies = GameObject.FindGameObjectsWithTag("Enemy");

        healCooldownTimer -= Time.deltaTime;

        if(healCooldownTimer <= 0)
        {
            Heal(Enemies);
            healCooldownTimer = HealCooldown;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Range);
    }

    void Heal(GameObject[] Enemies)
    {
        foreach (GameObject enemy in Enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy <= Range)
            {
                BaseEnemy baseEnemy = enemy.GetComponent<BaseEnemy>();
                if (baseEnemy.Health < baseEnemy.MaxHealth)
                {
                    GameObject HealingEffect = objectPooler.SpawnFromPool("HealingEffect", enemy.transform.localPosition, Quaternion.Euler(new Vector3(-90, 0, 0)));
                    HealingEffect.transform.parent = enemy.transform;
                }
                enemy.GetComponent<BaseEnemy>().Heal(HealAmount);
            }
        }
    }
}
