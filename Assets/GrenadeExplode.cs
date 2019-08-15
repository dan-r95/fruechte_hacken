using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeExplode : MonoBehaviour
{

    public GameObject explosionEffect;

    bool hasExploded = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision collisionInfo)
    {
        if (!hasExploded)
        {
            Debug.Log("Boom");
            Explode();
        }
    }
    void Explode()
    {
        Instantiate(explosionEffect, transform.position, transform.rotation);
        Destroy(gameObject);  // grenade itself
        hasExploded = true;
    }
}
