using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningStrike : StrikeBase
{
    public float TimeToWait = 1f;

    private void Start()
    {
        StartCoroutine(Deactivate(TimeToWait));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
            other.GetComponent<BaseEnemy>().Health -= StrikeDamage;
    }
}
