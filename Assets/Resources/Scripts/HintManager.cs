
using UnityEngine;
using System.Collections;

public class HintManager : MonoBehaviour
{

    GameManagerSurvival managerSurvival;

    public GameObject frenzyText, freezeText, multiplierText;
    public GameObject InfoText, InfoTextGo;

    public GameObject idleText, gameOverText;

    public GameObject scoreText, score;

    private bool displayLabel = false;

    void Start()
    {

    }

    public IEnumerator FlashLabel()
    {
        // Fancy pants flash of label on and off   
        while (true)
        {
            //object = true;
            yield return new WaitForSeconds(.5f);
            displayLabel = false;
            yield return new WaitForSeconds(.5f);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void showIdleText()
    {

    }

    public void showGameOverText()
    {

    }

    public IEnumerator FlashLabel(GameObject obj)
    {
        int i = 0;
        while (true)
        {
            if (obj.activeSelf)
            {
                obj.SetActive(false);
            }
            else
            {
                obj.SetActive(true);
            }
            yield return new WaitForSeconds(0.5f);
            i++;
            if (i > 20) break;

        }
    }


    public void showMultiplierText()
    {
        if (multiplierText != null)
        {
            StartCoroutine(FlashLabel(multiplierText));
        }
    }

    public void showFrenzyText()
    {
        if (multiplierText != null)
        {
            StartCoroutine(FlashLabel(frenzyText));
        }
    }

    public void showSlowMoText()
    {
        if (multiplierText != null)
        {
            StartCoroutine(FlashLabel(freezeText));
        }
    }

    public void showInfoText()
    {
        object[] parms = new object[2] { 3f, InfoText };
        StartCoroutine(showTxtForSomeTime(parms));
    }

    public void showGoText()
    {
        object[] parms = new object[2] { 2f, InfoTextGo };
        StartCoroutine(showTxtForSomeTime(parms));
    }


    public void showScoreText()
    {
        object[] parms = new object[2] { scoreText, score };
        activateText(parms);
    }

    public void hideScoreText()
    {
        object[] parms = new object[2] { scoreText, score };
        deactivateText(parms);
    }


    void activateText(object[] objects)
    {
        foreach (Object obj in objects)
        {
            GameObject objectToInteractWith = (GameObject)obj;
            objectToInteractWith.SetActive(true);
        }

    }

    void deactivateText(object[] objects)
    {
        foreach (Object obj in objects)
        {
            GameObject objectToInteractWith = (GameObject)obj;
            objectToInteractWith.SetActive(false);
        }
    }


    IEnumerator showTxtForSomeTime(object[] parms)
    {
        float length = (float)parms[0];
        GameObject objectToInteractWith = (GameObject)parms[1];
        objectToInteractWith.SetActive(true);
        yield return new WaitForSecondsRealtime(length);
        objectToInteractWith.SetActive(false);
    }
}
