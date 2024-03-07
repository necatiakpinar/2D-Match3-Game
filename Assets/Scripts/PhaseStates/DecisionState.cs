using System;
using System.Collections;
using System.Collections.Generic;
using NecatiAkpinar.Abstracts;
using NecatiAkpinar.Controllers;
using NecatiAkpinar.Enums;
using NecatiAkpinar.Managers;
using NecatiAkpinar.Matches;
using NecatiAkpinar.Miscs;
using NecatiAkpinar.Mono;
using NecatiAkpinar.Data;
using UnityEngine;

namespace NecatiAkpinar.PhaseStates
{
    public class DecisionState : BasePhaseState
    {
        private Action<PhaseStateType, StateInfoTransporter> _changeStateCallback;
        private MatchActivatorController _matchActivator;
        private MonoBehaviour _monoBehaviour;

        private TileMono _selectedTile;

        private List<BaseMatch> _matches;
        private List<TileMono> _matchedTiles;
        private List<TileMono> _blockerTiles;

        private int _recursiveCallCount;

        private TileMono[,] Tiles;

        private PowerUpController _powerUpController;
        private MatchCreationController _matchCreationController;

        public DecisionState(Action<PhaseStateType, StateInfoTransporter> changeStateCallback, MatchActivatorController matchActivator, MonoBehaviour monoBehaviour)
        {
            _changeStateCallback = changeStateCallback;
            _matchActivator = matchActivator;
            _monoBehaviour = monoBehaviour;

            _matches = new List<BaseMatch>();
            _matchedTiles = new List<TileMono>();
            _blockerTiles = new List<TileMono>();
            _recursiveCallCount = 0;
            Tiles = GameReferences.Instance.GridController.Tiles;
        }

        public override void Start(StateInfoTransporter stateInfoTransporter)
        {
            _selectedTile = stateInfoTransporter.SelectedTile;
            _powerUpController = new PowerUpController(_selectedTile);

            if (GameElementHelper.IsColorCube(_selectedTile.TileElement.ElementType))
                _monoBehaviour.StartCoroutine(CheckForColorMatch(_selectedTile));
            else if (GameElementHelper.IsPowerUp(_selectedTile.TileElement.ElementType))
                _monoBehaviour.StartCoroutine(CheckForPowerUpClick());
        }

        public override void End()
        {
            StateInfoTransporter stateInfoTranporter = new StateInfoTransporter(_matchedTiles);
            _changeStateCallback.Invoke(PhaseStateType.Fill, stateInfoTranporter);

            _matches = new List<BaseMatch>();
            _matchedTiles = new List<TileMono>();
            _blockerTiles = new List<TileMono>();
        }

        public IEnumerator CheckForColorMatch(TileMono elementToCheckMatch)
        {
            _recursiveCallCount++;

            foreach (TileMono neighbourTile in elementToCheckMatch.Neighbours.Values)
            {
                if (!elementToCheckMatch.HasElement() || !neighbourTile.HasElement() || neighbourTile.TileType == TileType.Spawner)
                    continue;

                if (elementToCheckMatch.TileElement.ElementType == neighbourTile.TileElement.ElementType)
                {
                    if (!_matchedTiles.Contains(neighbourTile))
                    {
                        _matchedTiles.Add(neighbourTile);
                        _monoBehaviour.StartCoroutine(CheckForColorMatch(neighbourTile));
                    }
                }
                else if (GameElementHelper.IsBlocker(neighbourTile.TileElement.ElementType))
                {
                    _blockerTiles.Add(neighbourTile);
                }
            }

            _recursiveCallCount--;

            if (_recursiveCallCount == 0)
            {
                if (_matchedTiles.Count > 0)
                {
                    ColorActivationMatch colorActivationMatch = new ColorActivationMatch(_matchedTiles);
                    _matches.Add(colorActivationMatch);

                    TryCreatePowerUp();
                    yield return _monoBehaviour.StartCoroutine(TryActivateBlocker());

                    _matchedTiles.AddRange(_blockerTiles);
                    yield return _monoBehaviour.StartCoroutine(_matchActivator.ActivateMatches(_matches, OnNewMatchActivated));

                    _matches = new List<BaseMatch>();

                    TryActivateDroppables();

                    if (_matches.Count > 0)
                        yield return _monoBehaviour.StartCoroutine(_matchActivator.ActivateMatches(_matches, OnNewMatchActivated));

                    EventManager.OnTileActivated?.Invoke();
                }

                End();
            }
        }

        public void TryCreatePowerUp()
        {
            _matchCreationController = new MatchCreationController(_selectedTile, _matchedTiles);
            BaseMatch foundMatch = _matchCreationController.GetPowerUpCreationMatch();
            if (foundMatch != null)
                _matches.Add(foundMatch);
        }

        public IEnumerator TryActivateBlocker()
        {
            if (_blockerTiles.Count == 0)
                yield return null;

            BlockerActivationMatch blockerActivationMatch = new BlockerActivationMatch(_blockerTiles);
            _matches.Add(blockerActivationMatch);
        }

        public void TryActivateDroppables()
        {
            List<TileMono> droppableTiles = GridCalculatorHelper.GetDroppableElementBottomTiles();
            if (droppableTiles.Count == 0)
                return;

            DroppableAnimalCollectedMatch droppableAnimalCollectedMatch = new DroppableAnimalCollectedMatch(droppableTiles);
            _matches.Add(droppableAnimalCollectedMatch);
            _matchedTiles.AddRange(droppableTiles);
        }

        private IEnumerator CheckForPowerUpClick()
        {
            if (GameElementHelper.IsPowerUp(_selectedTile.TileElement.ElementType))
            {
                List<TileMono> matchedTiles = _powerUpController.GetMatchedTiles((PowerUpType)_selectedTile.TileElement.ElementType);
                _matchedTiles.AddRange(matchedTiles);

                _matchCreationController = new MatchCreationController(_selectedTile, _matchedTiles);
                BaseMatch powerUpMatch = _matchCreationController.GetPowerUpActivationMatch((PowerUpType)_selectedTile.TileElement.ElementType);
                _matches.Add(powerUpMatch);
            }

            if (_matches.Count > 0)
            {
                yield return _monoBehaviour.StartCoroutine(_matchActivator.ActivateMatches(_matches, OnNewMatchActivated));
                EventManager.OnTileActivated?.Invoke();
            }

            End();
        }

        private void OnNewMatchActivated(List<TileMono> newMatchedTiles)
        {
            _matchedTiles.AddRange(newMatchedTiles);
        }
    }
}