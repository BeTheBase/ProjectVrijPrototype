using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseEnemy : MonoBehaviour
{
    public float Speed;

    [HideInInspector]
    public int NextPoint = 0;

    public float MaxHealth;
    public float Health;

    public float MaxShield;
    public float Shield;

    public int GoldGiven;

    public NewEnemySpawner enemySpawner;
    public GameManager gameManager;
    public ObjectPooler objectPooler;
    public WavePath WavePath;

    public bool IsSlowed;

    public bool HasShield;

    // Start is called before the first frame update
    public virtual void Start()
    {
        enemySpawner = NewEnemySpawner.Instance;
        gameManager = GameManager.Instance;
        objectPooler = ObjectPooler.Instance;
        NextPoint = 0;
        Health = MaxHealth;
        Shield = MaxShield;
    }

    // Update is called once per frame
    public virtual void Update()
    {
        Vector3 nextPointPosition;
        nextPointPosition = new Vector3(WavePath.EnemyMovePoints[NextPoint].position.x, transform.position.y, WavePath.EnemyMovePoints[NextPoint].position.z);
        //Move from point to point
        if (NextPoint < WavePath.EnemyMovePoints.Count)
        {
            if (Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(WavePath.EnemyMovePoints[NextPoint].position.x, 0, WavePath.EnemyMovePoints[NextPoint].position.z)) < 0.1f)
            {
                NextPoint++;
            }


            transform.position = Vector3.MoveTowards(transform.position, nextPointPosition, Speed * Time.deltaTime);
            transform.LookAt(nextPointPosition);
        }


        //Disable if fall from map
        if (transform.position.y <= -5)
        {
            enemySpawner.EnemiesAlive--;
            gameObject.SetActive(false);
        }

        //If health is less than 0 execute Die function
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
        float baseSpeed = Speed;
        Speed *= slowMultiplier;
        yield return new WaitForSeconds(slowTime);
        Speed = baseSpeed;
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

    public void TakeDamage(float Damage)
    {
        if(HasShield && Shield > 0)
        {
            Shield -= Damage;
        }
        else
        {
            Health -= Damage;
        }
    }

    public void ApplyBurn(float ticks, float damage, float timeBetween)
    {
        StartCoroutine(Burn(ticks, damage, timeBetween));
    }

    private IEnumerator Burn(float burnTicks, float burnDamage, float timeBetween)
    {
        while(burnTicks >0)
        {
            burnTicks--;
            TakeDamage(burnDamage);
            yield return new WaitForSeconds(timeBetween);
        }
    }
}
