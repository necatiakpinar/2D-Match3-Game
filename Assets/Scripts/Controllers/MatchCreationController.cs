using System;
using System.Collections.Generic;
using NecatiAkpinar.Abstracts;
using NecatiAkpinar.Enums;
using NecatiAkpinar.Matches;
using NecatiAkpinar.Mono;

namespace NecatiAkpinar.Controllers
{
    public class MatchCreationController
    {
        private TileMono _selectedTile;
        private List<TileMono> _matchedTiles;

        private Dictionary<PowerUpType, BaseMatch> _powerUpActivationMatches;
        private Dictionary<int, BaseMatch> _powerUpCreationMatches;

        private readonly int _rocketCreationAmount = 5;

        public MatchCreationController(TileMono selectedTile, List<TileMono> matchedTiles)
        {
            _selectedTile = selectedTile;
            _matchedTiles = matchedTiles;

            _powerUpActivationMatches = new Dictionary<PowerUpType, BaseMatch>()
            {
                { PowerUpType.RocketHorizontal, new RocketHorizontalActivationMatch(_selectedTile, _matchedTiles) },
                { PowerUpType.RocketVertical, new RocketVerticalActivationMatch(_selectedTile, _matchedTiles) },
            };

            _powerUpCreationMatches = new Dictionary<int, BaseMatch>()
            {
                { _rocketCreationAmount, new RocketPowerUpCreationMatch(_selectedTile, _matchedTiles) },
            };
        }

        public BaseMatch GetPowerUpActivationMatch(PowerUpType powerUpType)
        {
            BaseMatch foundMatch;
            if (_powerUpActivationMatches.TryGetValue(powerUpType, out foundMatch))
                return foundMatch;

            return null;
        }

        public BaseMatch GetPowerUpCreationMatch()
        {
            if (_matchedTiles.Count < _rocketCreationAmount)
                return null;

            BaseMatch foundMatch;
            if (_powerUpCreationMatches.TryGetValue(_rocketCreationAmount, out foundMatch))
                return foundMatch;

            return null;
        }
    }
}