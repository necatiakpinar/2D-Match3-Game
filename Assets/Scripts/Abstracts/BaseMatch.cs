using System;
using System.Collections;
using System.Collections.Generic;
using NecatiAkpinar.Mono;
using NecatiAkpinar.Controllers;
using NecatiAkpinar.Enums;
using NecatiAkpinar.Managers;
using NecatiAkpinar.Matches;
using NecatiAkpinar.Miscs;

namespace NecatiAkpinar.Abstracts
{
    public abstract class BaseMatch
    {
        protected TileMono _selectedTile;
        protected List<TileMono> _matchedTiles = new List<TileMono>();
        protected TileMono[,] _tiles;
        protected bool _isActivationOperationFinished = false;
        protected bool _isGoalExist;
        public List<TileMono> MatchedTiles => _matchedTiles;

        public virtual IEnumerator ActivateMatch(Action<BaseMatch> newMatchCallback)
        {
            yield break;
        }
    }
}