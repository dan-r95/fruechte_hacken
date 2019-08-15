using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialItemsBehaviour : MonoBehaviour
{

     public GameObject fracturedFruit;

     public GameObject originalObject;
    public GameObject fracturedObject;
    private GameObject origObj;

    // Start is called before the first frame update  void Start()
     void Start()
    {
    }

    void Update()
    {
      
    }

    public void SpawnFracturedObject(){
           GameObject.Instantiate(fracturedFruit, gameObject.transform.position, transform.rotation);
            Destroy(gameObject);
            //fractObj.GetComponent<ExplodeItemScript>().ExplodeItem();
    }

     public void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.collider.tag == "Player")
        {
            Debug.Log("Collided!");
            SpawnFracturedObject();
          
            //audioManager = FindObjectOfType<AudioManager>();
           // audioManager.playSpecialSound();
        }

    }
}
