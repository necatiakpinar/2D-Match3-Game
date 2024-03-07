using System;
using System.Collections;
using System.Collections.Generic;
using NecatiAkpinar.Abstracts;
using NecatiAkpinar.Enums;
using NecatiAkpinar.Managers;
using NecatiAkpinar.Mono;

namespace NecatiAkpinar.Matches
{
    public class DroppableAnimalCollectedMatch : BaseMatch
    {
        public DroppableAnimalCollectedMatch(List<TileMono> animalTiles)
        {
            _matchedTiles = _matchedTiles = new List<TileMono>(animalTiles);
            
            if (_matchedTiles.Count > 0)
                _isGoalExist = EventManager.IsGoalAvailable != null && EventManager.IsGoalAvailable((GoalType)_matchedTiles[0].TileElement.ElementType);
        }

        public override IEnumerator ActivateMatch(Action<BaseMatch> newMatchCallback)
        {
            base.ActivateMatch(newMatchCallback);
            foreach (var matchedtileElement in _matchedTiles)
                yield return matchedtileElement.ActivateElement(_isGoalExist);

            _isActivationOperationFinished = true;

            yield return null;
        }
    }
}