using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    Canvas Canvas;


    // Start is called before the first frame update
    void Start()
    {
        Canvas = GetComponent<Canvas>();
        Canvas.enabled = true;

    }


    public void LoadLevel(string levelName)
    {
        //SceneManager.LoadScene(levelName);
        //Open Loading screen
        SceneManager.LoadScene("Loading Scene");

        StartCoroutine(LoadLevelAsyncCoroutine(levelName));
    }

    public void QuitGame()
    {
        Application.Quit();
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
