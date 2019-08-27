using UnityEngine;

public class SpawnFracturedScript : MonoBehaviour
{
    public GameObject originalObject;
    public GameObject fracturedObject;
    private GameObject origObj;

    void Start()
    {
        origObj = Instantiate(originalObject, new Vector3(0, 0.5f, 0), Quaternion.identity);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SpawnFracturedObject();
        }
    }

    public void SpawnFracturedObject()
    {        
        Destroy(origObj);
        GameObject fractObj = Instantiate(fracturedObject) as GameObject;
        fractObj.GetComponent<ExplodeFruitsScript>().ExplodeFruits();
    }
}
