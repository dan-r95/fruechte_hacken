using UnityEngine;
using System.Collections.Generic;

public class FruitSpawn : MonoBehaviour {

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
    int randindex, randindex_old = 0, spawnlimit;
    GameManager manager;
    bool[] spawnslotused;
    Vector3 spawnpos;
    

	void Start () {
        manager = FindObjectOfType<GameManager>();
        waveTime = startWaveTime;
        startTime = Time.time;
        manager.runningGame = false;
        fruits.Add(apple);
        fruits.Add(banana);
        fruits.Add(cherry);
        fruits.Add(kiwi);
        fruits.Add(orange);
        fruits.Add(peach);
    }
	
	// Update is called once per frame
    void FixedUpdate()
    {
        if (manager.runningGame)
        {
            if ((Time.time - startTime) >= spawnTime)
            {
                SpawnColliders();
                spawnTime += waveTime;
                Time.timeScale += timeincrease;
            }
        }
	}

    public void newGame() {
        FindObjectOfType<FruitSpawn>().startTime = Time.time; // equal to this.startTime = Time.time?
        waveTime = startWaveTime;
        spawnTime = 0f;
        startTime = Time.time;
    }

    

    void SpawnColliders()
    {
        int[] spawnslotused = new int[4];
        spawnslotused[0] = 0; 
        spawnslotused[1] = 0; 
        spawnslotused[2] = 0;
        spawnslotused[3] = 0;
        //spawnslotused[4] = 0;

        GameObject activeFruit;

        spawnlimit = 2;
        for (int i = 0; i <= manager.turn / 10 && i < spawnlimit; i++) //i represents the number of obstacles or items to spawn in a single wave
        {
            randindex = Random.Range(0, spawnslotused.Length);

            activeFruit = fruits[Random.Range(0, fruits.Count)];
            Instantiate(activeFruit, spawnPoint[randindex].position, Quaternion.identity);
            spawnlimit++;
            spawnslotused[randindex] = 1;
        }
        manager.turn++;
    }
}
