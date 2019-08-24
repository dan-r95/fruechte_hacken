using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class StartButtonSurvivalMode : MonoBehaviour
{

    // Use this for initialization
    GameManagerSurvival manager;
    AudioManager audioManager;
    void Start()
    {
        manager = FindObjectOfType<GameManagerSurvival>();
    }

    public void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.collider.tag == "Player")
        {
         StartCoroutine(loadScene());
        }
    }

      IEnumerator loadScene()
    {
            audioManager = FindObjectOfType<AudioManager>();
            audioManager.playStartSound();
            yield return new WaitForSeconds(1.5f);
            // darken the screen
            SceneManager.LoadSceneAsync("SurvivalMode");
    }
}
