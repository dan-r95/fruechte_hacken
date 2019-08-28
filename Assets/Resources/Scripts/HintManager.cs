
using UnityEngine;
using System.Collections;

public class HintManager : MonoBehaviour
{

    GameManagerSurvival managerSurvival;

    public GameObject frenzyText, freezeText, multiplierText;

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

}
