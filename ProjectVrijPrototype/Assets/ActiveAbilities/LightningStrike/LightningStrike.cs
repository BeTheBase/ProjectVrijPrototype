using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningStrike : BaseProjectile
{
    public float AreaRange;
    public float TimeToWait = 1f;

    private void OnEnable()
    {
        StartCoroutine(Deactivate(TimeToWait));
    }

    private IEnumerator Deactivate(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, AreaRange);
    }

    /*
    private void OnEnable()
    {
        GameObject[] Enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (Enemies == null) return;
        foreach (GameObject enemy in Enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < AreaRange)
            {
                List<GameObject> enemies = new List<GameObject>();
                enemies.Add(enemy);
                if(enemies.Count < 2)
                {
                    foreach(GameObject en in enemies)
                    {
                        en.GetComponent<BaseEnemy>().Health -= Damage;
                    }
                }
            }
        }

    }*/

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
            other.GetComponent<BaseEnemy>().Health -= Damage;
    }
}
