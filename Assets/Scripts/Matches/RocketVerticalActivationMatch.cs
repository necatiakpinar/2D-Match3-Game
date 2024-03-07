using System;
using System.Collections;
using System.Collections.Generic;
using NecatiAkpinar.Abstracts;
using NecatiAkpinar.Controllers;
using NecatiAkpinar.Enums;
using NecatiAkpinar.Managers;
using NecatiAkpinar.Miscs;
using NecatiAkpinar.Mono;
using UnityEngine;

namespace NecatiAkpinar.Matches
{
    public class RocketVerticalActivationMatch : BaseMatch
    {
        private PowerUpController _powerUpController;
        private MatchCreationController _matchCreationController;
        private List<TileMono> _newCreatedMatchMatchedTiles;
        private BaseMatch _newPowerUpMatch;

        public RocketVerticalActivationMatch(TileMono selectedTile, List<TileMono> matchedTiles)
        {
            _selectedTile = selectedTile;
            _matchedTiles = new List<TileMono>(matchedTiles);
            _newCreatedMatchMatchedTiles = new List<TileMono>();
        }

        public override IEnumerator ActivateMatch(Action<BaseMatch> newMatchCallback)
        {
            base.ActivateMatch(newMatchCallback);
            _selectedTile.ActivateElement(false);

            foreach (var matchedtileElement in _matchedTiles)
            {
                if (matchedtileElement.TileElement && GameElementHelper.IsPowerUp(matchedtileElement.TileElement.ElementType))
                {
                    _powerUpController = new PowerUpController(matchedtileElement);
                    _newCreatedMatchMatchedTiles = _powerUpController.GetMatchedTiles((PowerUpType)matchedtileElement.TileElement.ElementType);
                    _matchCreationController = new MatchCreationController(matchedtileElement, _newCreatedMatchMatchedTiles);
                    _newPowerUpMatch = _matchCreationController.GetPowerUpActivationMatch((PowerUpType)matchedtileElement.TileElement.ElementType);
                    newMatchCallback?.Invoke(_newPowerUpMatch);
                }

                if (matchedtileElement.TileElement)
                    _isGoalExist = EventManager.IsGoalAvailable != null && EventManager.IsGoalAvailable((GoalType)matchedtileElement.TileElement.ElementType);
                
                yield return matchedtileElement.ActivateElement(_isGoalExist);
            }

            _isActivationOperationFinished = true;
        }
    }
}