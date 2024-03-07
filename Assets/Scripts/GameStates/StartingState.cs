using System;
using NecatiAkpinar.Abstracts;
using NecatiAkpinar.Controllers;
using NecatiAkpinar.Enums;
using UnityEngine;

namespace NecatiAkpinar.GameStates
{
    public class StartingState : BaseGameState
    {
        private Action<GameStateType,GameStateInfoTransporter> _changeStateCallback;
        private GridController _gridController;

        public StartingState(Action<GameStateType, GameStateInfoTransporter> changeStateCallback, GridController gridController)
        {
            _changeStateCallback = changeStateCallback;
            _gridController = gridController;
        }

        public override void Start(GameStateInfoTransporter stateInfoTransporter)
        {
            _gridController.CreateGrid(End);
        }

        public override void End()
        {
            GameStateInfoTransporter infoTransporter = new GameStateInfoTransporter();
            _changeStateCallback?.Invoke(GameStateType.InGame,infoTransporter);
        }
    }
}