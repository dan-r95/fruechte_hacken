using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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

    public Scene current;

    public GameObject avatar;

    public GameObject startPos;
    public GameObject healthStartPos1, healthStartPos2;

    void Awake()
    {
        scoreTxt = FindObjectOfType<ScoreScript>();
        button1 = FindObjectOfType<StartButtonTimeMode>();
        button2 = FindObjectOfType<StartButtonSurvivalMode>();
        headlineTxt = FindObjectOfType<headlineTxt>();
        // QualitySettings.vSyncCount = 1;
        // Sync framerate to monitors refresh rate
        current = SceneManager.GetActiveScene();
    }
    // Use this for initialization
    void Start()
    {
        // Turn off v-sync
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
        StartCoroutine(StartGameAfterLoading());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            noclip = !noclip;
        }
        if (remaining_invulnarebility > 0)
            remaining_invulnarebility--;
        if (runningGame)
            Time.timeScale += 0.000001f; //speeding up the game
    }

    private IEnumerator StartGameAfterLoading()
    {
        yield return new WaitForSecondsRealtime(2f);
        avatar.SetActive(true);
        yield return new WaitForSecondsRealtime(3f);
        newGame();
    }


    public void spawnPowerUp()
    {
        GameObject target0 = GameObject.Find("end"); //GameObject.Find("end");
        GameObject target1 = GameObject.Find("end (1)");//GameObject.Find("end (1)");
        GameObject target2 = GameObject.Find("end (2)"); //GameObject.Find("end (2)");

        // on motion detection spawn health item

        Debug.Log("spawning new powerup");
        int next = Random.Range(0, 1);
        int nextPlacement = Random.Range(0, 2);
        Debug.Log("nextplacement" + nextPlacement);
        Vector3 pos;
        Debug.Log(next);
        if (next == 1)
        {
            pos = new Vector3(healthStartPos1.transform.position.x, healthStartPos1.transform.position.y, healthStartPos1.transform.position.z);
        }
        else
        {
            pos = new Vector3(healthStartPos2.transform.position.x, healthStartPos2.transform.position.y, healthStartPos2.transform.position.z);
        }


        GameObject banana = switchBananas(nextPlacement, pos);


        ThrowArcLike script = (ThrowArcLike)banana.GetComponent<ThrowArcLike>();
        script.firingAngle = 25f;

        script.gravity = 6.1f;
        if (target1 != null && target2 != null & target0 != null)
        {
            nextPlacement = Random.Range(0, 2);
            switch (nextPlacement)
            {
                case 0: script.Target = target0.transform; break;
                case 1: script.Target = target1.transform; break;
                case 2: script.Target = target2.transform; break;
            }

            // script.shouldRotate = true;
            script.SimulateProjectile();
        }
        Debug.Log("exiting the spawn poweriu");
        return;
    }

    public GameObject switchBananas(int nextPlacement, Vector3 pos)
    {
        GameObject banana = new GameObject();
        switch (nextPlacement)
        {
            case 0: banana = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/Banana yellow"), pos, transform.rotation); break;
            case 1: banana = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/Banana ice"), pos, transform.rotation); break;
            case 2: banana = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/Banana blue"), pos, transform.rotation); break;
        }
        banana.gameObject.layer = Random.Range(10, 16);
        Debug.Log("spawn bananana");
        Debug.Log(banana.gameObject.layer);
        return banana;
    }

    public void newGame()
    {

        if (current.name != "SurvivalMode" && current.name != "Main Menu")
        {
            Debug.Log("NEW  GAME(Time)");
            if (headlineTxt != null)
            {
                headlineTxt.changeState(false);
            }
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
            StartCoroutine(spawnSpecialItems());
        }

    }

    private IEnumerator spawnSpecialItems()
    {
        //while (runningGame)
        //{
        Debug.Log("Waiting");
        yield return new WaitForSecondsRealtime(5.5f);
        spawnPowerUp();
        Debug.Log("done spawn");
        yield return new WaitForSecondsRealtime(5.5f);
          Debug.Log("done wait 2");
        //}
    }
    public IEnumerator showPowerUpBillboard()
    {
        yield return new WaitForSecondsRealtime(3f);
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
