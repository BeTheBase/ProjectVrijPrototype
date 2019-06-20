using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceStrike : StrikeBase
{
    public float AreaRange;
    public float TimeToWait;
    public float IceSlowCount = 3f;
    public float IceSlowDamage = 1f;
    public float TimeBetweenTicks = 1.3f;

    private bool IsInRange = false;

    private void Start()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z);
    }

    private void LateUpdate()
    {
        if (IsInRange)
        {
            StartCoroutine(Deactivate(TimeToWait));
            IsInRange = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            IsInRange = true;
            other.GetComponent<BaseEnemy>().ApplyIceSlow(IceSlowCount, IceSlowDamage, TimeBetweenTicks, StrikeEffect.name);
        }
    }
}
