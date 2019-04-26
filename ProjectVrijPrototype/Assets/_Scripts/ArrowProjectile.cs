using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowProjectile : MonoBehaviour
{
    public Transform Target;

    public float Damage;
    public float Speed = 50f;
    // Update is called once per frame
    void Update()
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

    void HitTarget()
    {
        Target.GetComponent<BaseEnemy>().Health -= Damage;
        gameObject.SetActive(false);
    }
}
