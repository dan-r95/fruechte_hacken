using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemController : MonoBehaviour
{
    // Start is called before the first frame update
    public ParticleSystem particleEmitter;

    void Start()
    {
        StartCoroutine(startParticlesAfterLoading());

    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator startParticlesAfterLoading()
    {
          // turn the particle system off at startup
        yield return new WaitForSeconds(2); // wait for 5 seconds
        particleEmitter.Play(true); // turn the particle system on
    }

}
