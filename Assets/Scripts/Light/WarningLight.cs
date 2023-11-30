using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningLight : MonoBehaviour
{

    Light myLight;
    public float intensityChangeInterval = 2.0f;
    private float lastIntensityChangeTime;


    private float maxIntensity;
    private float minIntensity;

    private bool isSwitched = true;

    private  
    // Start is called before the first frame update
    void Start()
    {
        myLight = GetComponent<Light>();
        maxIntensity = myLight.intensity;
        minIntensity = maxIntensity / 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastIntensityChangeTime >= intensityChangeInterval)
        {
            if(isSwitched)
            {
                isSwitched = false;
                myLight.intensity = minIntensity;
            }
            else
            {
                isSwitched = true;
                myLight.intensity = maxIntensity;
            }

            lastIntensityChangeTime = Time.time;
        }
        
    }
}
