using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseEnemy : MonoBehaviour
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
        agent.SetDestination((gameManager.EndPosition.position));
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

    private void LateUpdate()
    {
        if (Vector3.Distance(gameManager.EndPosition.position, transform.position) < 5f)
            MadeIt();
    }

    private void Die()
    {
        GameObject Effect = objectPooler.SpawnFromPool("Blood", transform.position, Quaternion.identity);
        enemySpawner.EnemiesAlive--;
        gameManager.Gold += GoldGiven;
        gameObject.SetActive(false);
    }

    private void MadeIt()
    {
        enemySpawner.EnemiesAlive--;
        gameObject.SetActive(false);
        gameManager.Lives--;
    }
}
