using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombProjectile : BaseProjectile
{
    protected float Animation;
    private Vector3 TargetPosition;
    public SphereCollider Trigger;
    Vector3 dir;

    private void Start()
    {
        TargetPosition = Target.position;
        dir = Target.position - transform.position;        
    }

    // Update is called once per frame
    public override void Update()
    {
        if (Target == null)
        {
            gameObject.SetActive(false);
            return;
        }
        float distanceThisFrame = Speed * Time.deltaTime;
        transform.Translate(dir.normalized * distanceThisFrame * 0.35f, Space.World);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Ground" || collision.transform.tag == "Enemy")
        {
            print("dasd");

            Trigger.enabled = true;
            Trigger.enabled = false;
            gameObject.SetActive(false);
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            print("BOOP");
            Target.GetComponent<BaseEnemy>().Health -= Damage;

        }

    }

}
