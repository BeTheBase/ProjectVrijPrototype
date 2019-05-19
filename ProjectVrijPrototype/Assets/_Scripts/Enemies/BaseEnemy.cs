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

    public bool IsSlowed;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponentInChildren<NavMeshAgent>();
        enemySpawner = EnemySpawner.Instance;
        gameManager = GameManager.Instance;
        objectPooler = ObjectPooler.Instance;
    }

    // Update is called once per frame
    public virtual void Update()
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

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "End")
        {
            enemySpawner.EnemiesAlive--;
            gameObject.SetActive(false);
        }
    }

    public void Die()
    {
        objectPooler.SpawnFromPool("Blood", transform.position, Quaternion.identity);
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

    public IEnumerator Slow(float slowMultiplier, float slowTime)
    {
        IsSlowed = true;
        float baseSpeed = agent.speed;
        agent.speed *= slowMultiplier;
        yield return new WaitForSeconds(slowTime);
        agent.speed = baseSpeed;
        IsSlowed = false;
    }

    public void Heal(float healAmount)
    {
        if(healAmount <= (MaxHealth - Health))
        {
            Health += healAmount;
        }
        else
        {
            Health = MaxHealth;
        }       
    }
}
