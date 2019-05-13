using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableParticleSystem : MonoBehaviour
{
    public float EffectDuration;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DisableAfterTime());
    }

    IEnumerator DisableAfterTime()
    {
        yield return new WaitForSeconds(EffectDuration + 0.1f);
        gameObject.SetActive(false);
    }
}
