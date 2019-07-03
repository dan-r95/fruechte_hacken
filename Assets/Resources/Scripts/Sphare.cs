using UnityEngine;
using System.Collections;


public class Sphare : MonoBehaviour {

    Rigidbody rb;
    ConstantForce cf;
    GameManager manager;
    public GameObject fracturedFruit;
    ShowSplashImageCanvas splashimage;

    void Start() {
        rb = GetComponent<Rigidbody>();
        cf = GetComponent<ConstantForce>();
        //Debug.Log(rb);
        rb.AddForce(10f,0,0);
    }
	
	// Update is called once per frame
	void Update () {
        if (manager == null)
            manager = FindObjectOfType<GameManager>();

        if (splashimage == null)
            splashimage = FindObjectOfType<ShowSplashImageCanvas>();

        if ((transform.position.x > 5) || (!manager.runningGame && transform.position.x < 0))
        {
            Destroy(gameObject);
            if (manager.extralife > 0)
            {
                manager.addExtralife(-1);
            }

            switch (manager.extralife)
            {
                case 0:
                    splashimage.image10.enabled = true;
                    break;
                case 1:
                    splashimage.image9.enabled = true;
                    break;
                case 2:
                    splashimage.image8.enabled = true;
                    break;
                case 3:
                    splashimage.image7.enabled = true;
                    break;
                case 4:
                    splashimage.image6.enabled = true;
                    break;
                case 5:
                    splashimage.image5.enabled = true;
                    break;
                case 6:
                    splashimage.image4.enabled = true;
                    break;
                case 7:
                    splashimage.image3.enabled = true;
                    break;
                case 8:
                    splashimage.image2.enabled = true;
                    break;
                case 9:
                    splashimage.image1.enabled = true;
                    break;
                default:
                    break;
            }
        }

        if (manager.extralife <= 0)
        {
            manager.extralife = 0; // be sure we dont get a negative one
            manager.EndGame();
            return;
        }
    }

    public void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.collider.tag == "Player")
        {   
            GameObject fractObj = Instantiate(fracturedFruit, gameObject.transform.position, Quaternion.identity) as GameObject;
            Destroy(gameObject);
            fractObj.GetComponent<ExplodeFruitsScript>().ExplodeFruits();
            manager.addScore(1);
        }
        
    }
}
