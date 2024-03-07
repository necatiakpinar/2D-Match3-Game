using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using NecatiAkpinar.Abstracts;
using NecatiAkpinar.Data;
using NecatiAkpinar.Enums;
using NecatiAkpinar.Interfaces;
using NecatiAkpinar.Managers;
using NecatiAkpinar.Miscs;
using NecatiAkpinar.Controllers;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

namespace NecatiAkpinar.Mono
{
    public class TileMono : MonoBehaviour
    {
        [SerializeField] private TileType _tileType;
        private Vector2Int _coordinates;
        private Dictionary<TileDirectionType, TileMono> _neighbours;
        private BaseTileElement _tileElement;
        private TileMonoData _data;
        private TileStateType _state;

        public TileType TileType
        {
            get { return _tileType; }
            set { _tileType = value; }
        }

        public Vector2Int Coordinates => _coordinates;
        public Dictionary<TileDirectionType, TileMono> Neighbours => _neighbours;
        public BaseTileElement TileElement => _tileElement;

        public TileStateType State => _state;

        public void Init(TileMonoData tileMonoData, int rowX, int columnY)
        {
            _data = tileMonoData;
            _coordinates = new Vector2Int(rowX, columnY);
            transform.position = new Vector3(_coordinates.x, _coordinates.y, transform.position.z);
            _state = TileStateType.Empty;
        }

        public void SetNeighbours(Dictionary<TileDirectionType, TileMono> neighbours)
        {
            _neighbours = neighbours;
        }

        public void SetTileElement(BaseTileElement tileElement)
        {
            _tileElement = tileElement;
            _tileElement.transform.parent = this.transform;
            _state = TileStateType.Idle;
        }

        public bool HasElement() => _tileElement;
        public bool CanTakeElement() => _neighbours[TileDirectionType.Up];

        public TileMono GetNeighbourAt(TileDirectionType directionType) => _neighbours[directionType];

        public IEnumerator TakeElement()
        {
            if (_tileType == TileType.Spawner)
                yield break;

            TileMono topNeighbourTile = _neighbours[TileDirectionType.Up];

            if (topNeighbourTile == null)
                yield break;

            yield return StartCoroutine(topNeighbourTile.DropElement());
        }

        public IEnumerator DropElement()
        {
            TileMono bottomNeighbourTile = _neighbours[TileDirectionType.Down];

            if (bottomNeighbourTile == null || bottomNeighbourTile.TileElement != null)
                yield return null;

            if (_tileType == TileType.Spawner)
            {
                BaseTileElement tileElement;
                tileElement = GameReferences.Instance.GridController.CreateAllowedGridElement(bottomNeighbourTile);
                tileElement.transform.DOLocalMove(Vector3.zero, _data.TileElementDropSpeed);
                bottomNeighbourTile.SetTileElement(tileElement);
                tileElement.SetSortingOrder(bottomNeighbourTile.Coordinates.y);
                
                yield return new WaitForSeconds(_data.TileElementDropSpeed);

                yield return null;
            }
            else if (TileElement != null)
            {
                bottomNeighbourTile.SetTileElement(TileElement);
                TileElement.transform.DOLocalMove(Vector3.zero, _data.TileElementDropSpeed);
                TileElement.SetSortingOrder(bottomNeighbourTile.Coordinates.y);
                yield return new WaitForSeconds(_data.TileElementDropSpeed);

                _tileElement = null;
                _state = TileStateType.Empty;

                if (bottomNeighbourTile.Coordinates.y == 0 && GameElementHelper.IsDroppableAnimal(bottomNeighbourTile.TileElement.ElementType))
                {
                    bool isGoalExist = EventManager.IsGoalAvailable != null && EventManager.IsGoalAvailable((GoalType)bottomNeighbourTile.TileElement.ElementType);
                    yield return StartCoroutine(bottomNeighbourTile.CollectElement(isGoalExist));
                    yield return StartCoroutine(TakeElement());
                    yield return bottomNeighbourTile.StartCoroutine(bottomNeighbourTile.TakeElement());

                    yield return null;
                }
                else
                {
                    yield return StartCoroutine(TakeElement());
                }
            }
        }

        public IEnumerator ActivateElement(bool isInGoals)
        {
            if (_tileElement == null)
                yield break;

            IActivatable activatableElement = _tileElement as IActivatable;

            if (activatableElement != null)
            {

                _state = TileStateType.Activation;
                yield return StartCoroutine(activatableElement.Activate(isInGoals));
                
                if (isInGoals)
                    EventManager.OnGoalUpdated?.Invoke((GoalType)_tileElement.ElementType);

                _tileElement = null;
                _state = TileStateType.Empty;

                yield return null;
            }
        }

        public IEnumerator CollectElement(bool isInGoals)
        {
            if (_tileElement == null)
                yield break;

            ICollectable collectableElement = _tileElement as ICollectable;

            if (collectableElement != null)
            {

                _state = TileStateType.Activation;
                yield return StartCoroutine(collectableElement.Collect(isInGoals));

                if (isInGoals)
                    EventManager.OnGoalUpdated?.Invoke((GoalType)_tileElement.ElementType);

                _tileElement = null;
                _state = TileStateType.Empty;

                yield return null;
            }
        }
    }
}