using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceStrike : StrikeBase
{
    public float AreaRange;
    public float TimeToWait;
    public float IceSlowCount;
    public float IceSlowDamage;
    public float TimeBetweenTicks;
    public float IceSlowMultiplier;

    private bool IsInRange = false;

    private void Start()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z);
        FireArea.SetActive(false);
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
            other.GetComponent<BaseEnemy>().ApplyIceSlow(IceSlowCount, IceSlowDamage, TimeBetweenTicks, StrikeEffect.name, IceSlowMultiplier);
            FireArea.SetActive(true);
            GlowOrb.SetActive(false);
        }
    }
}
