using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestructScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(destruct());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator destruct()
    {   
        Debug.Log("8s now");
        yield return new WaitForSecondsRealtime(8f);
        this.enabled = false;
        gameObject.SetActive(false);
        Debug.Log("self destructed");
    }
}
