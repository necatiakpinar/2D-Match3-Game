using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NecatiAkpinar.Abstracts;
using NecatiAkpinar.Enums;
using NecatiAkpinar.Miscs;
using NecatiAkpinar.Mono;
using UnityEngine;

namespace NecatiAkpinar.PhaseStates
{
    public class FillState : BasePhaseState
    {
        private Action<PhaseStateType, StateInfoTransporter> _changeStateCallback;

        private List<TileMono> _matchedTiles;

        private List<TileMono> _emptyTiles;
        private MonoBehaviour _monoBehaviour;

        public FillState(Action<PhaseStateType, StateInfoTransporter> changeStateCallback, MonoBehaviour monoBehaviour)
        {
            _changeStateCallback = changeStateCallback;
            _monoBehaviour = monoBehaviour;
            _matchedTiles = new List<TileMono>();
            _emptyTiles = new List<TileMono>();
        }

        public override void Start(StateInfoTransporter stateInfoTransporter)
        {
            _matchedTiles = stateInfoTransporter.MatchedTiles;
            _monoBehaviour.StartCoroutine(TryFill());
        }

        public override void End()
        {
            StateInfoTransporter stateInfoTranporter = new StateInfoTransporter();
            
            if (GameReferences.Instance.LevelController.IsLevelFinished)
                _changeStateCallback.Invoke(PhaseStateType.Idle, stateInfoTranporter);
            else
                _changeStateCallback.Invoke(PhaseStateType.Input, stateInfoTranporter);
        }

        public void SortMatchedTiles()
        {
            _matchedTiles = _matchedTiles.OrderByDescending(obj => obj.Coordinates.y).ToList();
        }

        public IEnumerator TryFill()
        {
            if (_matchedTiles.Count == 0)
                End();

            TileMono matchedTile;
            TileMono takeElementNeighbourTile;

            if (_matchedTiles.Count > 0)
                SortMatchedTiles();

            for (int i = 0; i < _matchedTiles.Count; i++)
            {
                matchedTile = _matchedTiles[i];

                //If matched tile has neighbour top(top) direction
                if (matchedTile.TileElement == null && matchedTile.CanTakeElement())
                {
                    takeElementNeighbourTile = matchedTile.GetNeighbourAt(TileDirectionType.Up);
                    yield return _monoBehaviour.StartCoroutine(takeElementNeighbourTile.DropElement());
                }
            }

            End();
        }
    }
}