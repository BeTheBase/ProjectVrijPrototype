using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Standard : MonoBehaviour
{
    public float MaxHealth;

    public float Health;

    public int GoldGiven;

    private NavMeshAgent agent;

    private EnemySpawner enemySpawner;
    private GameManager gameManager;
    private ObjectPooler objectPooler;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponentInChildren<NavMeshAgent>();
        enemySpawner = EnemySpawner.Instance;
        gameManager = GameManager.Instance;
        objectPooler = ObjectPooler.Instance;
        agent.SetDestination((gameManager.EndPosition));
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y <= -5)
        {
            enemySpawner.EnemiesAlive--;
            gameObject.SetActive(false);
        }
        if(Health <= 0)
        {
            Die();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "End")
        {
            enemySpawner.EnemiesAlive--;
            gameObject.SetActive(false);
        }
    }

    private void Die()
    {
        GameObject Effect = objectPooler.SpawnFromPool("Blood", transform.position, Quaternion.identity);
        enemySpawner.EnemiesAlive--;
        gameManager.Gold += GoldGiven;
        gameObject.SetActive(false);
    }
}
