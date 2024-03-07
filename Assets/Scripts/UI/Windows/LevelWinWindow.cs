using System;
using NecatiAkpinar.Abstracts;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace NecatiAkpinar.UI.Windows
{
    public class LevelWinWindow : BaseWindow
    {
        [SerializeField] private Button _continueButton;

        private void Start()
        {
            SetButtons();
        }

        private void SetButtons()
        {
            _continueButton.onClick.AddListener(LoadNextLevel);
        }

        private void LoadNextLevel() //For this case reload the scene :)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}