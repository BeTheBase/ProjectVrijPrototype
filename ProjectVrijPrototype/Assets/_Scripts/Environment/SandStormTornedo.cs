using System.Collections;
using UnityEngine;
using UnityEngine.AI;

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
    public bool CanSwirl = true;
    public string enemyTag = "Enemy";

    private ObjectPooler objectPooler;

    private Transform target;

    private void Start()
    {
        objectPooler = ObjectPooler.Instance;
        InvokeRepeating("UpdateTarget", 0f, DelayTime);
    }

    private void UpdateTarget()
    {
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
                Invoke("AttackEnemy", 0f);
            }

            if (nearestEnemy != null && shortestDistance <= PickRange && CanSwirl)
            {
                target = nearestEnemy.transform;
                StartCoroutine(WaitAndDie());
            }
        }
    }

    private IEnumerator StartDelay(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
    }

    private void AttackEnemy()
    {
        GameObject dustAttack = objectPooler.SpawnFromPool("DustProjectile", transform.position, transform.rotation);
        DustProjectille dustProjectilleScript = dustAttack.GetComponent<DustProjectille>();
        if (dustProjectilleScript != null)
        {
            dustProjectilleScript.Target = target;
            dustProjectilleScript.Damage = Damage;
        }
    }

    private void PullEnemy()
    {

    }

    private void TornedoPull()
    {
        Vector3 direction = transform.position - target.transform.position;

        target.GetComponent<Rigidbody>().AddForce(direction * TwirlForce * (1 / Vector3.Distance(transform.position, target.transform.position)), ForceMode.Impulse);
    }

    private void TornedoSwirl()
    { 
        Vector3 direction = transform.position - target.transform.position;
        float distance = Vector3.Distance(transform.position, target.transform.position) * MoveRange;
        float swirlForce = SwirlForce - distance;
        if (swirlForce < 0) swirlForce = 0;
        if (swirlForce > 3) swirlForce = 3;

        //Adding rigidbody and force to enemy
        Rigidbody targetBody = target.gameObject.GetComponent<Rigidbody>();
        targetBody.isKinematic = false;
        targetBody.AddForce(direction * swirlForce);
        targetBody.AddForce(Vector3.up * (swirlForce * UpwardsForce));

        //Twirling
        Vector3 Foo = new Vector3(transform.position.x, 0, transform.position.x) - new Vector3(target.transform.position.x, 0, target.transform.position.z);
        Vector3 final;
        Foo = Foo * -1;
        final = Quaternion.Euler(0, 90, 0) * Foo;
        targetBody.AddForce(final * (swirlForce / TwirlForce));
    }

    private IEnumerator WaitAndDie()
    {
        yield return new WaitForSeconds(3f);
        target.GetComponent<BaseEnemy>().Health = 0;
    }

    private void Update()
    {
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y + 10 * Time.deltaTime, transform.localEulerAngles.z);
        if (target == null) return;

        if (!CanSwirl) return;
        TornedoSwirl();
        //TornedoPull();
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
