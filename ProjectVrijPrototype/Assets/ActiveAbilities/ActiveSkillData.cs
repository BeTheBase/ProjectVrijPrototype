using System;
using UnityEngine;

[Serializable]
public class ActiveSkillData 
{
    [SerializeField]
    public string name;
    [SerializeField]
    public GameObject Effect;
    [SerializeField]
    public float Damage;
    [SerializeField]
    public float AreaRange;
    [SerializeField]
    public bool IsReady;
    [SerializeField]
    public GameObject StrikeBluePrint;
}
