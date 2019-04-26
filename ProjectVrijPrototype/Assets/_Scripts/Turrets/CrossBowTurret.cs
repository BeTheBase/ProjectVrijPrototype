using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossBowTurret : Turret
{
    public override void Shoot()
    {
        GameObject Arrow = objectPooler.SpawnFromPool("Arrow", FirePoint.position, transform.rotation);
        ArrowProjectile ArrowScript = Arrow.GetComponent<ArrowProjectile>();
        if (ArrowScript != null)
        {
            ArrowScript.Target = Target;
            ArrowScript.Damage = Damage;
        }
    }
}
