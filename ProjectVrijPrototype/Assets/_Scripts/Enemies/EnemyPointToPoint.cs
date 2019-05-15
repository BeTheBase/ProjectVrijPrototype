using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPointToPoint : MonoBehaviour
{
    public MovePoints AllMovePoints;
    public int NextPoint = 0;
    public float EnemyWalkSpeed = 5f;

    public float MaxHealth;

    public float Health;

    public int GoldGiven;

    private EnemySpawner enemySpawner;
    private GameManager gameManager;
    private ObjectPooler objectPooler;

    public bool IsSlowed;


    // Start is called before the first frame update
    void Start()
    {
        AllMovePoints = GameObject.FindGameObjectWithTag("MovePoints").GetComponent<MovePoints>();
        enemySpawner = EnemySpawner.Instance;
        gameManager = GameManager.Instance;
        objectPooler = ObjectPooler.Instance;
        NextPoint = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, AllMovePoints.EnemyMovePoints[NextPoint].position) < 0.1f)
            NextPoint++;

        transform.position = Vector3.MoveTowards(transform.position, AllMovePoints.EnemyMovePoints[NextPoint].position, EnemyWalkSpeed * Time.deltaTime);
        if (transform.position.y <= -5)
        {
            enemySpawner.EnemiesAlive--;
            gameObject.SetActive(false);
        }
        if (Health <= 0)
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
        if (collision.transform.tag == "End")
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
        float baseSpeed = EnemyWalkSpeed;
        EnemyWalkSpeed *= slowMultiplier;
        yield return new WaitForSeconds(slowTime);
        EnemyWalkSpeed = baseSpeed;
        IsSlowed = false;
    }
}
