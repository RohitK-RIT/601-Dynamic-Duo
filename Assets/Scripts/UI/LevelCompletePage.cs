using Core.Game_Systems.UI_System;
using SceneManager = UnityEngine.SceneManagement.SceneManager;

namespace UI
{
    public class LevelCompletePage : UIPage
    {
        public override PageID PageID => PageID.LevelComplete;

        private void OnEnable()
        {
            Invoke(nameof(ReloadLevel), 5f);
        }

        private void ReloadLevel()
        {
            SceneManager.LoadScene("Menu Scene");
        }
    }
}