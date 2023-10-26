using System;
using Core.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class MainMenuPage : MonoBehaviour
    {
        [SerializeField] private Button playButton;

        private void OnEnable()
        {
            playButton.onClick.RemoveAllListeners();
            playButton.onClick.AddListener(() => GameManager.Instance.SceneManager.LoadScene("Level Scene"));
        }
    }
}