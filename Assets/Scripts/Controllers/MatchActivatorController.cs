using System;
using System.Collections;
using System.Collections.Generic;
using NecatiAkpinar.Abstracts;
using NecatiAkpinar.Mono;
using UnityEngine;

namespace NecatiAkpinar.Controllers
{
    public class MatchActivatorController
    {
        private MonoBehaviour _monoBehaviour;

        private Queue<BaseMatch> _matchesToActivate;

        private Action<List<TileMono>> _onNewMatchActivated;

        public MatchActivatorController(MonoBehaviour monoBehaviour)
        {
            _monoBehaviour = monoBehaviour;
            _matchesToActivate = new Queue<BaseMatch>();
        }
        
        public IEnumerator ActivateMatches(List<BaseMatch> matches, Action<List<TileMono>> onNewMatchActivated)
        {
            _matchesToActivate = new Queue<BaseMatch>(matches);
            BaseMatch currentMatch;
            _onNewMatchActivated = onNewMatchActivated;
            
            while (_matchesToActivate.Count > 0)
            {
                currentMatch = _matchesToActivate.Peek();
                yield return _monoBehaviour.StartCoroutine(currentMatch.ActivateMatch(OnNewMatchCreation));
                _matchesToActivate.Dequeue();
            }
        }

        public void OnNewMatchCreation(BaseMatch newMatch)
        {
            _matchesToActivate.Enqueue(newMatch);
            _onNewMatchActivated?.Invoke(newMatch.MatchedTiles);
        }
    }
}