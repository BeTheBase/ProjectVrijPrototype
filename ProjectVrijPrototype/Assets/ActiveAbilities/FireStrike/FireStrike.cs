using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireStrike : BaseProjectile
{
    public float AreaRange;
    public float TimeToWait = 1f;
    public float BurnTicksAmount = 3f;
    public float BurnDamage = 1f;
    public float TimeBetweenTicks = 1.3f;

    private void Start()
    {
        StartCoroutine(Deactivate(TimeToWait));
        transform.position = new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z);
    }

    private IEnumerator Deactivate(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, AreaRange);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
            other.GetComponent<BaseEnemy>().ApplyBurn(BurnTicksAmount, BurnDamage, TimeBetweenTicks);
    }
}
