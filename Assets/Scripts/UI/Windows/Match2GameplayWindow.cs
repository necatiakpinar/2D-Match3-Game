using System;
using System.Collections.Generic;
using NecatiAkpinar.Abstracts;
using NecatiAkpinar.Controllers;
using NecatiAkpinar.Data;
using NecatiAkpinar.Enums;
using NecatiAkpinar.Level;
using NecatiAkpinar.Managers;
using NecatiAkpinar.Miscs;
using NecatiAkpinar.UI.Widgets;
using TMPro;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

namespace NecatiAkpinar.UI.Windows
{
    public class Match2GameplayWindow : BaseWindow
    {
        [Header("Goals")] [SerializeField] private GoalWidget _goalWidgetPF;
        [SerializeField] private Transform _goalsParent;

        [Header("Moves")] [SerializeField] private TMP_Text _movesLabel;

        private SpriteAtlas _goalSpriteAtlas;
        private LevelController _levelController;
        private LevelData _levelData;
        private List<GoalWidget> _goalWidgets = new List<GoalWidget>();

        private void OnEnable()
        {
            EventManager.OnMovesUpdated += UpdateRemainingMoves;
            EventManager.GetGoalWidgetPosition += GetGoalWidgetPositionMethod;
            EventManager.OnGoalUpdatedUI += UpdateGoalAmount;
        }

        private void OnDisable()
        {
            EventManager.OnMovesUpdated -= UpdateRemainingMoves;
            EventManager.GetGoalWidgetPosition -= GetGoalWidgetPositionMethod;
            EventManager.OnGoalUpdatedUI -= UpdateGoalAmount;
        }

        private void Start()
        {
            _levelController = GameReferences.Instance.LevelController;
            _levelData = _levelController.CurrentLevel;
            _goalSpriteAtlas = DataManager.Instance.GoalSpriteAtlas;
            PrepareUIElements();
        }

        private void PrepareUIElements()
        {
            GameGoal goal;
            Sprite goalSprite;
            int goalAmount;
            GoalWidget goalWidget;

            for (int i = 0; i < _levelData.Goals.Count; i++)
            {
                goal = _levelData.Goals[i];
                goalSprite = GetSpriteByType(goal.Type.ToString());
                goalAmount = goal.Amount;
                goalWidget = Instantiate(_goalWidgetPF, _goalsParent);
                goalWidget.Init(goal.Type, goalSprite, goalAmount);
                _goalWidgets.Add(goalWidget);
            }

            _movesLabel.text = _levelData.Moves.ToString();
        }

        private Sprite GetSpriteByType(string spriteName)
        {
            return _goalSpriteAtlas.GetSprite(spriteName);
        }

        private void UpdateGoalAmount(GoalType type)
        {
            GoalWidget goalWidget;
            for (int i = 0; i < _goalWidgets.Count; i++)
            {
                goalWidget = _goalWidgets[i];

                if (goalWidget.GoalType == type)
                    goalWidget.UpdateAmount();
            }
        }

        private void UpdateRemainingMoves()
        {
            _movesLabel.text = _levelController.TotalMoves.ToString();
        }

        public Vector3 GetGoalWidgetPositionMethod(GoalType goalType)
        {
            GoalWidget goalWidget;
            Vector3 widgetPosition = Vector3.zero;
            for (int i = 0; i < _goalsParent.childCount; i++)
            {
                goalWidget = _goalsParent.GetChild(i).GetComponent<GoalWidget>();
                if (goalWidget.GoalType == goalType)
                {
                    widgetPosition = goalWidget.transform.position;
                    return widgetPosition;
                }
            }

            return widgetPosition;
        }
    }
}