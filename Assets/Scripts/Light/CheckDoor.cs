using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckDoor : MonoBehaviour
{

    public List<GameObject> doorCheckList = new List<GameObject>();

    public List<GameObject> lightList = new List<GameObject>();
    bool ifAllNull = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ifAllNull = true;
        foreach (GameObject door in doorCheckList)
        {
            if (door != null)
            {
                ifAllNull = false;
            }
        }


        if (ifAllNull == true)
        {
            if (lightList.Count > 0)
            {
                foreach (GameObject go in lightList)
                {
                    Light light = go.GetComponent<Light>();
                    light.enabled = true;
                }
            }
        }
    }
}
