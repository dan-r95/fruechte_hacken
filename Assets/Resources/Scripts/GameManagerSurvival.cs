using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManagerSurvival : MonoBehaviour
{

    public float speed = 1f;
    private float startTime, journeyLength;
    public bool runningGame = false;
    public bool noclip = false;
    public int score = 0;
    public int turn = 0;
    ScoreScript scoreTxt;
    headlineTxt headlineTxt;
    public Text extralives;
    public Text textinvulnarebility;
    StartButtonTimeMode button1;
    StartButtonSurvivalMode button2;
    public Random rng;
    public int extralife;
    public int remaining_invulnarebility;
    //public GameObject startbutton;

    public GameObject idleText, gameOverText;

    public ShowSplashImageCanvas splashgroup;

    public GameObject backToMenu;

    public GameObject startPos;

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
        if (Input.GetKey(KeyCode.N) && !runningGame)
        {
            newGame();
        }
        if (remaining_invulnarebility > 0)
            remaining_invulnarebility--;
        if (runningGame)
            Time.timeScale += 0.00001f; //speeding up the game
        if (Input.GetKeyDown(KeyCode.B))
        {
            float speed = 4;
            GameObject target = GameObject.Find("end");
            Vector3 pos = new Vector3(startPos.transform.position.x, startPos.transform.position.y, startPos.transform.position.z);
            GameObject bomb = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/bomb_ST6TPHC Variant"), pos, transform.rotation);

            ThrowArcLike script = (ThrowArcLike)bomb.GetComponent<ThrowArcLike>();
            script.firingAngle = 10f;
            script.Target = target.transform;
            script.gravity = 3.1f;
            // script.shouldRotate = true;
            StartCoroutine(script.SimulateProjectile());

        }
    }

    public IEnumerator spawnBombs()
    {


        yield return new WaitForSeconds(10f);
    }

    public void newGame()
    {
        Debug.Log("New Game(Survival)");
        // start the counter
        headlineTxt.changeState(false);
        // dont display the header animation when game is started
        //idleText = GameObject.Find("IdleText");
        // idleText.gameObject.SetActive(false);
        FindObjectOfType<FruitSpawn>().isSurvialMode = true;
        FindObjectOfType<FruitSpawn>().newGame();

        addScore(-score);
        turn = 0;
        extralife = 0;
        // setExtralife(10);
        //button1.transform.position = new Vector3(0.5f, 1.5f, -5f);
        runningGame = true;
        Time.timeScale = 1;
        splashgroup.hideAllSplashImage();
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
        extralives.text = "Leben: " + extralife;
    }

    public void addExtralife(int l)
    {
        extralife += l;
        // TODO delete
        extralives.text = "Leben: " + extralife;
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
        Vector3 pos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 1, gameObject.transform.position.z);
        GameObject.Instantiate(Resources.Load("Prefabs/gong backToMain Variant"), pos, Quaternion.identity); //Sphären Instanzieren und Erstellen

    }
}
