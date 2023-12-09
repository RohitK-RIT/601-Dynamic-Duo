using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Dialog : MonoBehaviour
{
    public TextMeshProUGUI leftDialogueText;
    public string[] leftDialogueLines;
    private int leftNum = 0;

    public TextMeshProUGUI rightDialogueText;
    public string[] rightDialogueLines;
    private int rightNum = 0;

    bool ifLastLeft = false;

    public string NextLevelName = "OnboardingScene";

    // Start is called before the first frame update
    void Start()
    {
        rightDialogueText.text = rightDialogueLines[rightNum];
        leftDialogueText.text = leftDialogueLines[leftNum];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextText()
    {
        if(ifLastLeft == true)
        {
            rightNum++;
            
            //If dialog ends
            if(rightNum >= rightDialogueLines.Length)
            {
                LoadLevel();
                return;
            }
            rightDialogueText.text = rightDialogueLines[rightNum];
            ifLastLeft = false;
        }
        else
        {
            leftNum++;

            if (leftNum >= leftDialogueLines.Length)
            {
                LoadLevel();
                return;
            }

            leftDialogueText.text = leftDialogueLines[leftNum];
            ifLastLeft = true;
        }
    }

    public void LoadLevel()
    {

        SceneManager.LoadScene("Loading Scene");

        StartCoroutine(LoadLevelAsyncCoroutine(NextLevelName));
    }

    private IEnumerator LoadLevelAsyncCoroutine(string levelName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(levelName);

        while (!asyncLoad.isDone)
        {
            //float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);      

            yield return null;
        }
    }
}
