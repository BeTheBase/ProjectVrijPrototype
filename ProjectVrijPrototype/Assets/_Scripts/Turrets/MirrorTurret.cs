using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class MirrorTurret : Turret
{
    [SerializeField]
    public LineRenderer lineRenderer;
    private NavMeshAgent currentEnemyAgent;
    public ParticleSystem SparkEffect;
    
    public override void Start()
    {
        base.Start();
        lineRenderer = GetComponent<LineRenderer>(); 
    }

    public override void Update()
    {
        base.Update();
        Laser();
    }

    void Laser()
    {
        lineRenderer.SetPosition(0, FirePoint.position);
        if (Target == null)
        {
            lineRenderer.enabled = false;
            SparkEffect.Stop();
            return;
        }
        if (Target != null)
        {
            if (!lineRenderer.enabled)
            {
                lineRenderer.enabled = true;
                SparkEffect.Play();
            }
            lineRenderer.SetPosition(1, Target.position);

            Vector3 dir = FirePoint.position - Target.position;

            SparkEffect.transform.position = Target.position + dir.normalized * 0.5f;

            SparkEffect.transform.rotation = Quaternion.LookRotation(dir);

            BaseEnemy EnemyScript = Target.GetComponent<BaseEnemy>();

            EnemyScript.GetComponent<BaseEnemy>().TakeDamage(Damage / 120);
            if (!EnemyScript.IsSlowed)
            {
                StartCoroutine(EnemyScript.Slow(0.6f, 2.5f));
            }
        }
    }
}
