using System;
using NecatiAkpinar.Abstracts;
using NecatiAkpinar.Enums;
using NecatiAkpinar.Interfaces;
using NecatiAkpinar.Miscs;
using NecatiAkpinar.Mono;
using NecatiAkpinar.GameElements.ColorCubes;
using NecatiAkpinar.Managers;
using UnityEngine;
using UnityEngine.EventSystems;

namespace NecatiAkpinar.PhaseStates
{
    public class InputState : BasePhaseState, IPointerDownHandler, IPointerUpHandler
    {
        private Action<PhaseStateType, StateInfoTransporter> _changeStateCallback;
        private Vector2 _firstClickPosition;
        private Camera _mainCamera;
        private TileMono[,] _gridTiles;

        private TileMono _selectedTile;

        public InputState(Action<PhaseStateType, StateInfoTransporter> changeStateCallback)
        {
            _changeStateCallback = changeStateCallback;
            _mainCamera = Camera.main;
            _gridTiles = GameReferences.Instance.GridController.Tiles;
        }

        public override void Start(StateInfoTransporter stateInfoTransporter)
        {
        }

        public override void End()
        {
            StateInfoTransporter stateInfoTranporter = new StateInfoTransporter(_selectedTile);
            _changeStateCallback.Invoke(PhaseStateType.Decision, stateInfoTranporter);

            _selectedTile = null;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            //Store the position of the click
            _firstClickPosition = eventData.position;
            //Get the click position relative to the board
            Vector2 worldPosition = _mainCamera.ScreenToWorldPoint(new Vector3(_firstClickPosition.x, _firstClickPosition.y, -_mainCamera.transform.position.z));
            int x = Mathf.RoundToInt(worldPosition.x);
            int y = Mathf.RoundToInt(worldPosition.y);

            //Select the correct tile
            SelectTile(x, y);
        }

        private void SelectTile(int x, int y)
        {
            if (x < 0 || y < 0 || x >= _gridTiles.GetLength(0) || y >= _gridTiles.GetLength(1))
                return;

            if (_gridTiles[x, y].TileElement is IPlayableElement)
                _selectedTile = _gridTiles[x, y];
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            //If player touches valid tile on the board, exit from the input state
            if (_selectedTile)
                End();
        }
    }
}