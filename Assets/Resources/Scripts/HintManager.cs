
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

    void OnGUI()
    {

        if (displayLabel == true)
            GUILayout.Label("I AM FLASHING");

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void showMultiplierText()
    {
        //  StartCoroutine(FlashLabel(multiplierText));
    }

    public void showIdleText()
    {

    }

    public void showGameOverText()
    {

    }

    public void showFrenzyText()
    {
        // StartCoroutine(FlashLabel(frenzyText));
    }

    public void showSlowMoText()
    {
        //StartCoroutine(FlashLabel(freezeText));
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
