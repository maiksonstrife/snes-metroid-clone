using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;
using UnityEngine.UI;

namespace Menu
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] private Button ResumeButton;
        [SerializeField] private Button RestartButton;
        [SerializeField] private Button QuitButton;

        private void Start()
        {
            ResumeButton.onClick.AddListener(HandleResumeClicked);
            RestartButton.onClick.AddListener(HandleRestartClicked);
            QuitButton.onClick.AddListener(HandleQuitClicked);
        }

        private void HandleQuitClicked()
        {
            GameManager.Instance.QuitGame();
        }
        private void HandleRestartClicked()
        {
            GameManager.Instance.RestartGame();
        }

        private void HandleResumeClicked()
        {
            GameManager.Instance.TogglePause();
        }
    }
}