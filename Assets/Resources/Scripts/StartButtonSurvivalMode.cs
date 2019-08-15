using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButtonSurvivalMode : MonoBehaviour {

    public Vector3 posAdd;

	// Use this for initialization
    GameManagerSurvival manager;
	void Start () {
        manager = FindObjectOfType<GameManagerSurvival>();
	}

    public void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.collider.tag == "Player")
        {
            SceneManager.LoadScene("SurvivalMode");
        }
    }
}
