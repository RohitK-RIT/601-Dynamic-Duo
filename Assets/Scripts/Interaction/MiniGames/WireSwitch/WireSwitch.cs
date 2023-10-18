using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireSwitch : MonoBehaviour
{

    //If each wire is connected
    bool WireStatus1 = false;
    bool WireStatus2 = false;
    bool WireStatus3 = false;
    bool WireStatus4 = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake()
    {
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //check player's 
    }

    void Destroy()
    {
        Time.timeScale = 1;
    }
}
