using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightAreaTrigger : MonoBehaviour
{

    public GameObject lightObject;

    Light light;
    // Start is called before the first frame update
    void Start()
    {
        light = lightObject.GetComponent<Light>();
        light.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("collision");
        light.enabled = true;
    }

    void OnTriggerExit(Collider other)
    {
        light.enabled = false;
    }
}
