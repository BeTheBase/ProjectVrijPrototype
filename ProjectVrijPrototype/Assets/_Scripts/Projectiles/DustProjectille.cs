using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustProjectille : BaseProjectile
{
    // Update is called once per frame
    public override void Update()
    {
        if (Target == null)
        {
            gameObject.SetActive(false);
            return;
        }
        transform.LookAt(Target);
        Vector3 dir = Target.position - transform.position;
        float distanceThisFrame = Speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    public override void HitTarget()
    {
        Target.GetComponent<BaseEnemy>().TakeDamage(Damage);
        gameObject.SetActive(false);
    }
}
