using System;
using System.Collections;
using System.Collections.Generic;
using NecatiAkpinar.Abstracts;
using NecatiAkpinar.Enums;
using NecatiAkpinar.Managers;
using NecatiAkpinar.Miscs;
using NecatiAkpinar.Mono;
using UnityEngine;

namespace NecatiAkpinar.Matches
{
    public class BlockerActivationMatch : BaseMatch
    {
        private string _blockerSFXKey;

        public BlockerActivationMatch(List<TileMono> matchedTiles)
        {
            _matchedTiles = new List<TileMono>(matchedTiles);

            if (_matchedTiles.Count > 0)
            {
                _blockerSFXKey = GFXHelper.GetActivatableElementSFXKey(_matchedTiles[0].TileElement.ElementType);
                _isGoalExist = EventManager.IsGoalAvailable != null && EventManager.IsGoalAvailable((GoalType)_matchedTiles[0].TileElement.ElementType);
            }
        }

        public override IEnumerator ActivateMatch(Action<BaseMatch> newMatchCallback)
        {
            base.ActivateMatch(newMatchCallback);

            if (_matchedTiles.Count == 0)
                yield break;

            foreach (var matchedtileElement in _matchedTiles)
            {
                yield return matchedtileElement.ActivateElement(_isGoalExist);
            }

            GFXManager.Instance.PlaySFX(_blockerSFXKey);

            _isActivationOperationFinished = true;
        }
    }
}