using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Towers
{
    public class Tower : TowerBase
    {
        private float attackPower;
        private float attackRange;
        private float costAmount;
        private float coolDown;
        private Transform targetEnemy;
        private float timeStamp = 0.0f;
        private GameObject towerPrefab;

        public GameObject TowerProjectile;

        private void Awake()
        {
            attackPower = AttackPower;
            attackRange = AttackRange;
            costAmount = CostAmount;
            towerPrefab = TowerPrefab;
            coolDown = CoolDown;
        }

        private void Start()
        {
            targetEnemy = GameObject.FindGameObjectWithTag("Enemy").transform;
        }

        public void FireProjectile()
        {
            transform.LookAt(targetEnemy);
            Instantiate(TowerProjectile, transform.position + Vector3.up, transform.rotation);
        }

        private void Update()
        {
            if (Time.time >= timeStamp && (targetEnemy.position - transform.position).magnitude < attackRange)
            {
                StartCoroutine(NewEnemy());
            }
        }

        private IEnumerator NewEnemy()
        {
            yield return new WaitForSeconds(1f);
            targetEnemy = GameObject.FindGameObjectWithTag("Enemy").transform;
            FireProjectile();
            timeStamp = Time.time + coolDown;
        }
    }
}
