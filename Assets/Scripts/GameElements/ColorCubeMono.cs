using System.Collections;
using DG.Tweening;
using NecatiAkpinar.Abstracts;
using NecatiAkpinar.Enums;
using NecatiAkpinar.GFX;
using NecatiAkpinar.Interfaces;
using NecatiAkpinar.Managers;
using NecatiAkpinar.Miscs;
using UnityEngine;

namespace NecatiAkpinar.GameElements.ColorCubes
{
    public class ColorCubeMono : BaseTileElement, IGridElement, IDroppableElement, IPlayableElement, IActivatable
    {
        private readonly float _flyDuration = 1.0f;
        private Vector3 _targetPosition;
        private GFXColorStoneController _gfxController;

        public override void Start()
        {
            base.Start();
            _gfxController = new GFXColorStoneController(this);
        }

        private IEnumerator TryFlyToGoals()
        {
            _spriteRenderer.sortingLayerName = Constants.SORTINGLAYER_VFX;
            SetSortingOrder(0);

            _targetPosition = EventManager.GetGoalWidgetPosition((GoalType)ElementType);
            transform.DOMove(_targetPosition, _flyDuration).OnComplete(() =>
            {
                EventManager.OnGoalUpdatedUI?.Invoke((GoalType)ElementType);
                StartCoroutine(_gfxController.Collect());
                ReturnToPool();
            });

            yield return null;
        }

        public IEnumerator Activate(bool isInGoals)
        {
            if (isInGoals)
            {
                StartCoroutine(_gfxController.Activate());
                StartCoroutine(TryFlyToGoals());
                yield break;
            }

            //EventManager.OnGoalUpdatedUI?.Invoke((GoalType)ElementType);
            yield return StartCoroutine(_gfxController.Activate());

            ReturnToPool();
            yield return null;
        }
        
        public override void ReturnToPool()
        {
            base.ReturnToPool();
            //Reset it's values
            _spriteRenderer.sortingLayerName = Constants.SORTINGLAYER_COLORCUBE;
            SetSortingOrder(0);
            
            TileElementPoolManager.Instance.ReturnToPool(ElementType,this);
        }
    }
}