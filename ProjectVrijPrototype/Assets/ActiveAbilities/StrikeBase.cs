using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrikeBase : MonoBehaviour
{
    public float StrikeDamage = 3f;
    public float StrikeRange = 20f;
    public GameObject StrikeEffect;
    public GameObject FireArea;

    public IEnumerator Deactivate(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, StrikeRange);
    }

}
