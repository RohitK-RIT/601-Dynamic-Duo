using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core.Managers
{
    public class SceneManager
    {
        public void LoadScene(string sceneName, LoadSceneMode sceneLoadMode = LoadSceneMode.Single)
        {
            GameManager.Instance.StartCoroutine(LoadSceneAsync(sceneName, sceneLoadMode));
        }

        private IEnumerator LoadSceneAsync(string sceneName, LoadSceneMode sceneLoadMode)
        {
            var sceneLoadOperation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Loading Scene", LoadSceneMode.Additive);
            yield return new WaitUntil(() => sceneLoadOperation.isDone);
            
            if(sceneLoadMode != LoadSceneMode.Additive)
            {
                var scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
                sceneLoadOperation = UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(scene);
                
                yield return new WaitUntil(() => sceneLoadOperation.isDone);
            }
            
            sceneLoadOperation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName, sceneLoadMode);
            yield return new WaitUntil(() => sceneLoadOperation.isDone);

            if (sceneLoadMode == LoadSceneMode.Additive)
            {
                sceneLoadOperation = UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("Loading Scene");
                yield return new WaitUntil(() => sceneLoadOperation.isDone);
            }
        }
    }
}