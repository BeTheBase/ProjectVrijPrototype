using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public Transform Target;

    //Public variables
    public float Range = 100f;
    public float FireRate;
    public float Damage;

    private GameObject Enemies;
    [HideInInspector]
    public ObjectPooler objectPooler;
    private float FireCooldown;
    public Transform FirePoint;

    // Start is called before the first frame update

    private void Awake()
    {
        objectPooler = ObjectPooler.Instance;
    }

    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.25f);
        InvokeRepeating("CheckTargetStatus", 0f, 0.05f);

        FireCooldown = 0;
    }

    //Update the current Target.
    void UpdateTarget()
    {
        GameObject[] Enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach(GameObject enemy in Enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if(distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if(nearestEnemy != null && shortestDistance <= Range)
        {
            Target = nearestEnemy.transform;
        }
    }

    //Check if Target is still alive to prevent shooting at nothing.
    void CheckTargetStatus()
    {
        if(Target != null)
        {
            if(!Target.gameObject.activeSelf)
            {
                Target = null;
            }
        }
    }

    void Update()
    {
        if(FireCooldown >= 0)
        {
            FireCooldown -= Time.deltaTime;
        }
        if(Target == null)
        {
            return;
        }
        if (FireCooldown <= 0f)
        {
            Shoot();
            FireCooldown = 1 / FireRate;
        }
        transform.LookAt(Target);

    }

    public virtual void Shoot()
    {    
        //This Method is meant to be overridden.
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Range);
    }
}
