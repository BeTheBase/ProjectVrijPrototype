using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowProjectile : BaseProjectile
{
    ObjectPooler objectPooler;
    public Transform FirePoint;


    private void Start()
    {
        objectPooler = ObjectPooler.Instance;
    }

    // Update is called once per frame
    public override void Update()
    {
        if(Target == null)
        {
            gameObject.SetActive(false);
            return;
        }
        transform.LookAt(Target);
        Vector3 dir = Target.position - transform.position;
        float distanceThisFrame = Speed * Time.deltaTime;

        if(dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    public override void HitTarget()
    {
        GameObject BloodHit = objectPooler.SpawnFromPool("BloodHit", FirePoint.position, transform.rotation);

        Vector3 dir = FirePoint.position - Target.position;

        BloodHit.transform.position = Target.position + dir.normalized * 0.5f;

        BloodHit.transform.rotation = Quaternion.LookRotation(dir);

        Target.GetComponent<BaseEnemy>().Health -= Damage;
        gameObject.SetActive(false);
    }
}
