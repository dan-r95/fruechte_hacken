using UnityEngine;
using System.Collections;

public class SpecialItemsBehaviour : MonoBehaviour
{

    public GameObject fracturedFruit;

    public GameObject originalObject;
    public GameObject fracturedObject;
    private GameObject origObj;

    AudioManager audioManager;
    GameManagerSurvival managerSurvival;
    GameManager manager;
    HintManager hintManager;

    // Start is called before the first frame update  void Start()
    void Start()
    {

        hintManager = FindObjectOfType<HintManager>();

        audioManager = FindObjectOfType<AudioManager>();
    }

    void Update()
    {


        if (gameObject == null && !gameObject.activeInHierarchy)
        {
            Debug.Log("disable");
            this.enabled = false;
        }
    }

    public void SpawnFracturedObject()
    {
        GameObject.Instantiate(fracturedFruit, gameObject.transform.position, transform.rotation);
        Destroy(gameObject);
        fracturedObject.GetComponent<ExplodeItemScript>().ExplodeItem();
    }

    public IEnumerator startActionBasedOnType()
    {
        manager = FindObjectOfType<GameManager>();
        switch (gameObject.name)
        {
            case "Banana blue":
                hintManager.showMultiplierText(); 
                manager.setMultiplier(3);
                yield return new WaitForSecondsRealtime(10f);
                manager.setMultiplier(3);
                break;
            case "Banana ice":
                hintManager.showSlowMoText();
                manager.enableSlowMo();
                yield return new WaitForSecondsRealtime(10f);
                manager.disableSlowMo();
                break;
            case "Banana yellow":
                hintManager.showFrenzyText(); 
                manager.enableFrenzyMode();
                yield return new WaitForSecondsRealtime(10f);
                manager.disableFrenzyMode();
                break;
        }
    }

    public void OnCollisionEnter(Collision collisionInfo)
    {
        Debug.Log(collisionInfo.collider.tag);
        if (collisionInfo.collider.tag == "Player")
        {
            Debug.Log("Collided!");
            SpawnFracturedObject();
            audioManager.playSpecialSound();
            if (managerSurvival != null)
            {
                managerSurvival = FindObjectOfType<GameManagerSurvival>();
                managerSurvival.addExtralife(+3);
            }
            else
            {
                StartCoroutine(startActionBasedOnType());

            }

            SpawnFracturedObject();

        }

    }
}
