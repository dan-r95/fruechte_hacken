using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManagerSurvival : MonoBehaviour
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
    public Text textinvulnarebility;
    StartButtonTimeMode button1;
    StartButtonSurvivalMode button2;
    public Random rng;
    public int extralife;
    public int remaining_invulnarebility;

    public GameObject gameOverText;

    public ShowSplashImageCanvas splashgroup;

    public GameObject startPos;
    public GameObject healthStartPos1, healthStartPos2;

    public GameObject avatar;

    Scene current;

    HintManager hintManager;

    void Awake()
    {
        scoreTxt = FindObjectOfType<ScoreScript>();
        button1 = FindObjectOfType<StartButtonTimeMode>();
        button2 = FindObjectOfType<StartButtonSurvivalMode>();
        headlineTxt = FindObjectOfType<headlineTxt>();
        current = SceneManager.GetActiveScene();
        hintManager = FindObjectOfType<HintManager>();
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
            Time.timeScale += 0.00001f; //speeding up the game
    }

    private IEnumerator StartGameAfterLoading()
    {
        hintManager.showInfoText();
        hintManager.hideScoreText();
        yield return new WaitForSecondsRealtime(2.5f);
        avatar.SetActive(true);
        hintManager.showGoText();
        hintManager.showScoreText();
        yield return new WaitForSecondsRealtime(3f);
        newGame();

    }

    public IEnumerator spawnBombs()
    {


        yield return new WaitForSecondsRealtime(10f);

        GameObject target0 = GameObject.Find("end");
        GameObject target1 = GameObject.Find("end (1)");
        GameObject target2 = GameObject.Find("end (2)");
        Vector3 pos = new Vector3(startPos.transform.position.x, startPos.transform.position.y, startPos.transform.position.z);
        GameObject bomb = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/bomb_ST6TPHC Variant"), pos, transform.rotation);

        ThrowArcLike script = (ThrowArcLike)bomb.GetComponent<ThrowArcLike>();
        script.firingAngle = 10f;

        script.gravity = 3.1f;
        int nextPlacement = Random.Range(0, 2);
        switch (nextPlacement)
        {
            case 0: script.Target = target0.transform; break;
            case 1: script.Target = target1.transform; break;
            case 2: script.Target = target2.transform; break;
        }
        // script.shouldRotate = true;
        StartCoroutine(script.SimulateProjectile());
    }

    public IEnumerator spawnHealthItems()
    {

        // on motion detection spawn health item
        yield return new WaitForSecondsRealtime(3f);

        GameObject target0 = GameObject.Find("end");
        GameObject target1 = GameObject.Find("end (1)");
        GameObject target2 = GameObject.Find("end (2)");
        int next = Random.Range(0, 1);
        Vector3 pos;
        if (next == 0)
        {
            pos = new Vector3(healthStartPos1.transform.position.x, healthStartPos1.transform.position.y, healthStartPos1.transform.position.z);
        }
        else
        {
            pos = new Vector3(healthStartPos2.transform.position.x, healthStartPos2.transform.position.y, healthStartPos2.transform.position.z);
        }
        GameObject bomb = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/asiabox"), pos, transform.rotation);
        bomb.gameObject.layer = 0;
        ThrowArcLike script = (ThrowArcLike)bomb.GetComponent<ThrowArcLike>();
        script.firingAngle = 10f;

        script.gravity = 4.1f;
        int nextPlacement = Random.Range(0, 2);
        switch (nextPlacement)
        {
            case 0: script.Target = target0.transform; break;
            case 1: script.Target = target1.transform; break;
            case 2: script.Target = target2.transform; break;
        }
        // script.shouldRotate = true;
        StartCoroutine(script.SimulateProjectile());
    }

    public void newGame()
    {

        if (current.name != "TimeLimitMode" && current.name != "Main Menu")
        {
            Debug.Log("New Game(Survival)");
            FruitSpawn fruitspawn = FindObjectOfType<FruitSpawn>();
            fruitspawn.isSurvialMode = true;
            fruitspawn.newGame();

            addScore(-score);
            turn = 0;
            extralife = 0;
            setExtralife(15);
            runningGame = true;
            Time.timeScale = 1;
            StartCoroutine(spawnBombs());
            StartCoroutine(spawnHealthItems());
        }
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
        extralives.text = extralife.ToString(); ;
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
        hintManager.toggleYourPointsTxt();
        gameOverText.SetActive(true);

        // add button to get back to the main menu
        Vector3 pos = new Vector3(-1.2f, 1f, 0f);
        GameObject gong = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/gong backToMain Variant"), pos, Quaternion.Euler(0, 90, 0));
        gong.transform.localScale = new Vector3(0.07f, 0.07f, 0.07f);
        Time.timeScale = 1;

    }
}