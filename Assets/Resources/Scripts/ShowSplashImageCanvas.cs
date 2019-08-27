using UnityEngine;
using UnityEngine.UI;

public class ShowSplashImageCanvas : MonoBehaviour
{
    public Image image1, image2, image3, image4, image5, image6, image7, image8, image9, image10;
    private GameObject gameOverText;
    // Start is called before the first frame update
    void Start()
    {
        hideAllSplashImage();
    }

    public void hideAllSplashImage()
    {
        image1.enabled = false;
        image2.enabled = false;
        image3.enabled = false;
        image4.enabled = false;
        image5.enabled = false;
        image6.enabled = false;
        image7.enabled = false;
        image8.enabled = false;
        image9.enabled = false;
        //image10.enabled = false;
    }

    // Update is called once per frame
    public void showSplashImage(int i)
    {
        switch(i)
        {
            case 1:
                image1.enabled = true;
                break;
            case 2:
                image2.enabled = true;
                break;
            case 3:
                image3.enabled = true;
                break;
            case 4:
                image4.enabled = true;
                break;
            case 5:
                image5.enabled = true;
                break;
            case 6:
                image6.enabled = true;
                break;
            case 7:
                image7.enabled = true;
                break;
            case 8:
                image8.enabled = true;
                break;
            case 9:
                image9.enabled = true;
                break;
            case 10:
                Debug.Log("10");
                gameOverText = GameObject.Find("gameOverText");
                gameOverText.gameObject.SetActive(true);
                //image10.enabled = true;        
                break;
            default:
                break;
        }
        
    }
}
