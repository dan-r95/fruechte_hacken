using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Threading;

public class TimeManager : MonoBehaviour
{
    GameManager manager;
    public Text timeText;
    System.Timers.Timer LeTimer;
    public float timeLeft = 100f;

    public GameObject timeLeftText;

    void Start()
    {
        manager = FindObjectOfType<GameManager>();
    }
    public void startTimer()
    {
        timeLeftText.SetActive(true);
        //Initialize timer with 1 second intervals
        Time.timeScale = 1; //Just making sure that the timeScale is right
        StartCoroutine(CountDownToZero());
    }




    IEnumerator CountDownToZero()
    {
        while (true)
        {
            if (manager.isSlowMoEnabled())
            {
                yield return new WaitForSecondsRealtime(2);
            }
            else
            {
                yield return new WaitForSecondsRealtime(1);
            }
            timeLeft--;
            if (timeLeft == 0)
            {
                timeLeftText.SetActive(false);
                FindObjectOfType<GameManager>().EndGame();
                yield break;
            }

        }
    }

    void Update()
    {
        if (timeLeftText.activeSelf)
        {
            setTimeText(timeLeft);
        }
    }
    public void setTimeText(float time)
    {
        timeText.text = string.Format("{0} S", time.ToString());
    }
}
