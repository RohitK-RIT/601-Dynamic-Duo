using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScreen : MonoBehaviour
{

    public Canvas startScreenCanvas;


    // Start is called before the first frame update
    void Start()
    {
        startScreenCanvas = GetComponent<Canvas>();
        startScreenCanvas.enabled = true;

    }


    public void StartGame()
    {
        startScreenCanvas.enabled = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
