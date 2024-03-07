using System;
using System.Collections.Generic;
using NecatiAkpinar.Data;
using NecatiAkpinar.Enums;
using NecatiAkpinar.Level;
using NecatiAkpinar.Managers;
using NecatiAkpinar.Miscs;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NecatiAkpinar.Controllers
{
    public class LevelController
    {
        private LevelData _currentLevel;
        private int _totalMoves;
        private bool _isLevelFinished;
        public LevelData CurrentLevel => _currentLevel;

        public int TotalMoves => _totalMoves;

        public bool IsLevelFinished => _isLevelFinished;

        private Dictionary<GoalType, int> _goals = new Dictionary<GoalType, int>();

        public LevelController(LevelData currentLevel)
        {
            _currentLevel = currentLevel;
            _totalMoves = currentLevel.Moves;

            GameGoal goal;
            for (int i = 0; i < currentLevel.Goals.Count; i++)
            {
                goal = currentLevel.Goals[i];
                _goals.Add(goal.Type, goal.Amount);
            }

            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            EventManager.OnTileActivated += DecreaseMoveCount;
            EventManager.OnGoalUpdated += UpdateGoalAmount;
            EventManager.IsGoalAvailable += HasElementExistInGoals;
        }

        private void UnsubscribeEvents()
        {
            EventManager.OnTileActivated -= DecreaseMoveCount;
            EventManager.OnGoalUpdated -= UpdateGoalAmount;
            EventManager.IsGoalAvailable -= HasElementExistInGoals;
        }

        public void DecreaseMoveCount()
        {
            _totalMoves--;
            EventManager.OnMovesUpdated?.Invoke();

            if (_totalMoves == 0 && _goals.Count > 0)
                LevelLose();
        }

        public void UpdateGoalAmount(GoalType goalType)
        {
            if (!_goals.ContainsKey(goalType))
                return;

            _goals[goalType]--;
            if (_goals[goalType] <= 0)
                _goals.Remove(goalType);

            if (_goals.Count == 0)
            {
                DecreaseMoveCount();
                LevelWin();
            }
        }

        public bool HasElementExistInGoals(GoalType goalType)
        {
            if (_goals.TryGetValue(goalType, out var test))
                return true;

            return false;
        }

        public bool HasExistInSpawnableElements(GameElementType elementType)
        {
            foreach (var spawnableElement in _currentLevel.SpawnableElements)
                if (spawnableElement.ElementType == elementType)
                    return true;

            return false;
        }

        public void LevelWin()
        {
            _isLevelFinished = true;
            UnsubscribeEvents();
            EventManager.OnLevelFinished?.Invoke(true);
            EventManager.OnLevelFinishedUI?.Invoke(true);
        }

        public void LevelLose()
        {
            _isLevelFinished = true;
            UnsubscribeEvents();
            EventManager.OnLevelFinished?.Invoke(false);
            EventManager.OnLevelFinishedUI?.Invoke(false);
        }
    }
}