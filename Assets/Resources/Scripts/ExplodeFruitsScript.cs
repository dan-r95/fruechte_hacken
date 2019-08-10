using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeFruitsScript : MonoBehaviour
{
    public GameObject particleEffect;
    public Vector3 particleEffectOffset;
    public float destroyDelay;
    public float minForce;
    public float maxForce;
    public float radius;

    public void ExplodeFruits()
    {
        if(particleEffect != null)
        {
            GameObject particleEffectFX = Instantiate(particleEffect, transform.position + particleEffectOffset, Quaternion.identity) as GameObject;
            Destroy(particleEffectFX, 5);
        }

        foreach (Transform t in transform)
        {
            var rb = t.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(Random.Range(minForce, maxForce), transform.position, radius);
            }

            Destroy(t.gameObject, destroyDelay);
        }
    }
}
