using UnityEngine;
using UnityEngine.UI;

public class SetAlpha : MonoBehaviour
{
    Image image;
    public float alpha=  0.9f;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();

        Color c = image.color;
        c.a = alpha;
        image.color = c;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
