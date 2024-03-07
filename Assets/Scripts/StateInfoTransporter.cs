using System.Collections.Generic;
using NecatiAkpinar.Mono;
using NecatiAkpinar.Abstracts;

namespace NecatiAkpinar.PhaseStates
{
    public class StateInfoTransporter
    {
        private TileMono _selectedTile;
        private List<TileMono> _matchedTileses = new List<TileMono>();
        public TileMono SelectedTile => _selectedTile;
        public List<TileMono> MatchedTiles => _matchedTileses;

        public StateInfoTransporter()
        {
        }

        public StateInfoTransporter(TileMono selectedTile)
        {
            _selectedTile = selectedTile;
        }

        public StateInfoTransporter(List<TileMono> matchedTileses)
        {
            _matchedTileses = matchedTileses;
        }
    }
}