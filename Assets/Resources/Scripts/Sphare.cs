using UnityEngine;

public class Sphare : MonoBehaviour
{

    Rigidbody rb;
    ConstantForce cf;
    GameManager manager;
    GameManagerSurvival managerSurvival;

    public bool isSurivalMode = false;
    AudioManager audioManager;
    public GameObject fracturedFruit;
    ShowSplashImageCanvas splashimage;



    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cf = GetComponent<ConstantForce>();
        rb.AddForce(10f, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {

        if (isSurivalMode)
        {
            if (managerSurvival == null)
                managerSurvival = FindObjectOfType<GameManagerSurvival>();

            if (managerSurvival != null)
            {
                if (splashimage == null)
                    splashimage = FindObjectOfType<ShowSplashImageCanvas>();

                if ((transform.position.x > 5) || (!managerSurvival.runningGame && transform.position.x < 0))
                {
                    Destroy(gameObject);
                    if (managerSurvival.extralife > 0)
                    {
                        managerSurvival.addExtralife(-1);
                    }

                    switch (managerSurvival.extralife)
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

                if (managerSurvival.extralife <= 0)
                {
                    Debug.Log("oh not, not lives left :/ Game over");
                    managerSurvival.extralife = 0; // be sure we dont get a negative one
                    managerSurvival.EndGame();
                    return;
                }
            }
        }
        else
        {
            if (manager == null)
                manager = FindObjectOfType<GameManager>();
            if (manager != null)
            {
                if (splashimage == null)
                    splashimage = FindObjectOfType<ShowSplashImageCanvas>();

                if ((transform.position.x > 5) || (!manager.runningGame && transform.position.x < 0))
                {
                    Destroy(gameObject);
                    if (manager.extralife > 0)
                    {
                        manager.addExtralife(-1);
                    }


                }

            }
        }

    }

    public void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.collider.tag == "Player")
        {
            GameObject fractObj = Instantiate(fracturedFruit, gameObject.transform.position, Quaternion.identity) as GameObject;
            Destroy(gameObject);
            fractObj.GetComponent<ExplodeFruitsScript>().ExplodeFruits();
            if (isSurivalMode && managerSurvival != null)
            {
                managerSurvival.addScore(1);
            }
            else
            {
                if (manager != null)
                    manager.addScore(1);
            }


            // play splash music - randomly one of 2 sounds
            audioManager = FindObjectOfType<AudioManager>();
            audioManager.playSplash();
        }

    }
}
