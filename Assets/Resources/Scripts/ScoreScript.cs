using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{

    GameManager manager;
    GameManagerSurvival managerSurvival;

    public bool isSurvialMode = false;
    Text scoreTxt;
    // Use this for initialization
    void Start()
    {
        scoreTxt = GetComponent<Text>();
        if (isSurvialMode)
        {
            managerSurvival = FindObjectOfType<GameManagerSurvival>();
            if (!managerSurvival.runningGame)
            {
                scoreTxt.text = "";
            }
        }
        else
        {
            manager = FindObjectOfType<GameManager>();
            if (!manager.runningGame)
            {
                scoreTxt.text = "";
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setScore(float scoreVal)
    {
        scoreTxt.text = scoreVal.ToString();
    }

}
