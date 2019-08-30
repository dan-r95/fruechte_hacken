using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeExplode : MonoBehaviour
{

    public GameObject explosionEffect;
    AudioManager audioManager;

    GameManagerSurvival managerSurvival;
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
        Debug.Log(hasExploded);
        if (!hasExploded && collisionInfo.collider.tag == "Player")
        {
            hasExploded = true;
            Debug.Log("Boom");
            // deduct 3 lives when bomb hits
            managerSurvival = FindObjectOfType<GameManagerSurvival>();
            managerSurvival.EndGame();
            Explode();


        }
    }
    void Explode()
    {

        Instantiate(explosionEffect, transform.position, transform.rotation);
        audioManager = FindObjectOfType<AudioManager>();
        audioManager.playExplosion();
        Destroy(gameObject);  // grenade itself

    }
}
