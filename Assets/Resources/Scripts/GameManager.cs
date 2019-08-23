using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public float speed = 0.8f;
    private float startTime, journeyLength;
    public bool runningGame = false;
    public bool noclip = false;
    public int score = 0;
    public int turn = 0;
    ScoreScript scoreTxt;
    headlineTxt headlineTxt;
    public Text extralives;
    StartButtonTimeMode button1;
    StartButtonSurvivalMode button2;

    public int extralife;
    public int remaining_invulnarebility;


    public GameObject idleText, gameOverText;

    public ShowSplashImageCanvas splashgroup;



    void Awake()
    {
        scoreTxt = FindObjectOfType<ScoreScript>();
        button1 = FindObjectOfType<StartButtonTimeMode>();
        button2 = FindObjectOfType<StartButtonSurvivalMode>();
        headlineTxt = FindObjectOfType<headlineTxt>();
        // QualitySettings.vSyncCount = 1;
        // Sync framerate to monitors refresh rate
        Application.targetFrameRate = 60;

    }
    // Use this for initialization
    void Start()
    {
        if (!runningGame)
        {
            //extralives.text="";
            //headlineTxt.changeState(false);
            // idleText = GameObject.Find("IdleText");
            // idleText.gameObject.SetActive(true);
            //gameOverText = GameObject.Find("gameOverText");
            //gameOverText.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            noclip = !noclip;
        }
        if (Input.GetKeyDown(KeyCode.N) && !runningGame)
            newGame();

        if (remaining_invulnarebility > 0)
            remaining_invulnarebility--;
        if (runningGame)
            Time.timeScale += 0.000001f; //speeding up the game



    }

    public void newGame()
    {
        Debug.Log("NEW  GAME(Time)");
        headlineTxt.changeState(false);
        // dont display the header animation when game is started
        idleText = GameObject.Find("IdleText");
        // idleText.gameObject.SetActive(false);
        FruitSpawn fruitspawn = FindObjectOfType<FruitSpawn>();
        fruitspawn.isSurvialMode = false;
        fruitspawn.newGame();
        addScore(-score);
        turn = 0;
        extralife = 0;
        setExtralife(10);
        TimeManager timeManager = FindObjectOfType<TimeManager>();
        timeManager.startTimer();
        runningGame = true;
        Time.timeScale = 1;

    }
    public void addScore(int i)
    {
        score += i;
        scoreTxt.setScore(score);
    }

    public void setScore(int i)
    {
        score = i;
        scoreTxt.setScore(score);
    }

    public void setExtralife(int l)
    {
        extralife = l;
        // TODO delte
        extralives.text = extralife.ToString();
    }

    public void addExtralife(int l)
    {
        extralife += l;
        // TODO delete
        extralives.text = extralife.ToString();
    }


    public bool checkforItem()
    {
        //  Zusammenfassung:
        //Returns true, if there is an item going to be spawned
        if ((turn) % 3 == 0 && turn != 0)
            return true;
        return false;
    }

    public float propability()
    {
        //Returns a propability whats the chance of an high tier obstacle to spawn
        if (turn < 5)
            return 0;
        if (turn < 10)
            return 0.2f;
        return 0.4f;
    }

    public void increaseInvulnarebility(int i)
    {
        remaining_invulnarebility += i;
    }

    public void setInvulnarebility(int i)
    {
        remaining_invulnarebility = i;
    }



    public void EndGame()
    {
        runningGame = false;
        headlineTxt.changeState(true);
        //button1.transform.position = new Vector3(0.5f, 1.5f, -1.75f);
        gameOverText.SetActive(true);
        // add button to get back to the main menu

        Vector3 pos = new Vector3(-1.2f, 1f, 0f);
        GameObject gong = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/gong backToMain Variant"), pos, Quaternion.Euler(0, 90, 0));
        gong.transform.localScale = new Vector3(0.07f, 0.07f, 0.07f);
    }

}
