using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PauseScreen : MonoBehaviour
{

    public GameObject pauseScreen;
    // Start is called before the first frame update
    void Start()
    {
        pauseScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseScreen.SetActive(true);
            //Debug.Log("Esc key was pressed!");
        }
    }

    public void ResumeGame()
    {
        pauseScreen.SetActive(false);
    }


}
