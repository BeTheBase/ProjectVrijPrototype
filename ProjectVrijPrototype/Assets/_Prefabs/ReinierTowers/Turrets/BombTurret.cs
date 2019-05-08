using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombTurret : Turret
{
    public override void Shoot()
    {
        GameObject Bomb = objectPooler.SpawnFromPool("Bomb", FirePoint.position, transform.rotation);
        BombProjectile BombScript = Bomb.GetComponent<BombProjectile>();
        if (BombScript != null)
        {
            BombScript.Target = Target;
            BombScript.Damage = Damage;
            BombScript.FirePoint = FirePoint;
            //BombScript.CreateTrajectory();
        }
    }

}
