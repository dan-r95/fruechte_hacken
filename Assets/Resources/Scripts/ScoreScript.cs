using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour {

    GameManager manager;
    Text scoreTxt;
	// Use this for initialization
	void Start () {
        scoreTxt = GetComponent<Text>();
        manager = FindObjectOfType<GameManager>();
        if (!manager.runningGame){
            scoreTxt.text="";
        }  
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void setScore(float scoreVal) {
        scoreTxt.text = scoreVal.ToString();
    }
        
}
