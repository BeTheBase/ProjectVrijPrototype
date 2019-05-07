using System.Collections;
using UnityEngine;

public class SandStormTornedo : MonoBehaviour
{
    public float TwirlForce;
    public float UpwardsForce;
    public float SwirlForce;
    public float Damage;
    public float DelayTime;
    public float PickRange;
    public float MoveRange;
    public float DamageRange;

    public string enemyTag = "Enemy";

    private Transform target;

    private void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    private void UpdateTarget()
    {
        StartCoroutine(StartDelay(DelayTime));
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach(GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if(distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }

            if(nearestEnemy != null && shortestDistance <= DamageRange)
            {
                target = nearestEnemy.transform;
                //target.gameObject.AddComponent<Rigidbody>();
                AttackEnemy();
            }

            if (nearestEnemy != null && shortestDistance <= PickRange)
            {
                target = nearestEnemy.transform;
                //target.gameObject.AddComponent<Rigidbody>();
                TornedoSwirl();
            }
        }

    }

    private IEnumerator StartDelay(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
    }

    private void AttackEnemy()
    {
        var enemyScript = target.GetComponent<BaseEnemy>();
        enemyScript.Health -= Damage;
    }

    private void PullEnemy()
    {

    }

    private void TornedoSwirl()
    {
        Vector3 direction = transform.position - target.transform.position;
        float distance = Vector3.Distance(transform.position, target.transform.position) * MoveRange;
        float swirlForce = SwirlForce - distance;
        if (swirlForce < -5) swirlForce = -5;
        if (swirlForce > 5) swirlForce = 5;

        //Adding rigidbody and force to enemy
        Rigidbody targetBody = target.gameObject.GetComponent<Rigidbody>();
        target.gameObject.GetComponent<Rigidbody>().AddForce(direction * swirlForce);
        target.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * (swirlForce * UpwardsForce));

        //Twirling
        Vector3 Foo = new Vector3(transform.position.x, 0, transform.position.x) - new Vector3(target.transform.position.x, 0, target.transform.position.z);
        Vector3 final;
        Foo = Foo * -1;
        final = Quaternion.Euler(0, 90, 0) * Foo;
        target.gameObject.GetComponent<Rigidbody>().AddForce(final * (swirlForce / TwirlForce));

    }

    private void Update()
    {
        if (target == null) return;

        TornedoSwirl();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, DamageRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, PickRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, MoveRange);
    }
}
