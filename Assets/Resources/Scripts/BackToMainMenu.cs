using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    AudioManager audioManager;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision collisionInfo)
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
        SceneManager.LoadScene("Main Menu");
    }
}
