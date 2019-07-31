using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    GameManager manager;
    Text timeText;
    System.Timers.Timer LeTimer;
    public float timeLeft = 100f;
    public int BoomDown = 100;
    void Start()
    {
        timeText = GetComponent<Text>();
        //Initialize timer with 1 second intervals
        StartCoroutine(Example());
        Time.timeScale = 1; //Just making sure that the timeScale is right

    }




    IEnumerator Example()
    {
        yield return new WaitForSeconds(1);
        timeLeft--;
        if (timeLeft == 0)
        {
            Debug.Log("GameOver!");
            yield break;
        }

    }

    void Update()
    {
        setTimeText(timeLeft);








    }
    public void setTimeText(float time)
    {
        timeText.text = string.Format("{0} S", time.ToString());
    }
}
