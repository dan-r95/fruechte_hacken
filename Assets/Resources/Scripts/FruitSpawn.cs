using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class FruitSpawn : MonoBehaviour
{

    public Transform[] spawnPoint;
    List<GameObject> fruits = new List<GameObject>();
    public GameObject apple;
    public GameObject banana;
    public GameObject cherry;
    public GameObject kiwi;
    public GameObject orange;
    public GameObject peach;
    public float startTime;
    public float spawnTime = 0f;
    public float startWaveTime = 2f;
    float waveTime;
    public float timeincrease = 0.04f;
    int randindex;
    GameManager manager;
    GameManagerSurvival managerSurvival;
    bool[] spawnslotused;
    Vector3 spawnpos;

    public bool isSurvialMode = false;

    public int spawnlimit;

    float saveTimeScale;

    void Start()
    {
        Time.timeScale = 0.8f;
        StartCoroutine(StartGameAfterLoading());

    }



    private IEnumerator StartGameAfterLoading()
    {
        yield return new WaitForSecondsRealtime(3f);
        if (isSurvialMode)
        {
            managerSurvival = FindObjectOfType<GameManagerSurvival>();
            managerSurvival.runningGame = false;
        }
        else
        {
            manager = FindObjectOfType<GameManager>();
            manager.runningGame = false;
        }
        waveTime = startWaveTime;
        startTime = Time.time;

        fruits.Add(apple);
        fruits.Add(cherry);
        fruits.Add(kiwi);
        fruits.Add(orange);
        fruits.Add(peach);
        fruits.Add(banana);

        spawnlimit = 1;
    }

    public void enableSlowMo(){
        saveTimeScale = Time.timeScale;
        Time.timeScale = 0.5f;
    }

    public void disableSlowMo(){
        // also add the passed time to that ...
        Time.timeScale = saveTimeScale;
        saveTimeScale = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isSurvialMode)
        {
            if (managerSurvival != null && managerSurvival.runningGame)
            {
                if ((Time.time - startTime) >= spawnTime)
                {
                    SpawnColliders();
                    spawnTime += waveTime;
                    Time.timeScale += timeincrease;
                }
            }
        }
        else
        {
            if (manager != null && manager.runningGame)
            {
                if ((Time.time - startTime) >= spawnTime)
                {
                    SpawnColliders();
                    spawnTime += waveTime;
                    Time.timeScale += timeincrease;
                }
            }
        }

    }

    public void newGame()
    {
        //FindObjectOfType<FruitSpawn>().startTime = Time.time; // equal to this.startTime = Time.time?
        this.startTime = Time.time;
        waveTime = startWaveTime;
        spawnTime = 0f;
        startTime = Time.time;
    }



    void SpawnColliders()
    {
        int[] spawnslotused = new int[5];
        spawnslotused[0] = 0;
        spawnslotused[1] = 0;
        spawnslotused[2] = 0;
        spawnslotused[3] = 0;
        spawnslotused[4] = 0;

        GameObject activeFruit;

        
        if (isSurvialMode)
        {
            for (int i = 0; i <= managerSurvival.turn / 10 && i < spawnlimit; i++) //i represents the number of obstacles or items to spawn in a single wave
            {
                randindex = Random.Range(0, spawnslotused.Length);
                // use a different fruit each time
                activeFruit = fruits[Random.Range(0, fruits.Count)];
                Debug.Log(activeFruit.ToString());
                activeFruit.GetComponent<Sphare>().isSurivalMode = true;
                // set to random to layer to minimize colliding chance
                activeFruit.gameObject.layer = Random.Range(10, 16);
                Instantiate(activeFruit, spawnPoint[randindex].position, Quaternion.identity);
                spawnlimit++;
                spawnslotused[randindex] = 1;
            }

            managerSurvival.turn++;
        }
        else
        {
            for (int i = 0; i <= manager.turn / 10 && i < spawnlimit; i++) //i represents the number of obstacles or items to spawn in a single wave
            {
                randindex = Random.Range(0, spawnslotused.Length);
                // use a different fruit each time
                activeFruit = fruits[Random.Range(0, fruits.Count)];
                Instantiate(activeFruit, spawnPoint[randindex].position, Quaternion.identity);
                activeFruit.GetComponent<Sphare>().isSurivalMode = false;
                Instantiate(activeFruit, spawnPoint[randindex].position, Quaternion.identity);
                spawnlimit++;
                spawnslotused[randindex] = 1;
            }

            manager.turn++;
        }

    }
}
