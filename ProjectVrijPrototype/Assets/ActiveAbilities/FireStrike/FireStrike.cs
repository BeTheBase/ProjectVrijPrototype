using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireStrike : BaseProjectile
{
    public float AreaRange;
    public float TimeToWait = 1f;

    private void OnEnable()
    {
        StartCoroutine(Deactivate(TimeToWait));
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
            other.GetComponent<BaseEnemy>().Health -= Damage;
    }
}
