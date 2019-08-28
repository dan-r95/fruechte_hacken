using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
public class StartButtonTimeMode : MonoBehaviour
{

    // Use this for initialization
    GameManager manager;

    AudioManager audioManager;
    void Start()
    {
        manager = FindObjectOfType<GameManager>();
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
        yield return new WaitForSecondsRealtime(1.5f);
        // darken the screen
        SceneManager.LoadSceneAsync("TimeLimitMode");
    }
}
