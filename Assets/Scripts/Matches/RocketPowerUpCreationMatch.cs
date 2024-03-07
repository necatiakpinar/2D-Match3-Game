using System;
using System.Collections;
using System.Collections.Generic;
using NecatiAkpinar.Abstracts;
using NecatiAkpinar.Enums;
using NecatiAkpinar.Miscs;
using NecatiAkpinar.Mono;
using NecatiAkpinar.GameElements.PowerUps;

namespace NecatiAkpinar.Matches
{
    public class RocketPowerUpCreationMatch : BaseMatch
    {
        public RocketPowerUpCreationMatch(TileMono selectedTile, List<TileMono> matchedTiles)
        {
            _selectedTile = selectedTile;
            _matchedTiles = _matchedTiles = new List<TileMono>(matchedTiles);;
        }

        public override IEnumerator ActivateMatch(Action<BaseMatch> newMatchCallback)
        {
            base.ActivateMatch(newMatchCallback);
            int randomIndex = UnityEngine.Random.Range(0, 2);
            GameElementType randomRocketType;
            if (randomIndex == 0)
                randomRocketType = GameElementType.RocketHorizontal;
            else
                randomRocketType = GameElementType.RocketVertical;

            BaseTileElement powerUpElement = GameReferences.Instance.GridController.CreatePowerUpElement(_selectedTile, randomRocketType);
            _selectedTile.SetTileElement(powerUpElement);
            _isActivationOperationFinished = true;

            yield return null;
        }
    }
}