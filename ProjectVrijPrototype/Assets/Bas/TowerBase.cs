using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Towers
{
    public abstract class TowerBase : MonoBehaviour
    {
        public float AttackPower;
        public float AttackRange;
        public float CostAmount;
        public float CoolDown;
        public GameObject TowerPrefab { get; set; }

        public void Init()
        {

        }
    }
}
