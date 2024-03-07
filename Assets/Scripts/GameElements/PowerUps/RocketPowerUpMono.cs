using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using NecatiAkpinar.Enums;
using NecatiAkpinar.Interfaces;
using NecatiAkpinar.Managers;
using NecatiAkpinar.Mono;

namespace NecatiAkpinar.GameElements.PowerUps
{
    public class RocketPowerUpMono : PowerUpMono, IDroppableElement, IPlayableElement, IActivatable
    {
        [SerializeField] private Transform _rocketPart1; // Top, Left
        [SerializeField] private Transform _rocketPart2; // Bottom. Right

        private readonly float _flyDuration = 1.0f;
        private readonly float _targetEndPosition = 10.0f;

        private Vector3 _rocketPart1StartingPosition, _rocketPart2StartingPosition;

        public override void Start()
        {
            _rocketPart1StartingPosition = _rocketPart1.transform.localPosition;
            _rocketPart2StartingPosition = _rocketPart2.transform.localPosition;
            base.Start();
        }

        public IEnumerator PlayVerticalMovement()
        {
            _rocketPart1.DOMoveY(_rocketPart1.transform.position.x + _targetEndPosition, _flyDuration);
            _rocketPart2.DOMoveY(_rocketPart2.transform.position.x - _targetEndPosition, _flyDuration).OnComplete((() => { ReturnToPool(); }));
            yield return null;
        }

        public IEnumerator PlayHorizontalMovement()
        {
            _rocketPart1.DOMoveX(_rocketPart1.transform.position.x - _targetEndPosition, _flyDuration);
            _rocketPart2.DOMoveX(_rocketPart2.transform.position.x + _targetEndPosition, _flyDuration).OnComplete((() => { ReturnToPool(); }));
            yield return null;
        }

        public IEnumerator Activate(bool isInGoals)
        {
            if (ElementType == GameElementType.RocketVertical)
                yield return StartCoroutine(PlayVerticalMovement());
            else if (ElementType == GameElementType.RocketHorizontal)
                yield return StartCoroutine(PlayHorizontalMovement());

            yield break;
        }

        public override void Reset()
        {
            base.Reset();
            _rocketPart1.transform.localPosition = _rocketPart1StartingPosition;
            _rocketPart2.transform.localPosition = _rocketPart2StartingPosition;
        }

        public override void ReturnToPool()
        {
            base.ReturnToPool();

            TileElementPoolManager.Instance.ReturnToPool(ElementType, this);
        }
    }
}