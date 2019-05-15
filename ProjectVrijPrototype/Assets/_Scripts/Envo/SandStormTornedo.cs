using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class SandStormTornedo : Turret
{
    public float TwirlForce;
    public float UpwardsForce;
    public float SwirlForce;
    //public float PickRange;
    public float EnemyMoveRange;
    //public float DamageRange;
    public bool CanSwirl = false;
    //public string enemyTag = "Enemy";
    private TowerData upgradeTowerData;

    /*
    private ObjectPooler objectPooler;

    private Transform target;
    */
    public override void Start()
    {
        upgradeTowerData = TowerDataManager.Instance.TowerDatas.Find(t => t.Tower.name.Equals(this.gameObject.name));
        Range = upgradeTowerData.TowerRanges[0];
        Damage = upgradeTowerData.TowerDamages[0];
        FireRate = upgradeTowerData.TowerFireRates[0];
        InvokeRepeating("UpdateTarget", 0f, FireRate);
    }

    
    public override void UpdateTarget()
    {
        base.UpdateTarget();
        
        if(Target != null)
            StartCoroutine(WaitAndDie());
    }
    /*
    
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
    */

    private IEnumerator CheckIEnumerator(IEnumerator _IEnumerator)
    {
        yield return StartCoroutine(_IEnumerator);
    }
    private IEnumerator StartDelay(float delayTime)
    {
        TornedoSwirl();
        yield return new WaitForSeconds(delayTime);
    }

    public override void Shoot()
    {
        GameObject dustAttack = objectPooler.SpawnFromPool("DustProjectile", transform.position, transform.rotation);
        DustProjectille dustProjectilleScript = dustAttack.GetComponent<DustProjectille>();
        if (dustProjectilleScript != null)
        {
            dustProjectilleScript.Target = Target;
            dustProjectilleScript.Damage = Damage;
        }
    }

    private void TornedoPull()
    {
        Target.GetComponent<NavMeshAgent>().enabled = false;
        Vector3 direction = transform.position - Target.transform.position;

        Target.GetComponent<Rigidbody>().AddForce(direction * TwirlForce * (1 / Vector3.Distance(transform.position, Target.transform.position)), ForceMode.Impulse);
    }

    private void TornedoSwirl()
    {
        Target.GetComponent<NavMeshAgent>().enabled = false;
 
        Vector3 direction = transform.position - Target.transform.position;
        float distance = Vector3.Distance(transform.position, Target.transform.position) * EnemyMoveRange;
        float swirlForce = SwirlForce - distance;
        if (swirlForce < 0) swirlForce = 0;
        if (swirlForce > 3) swirlForce = 3;

        //Adding rigidbody and force to enemy
        Rigidbody targetBody = Target.gameObject.GetComponent<Rigidbody>();
        targetBody.isKinematic = false;
        targetBody.AddForce(direction * swirlForce);
        targetBody.AddForce(Vector3.up * (swirlForce * UpwardsForce));

        //Twirling
        Vector3 Foo = new Vector3(transform.position.x, 0, transform.position.x) - new Vector3(Target.transform.position.x, 0, Target.transform.position.z);
        Vector3 final;
        Foo = Foo * -1;
        final = Quaternion.Euler(0, 90, 0) * Foo;
        targetBody.AddForce(final * (swirlForce / TwirlForce));
        StartCoroutine(WaitAndDie());
    }

    private IEnumerator WaitAndDie()
    {
        yield return new WaitForSeconds(3f);
        Target.GetComponent<BaseEnemy>().Health = 0;
    }

    public override void Update()
    {
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y + 10 * Time.deltaTime, transform.localEulerAngles.z);
        if (Target == null) return;

        if (!CanSwirl) return;
        //StartCoroutine(CheckIEnumerator(StartDelay(10f)));
        TornedoPull();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Range);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, EnemyMoveRange);
    }
}
