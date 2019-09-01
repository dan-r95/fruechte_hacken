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
        GameObject fractObj = GameObject.Instantiate(fracturedFruit, gameObject.transform.position, transform.rotation);
        Destroy(gameObject);
        if (gameObject.name == "bomb_ST6TPHC Variant(Clone)" || gameObject.name == "asiabox(Clone)" )
        {
            fractObj.GetComponent<ExplodeItemScript>().ExplodeItem();
        }
        else
        {
            fractObj.GetComponent<ExplodeFruitsScript>().ExplodeFruits();
        }
    }

    public IEnumerator startActionBasedOnType()
    {
        manager = FindObjectOfType<GameManager>();
        Color color = new Color(244, 253, 251);
        switch (gameObject.name)
        {
            case "Banana NEW blue(Clone)":

                RenderSettings.fogColor = Color.blue;
                hintManager.showMultiplierText();
                manager.setMultiplier(3);
                yield return new WaitForSecondsRealtime(10f);
                manager.setMultiplier(1);
                RenderSettings.fogColor = color;
                break;
            case "Banana NEW ice(Clone)":
                RenderSettings.fogColor = Color.grey;
                hintManager.showSlowMoText();
                manager.enableSlowMo();
                yield return new WaitForSecondsRealtime(10f);
                manager.disableSlowMo();
                RenderSettings.fogColor = color;
                break;
            case "Banana NEW yellow(Clone)":
                RenderSettings.fogColor = Color.yellow;
                hintManager.showFrenzyText();
                manager.enableFrenzyMode();
                yield return new WaitForSecondsRealtime(10f);
                manager.disableFrenzyMode();
                RenderSettings.fogColor = color;
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
