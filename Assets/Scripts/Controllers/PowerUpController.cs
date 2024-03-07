using System;
using System.Collections.Generic;
using NecatiAkpinar.Enums;
using NecatiAkpinar.Miscs;
using NecatiAkpinar.Mono;
using NecatiAkpinar.Abstracts;
using NecatiAkpinar.Matches;

namespace NecatiAkpinar.Controllers
{
    public class PowerUpController
    {
        private TileMono _selectedTile;
        private List<TileMono> _matchedTiles;

        private Dictionary<PowerUpType, Func<List<TileMono>>> _powerUpMatchTiles;

        public PowerUpController(TileMono selectedTile)
        {
            _selectedTile = selectedTile;
            _matchedTiles = new List<TileMono>();

            _powerUpMatchTiles = new Dictionary<PowerUpType, Func<List<TileMono>>>()
            {
                { PowerUpType.RocketHorizontal, GetRocketHorizontalTiles },
                { PowerUpType.RocketVertical, GetRocketVerticalTiles },
            };
        }

        private List<TileMono> GetRocketHorizontalTiles()
        {
            _matchedTiles.Add(_selectedTile);
            _matchedTiles.AddRange(GridCalculatorHelper.GetHorizontalTilesForRocket(_selectedTile));
            return _matchedTiles;
        }

        private List<TileMono> GetRocketVerticalTiles()
        {
            _matchedTiles.Add(_selectedTile);
            _matchedTiles.AddRange(GridCalculatorHelper.GetVerticalTilesForRocket(_selectedTile));
            return _matchedTiles;
        }

        public List<TileMono> GetMatchedTiles(PowerUpType powerUpType)
        {
            if (_powerUpMatchTiles.TryGetValue(powerUpType, out var tiles))
                return tiles();

            return null;
        }
        
    }
}