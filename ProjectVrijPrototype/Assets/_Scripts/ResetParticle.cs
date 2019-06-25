using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetParticle : MonoBehaviour
{
    ParticleSystem PS;

    private void Awake()
    {
        PS = GetComponent<ParticleSystem>();
    }
    private void OnEnable()
    {
        PS.Stop();
        PS.Play();
        StartCoroutine(StopAfterTime());
    }
    IEnumerator StopAfterTime()
    {
        yield return new WaitForSeconds(PS.main.startLifetime.constantMax);
        gameObject.SetActive(false);
    }
}
