using System;
using NecatiAkpinar.Abstracts;
using UnityEngine;

namespace NecatiAkpinar.Managers
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private BaseWindow _match2GameplayWindow;
        [SerializeField] private BaseWindow _levelWinWindow;
        [SerializeField] private BaseWindow _levelLoseWindow;

        private void OnEnable()
        {
            EventManager.OnLevelFinishedUI += EnableLevelEndWindow;
        }

        private void OnDisable()
        {
            EventManager.OnLevelFinishedUI -= EnableLevelEndWindow;
        }

        private void Start()
        {
            DisableLevelEndWindows();
            _match2GameplayWindow.SetActive(true);
        }

        private void EnableLevelWinWindow()
        {
            DisableLevelEndWindows();
            _levelWinWindow.SetActive(true);
        }

        private void EnableLevelLoseWindow()
        {
            DisableLevelEndWindows();
            _levelLoseWindow.SetActive(true);
        }

        private void DisableLevelEndWindows()
        {
            _levelWinWindow.SetActive(false);
            _levelLoseWindow.SetActive(false);
        }

        private void EnableLevelEndWindow(bool isLevelWin)
        {
            if (isLevelWin)
                EnableLevelWinWindow();
            else
                EnableLevelLoseWindow();
        }
    }
}