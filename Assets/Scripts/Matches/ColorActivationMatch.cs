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
    public class ColorActivationMatch : BaseMatch
    {
        public ColorActivationMatch(List<TileMono> matchedTiles)
        {
            _matchedTiles = new List<TileMono>(matchedTiles);
            if (_matchedTiles.Count > 0)
                _isGoalExist = EventManager.IsGoalAvailable != null && EventManager.IsGoalAvailable((GoalType)_matchedTiles[0].TileElement.ElementType);
        }

        public override IEnumerator ActivateMatch(Action<BaseMatch> newMatchCallback)
        {
            base.ActivateMatch(newMatchCallback);

            foreach (var matchedtileElement in _matchedTiles)
                yield return matchedtileElement.ActivateElement(_isGoalExist);

            GFXManager.Instance.PlaySFX(Constants.SFX_CubeExplode);


            _isActivationOperationFinished = true;
        }
    }
}