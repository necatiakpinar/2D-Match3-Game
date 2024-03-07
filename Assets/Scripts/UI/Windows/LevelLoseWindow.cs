using NecatiAkpinar.Abstracts;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace NecatiAkpinar.UI.Windows
{
    public class LevelLoseWindow : BaseWindow
    {
        [SerializeField] private Button _retryButton;

        private void Start()
        {
            SetButtons();
        }

        private void SetButtons()
        {
            _retryButton.onClick.AddListener(RetryLevel);
        }

        private void RetryLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}