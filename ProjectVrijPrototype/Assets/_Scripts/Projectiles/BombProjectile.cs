using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombProjectile : BaseProjectile
{
    protected float Animation;
    public SphereCollider Trigger;
    Vector3 dir;
    public Transform Trajectory;
    public Transform FirePoint;
    private MeshRenderer meshRenderer;
    private Rigidbody rigidBody;
    private ObjectPooler objectPooler;
    bool hit;

    private void Start()
    {
        objectPooler = ObjectPooler.Instance;
        rigidBody = GetComponent<Rigidbody>();
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.enabled = true;
        Trigger.enabled = false;
    }


    //Trajectory for parabola
    /*
    public void CreateTrajectory()
    {
        Trajectory.GetChild(0).position = FirePoint.positio;
        Trajectory.GetChild(1).position = new Vector3(Vector3.Distance(transform.position, Target.transform.position) / 2, transform.position.y + 4f, Vector3.Distance(transform.position, Target.transform.position) / 2);
        Trajectory.GetChild(2).position = Target.transform.position;
    }
    */

    // Update is called once per frame
    public override void Update()
    {
        if (Target == null)
        {
            gameObject.SetActive(false);
            return;
        }
        Vector3 dir = Target.position - transform.position;

        float distanceThisFrame = Speed * Time.deltaTime;


        if (dir.magnitude <= distanceThisFrame && !hit)
        {
            hit = true;
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame * 0.35f, Space.World);

    }

    public override void HitTarget()
    {
        StartCoroutine(Explode());
    }
    
    IEnumerator Explode()
    {
        rigidBody.isKinematic = true;
        GameObject Effect = objectPooler.SpawnFromPool("Explosion", transform.position, Quaternion.identity);
        Effect.transform.localScale = new Vector3(Trigger.radius, Trigger.radius, Trigger.radius) / 7f;
        meshRenderer.enabled = false;
        Trigger.enabled = true;
        yield return new WaitForSeconds(0.25f);
        Trigger.enabled = false;
        gameObject.SetActive(false);
        meshRenderer.enabled = true;
        rigidBody.isKinematic = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Trigger.radius /5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<BaseEnemy>().Health -= Damage;
    }

}
