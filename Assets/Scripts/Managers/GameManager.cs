using System;
using System.Collections;
using System.Collections.Generic;
using NecatiAkpinar.Abstracts;
using NecatiAkpinar.Controllers;
using NecatiAkpinar.Enums;
using NecatiAkpinar.GameStates;
using NecatiAkpinar.Miscs;
using UnityEngine;

namespace NecatiAkpinar.Managers
{
    public class GameManager : MonoBehaviour
    {
        private StartingState _startingState;
        private InGameState _inGameState;
        private LevelEndState _levelEndState;

        private BaseGameState _currentState;

        private void Start()
        {
            if (DataManager.Instance == null)
            {
                Debug.LogError("Data manager is not exist! Provide data to proceed forward!");
                return;
            }

            GridController gridController = GameReferences.Instance.GridController;

            _startingState = new StartingState(ChangeGameState, gridController);
            _inGameState = new InGameState(ChangeGameState);
            _levelEndState = new LevelEndState(ChangeGameState);

            GameStateInfoTransporter infoTransporter = new GameStateInfoTransporter();
            ChangeGameState(GameStateType.Starting, infoTransporter);
        }

        private void ChangeGameState(GameStateType stateType, GameStateInfoTransporter _infoTransporter)
        {
            switch (stateType)
            {
                case GameStateType.Starting:
                    _currentState = _startingState;
                    break;
                case GameStateType.InGame:
                    _currentState = _inGameState;
                    break;
                case GameStateType.LevelEnd:
                    _currentState = _levelEndState;
                    break;
            }

            _currentState.Start(_infoTransporter);
        }
    }
}