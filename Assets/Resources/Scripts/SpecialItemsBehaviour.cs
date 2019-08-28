using UnityEngine;

public class SpecialItemsBehaviour : MonoBehaviour
{

    public GameObject fracturedFruit;

    public GameObject originalObject;
    public GameObject fracturedObject;
    private GameObject origObj;

    AudioManager audioManager;
    GameManagerSurvival managerSurvival;
    GameManager manager;

    // Start is called before the first frame update  void Start()
    void Start()
    {
    }

    void Update()
    {

    }

    public void SpawnFracturedObject()
    {
        GameObject.Instantiate(fracturedFruit, gameObject.transform.position, transform.rotation);
        Destroy(gameObject);
        fracturedObject.GetComponent<ExplodeItemScript>().ExplodeItem();
    }

    public void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.collider.tag == "Player")
        {
            Debug.Log("Collided!");
            SpawnFracturedObject();

            audioManager = FindObjectOfType<AudioManager>();
            audioManager.playSpecialSound();
            if (managerSurvival != null)
            {
                managerSurvival = FindObjectOfType<GameManagerSurvival>();
                managerSurvival.addExtralife(+3);
            }
            else
            {
                manager = FindObjectOfType<GameManager>();
                //manager.showPowerUpBillboard();
                // do something
            }

            SpawnFracturedObject();

        }

    }
}
