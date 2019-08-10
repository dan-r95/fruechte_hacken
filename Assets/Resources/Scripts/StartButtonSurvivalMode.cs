using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButtonSurvivalMode : MonoBehaviour {

    public Vector3 posAdd;

	// Use this for initialization
    GameManager manager;
	void Start () {
        manager = FindObjectOfType<GameManager>();
	}

    public void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.collider.tag == "Player")
        {
            SceneManager.LoadScene("SurvivalMode");
        }
    }
}
