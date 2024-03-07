using System.Collections;
using NecatiAkpinar.Enums;
using NecatiAkpinar.GFX;
using NecatiAkpinar.Interfaces;
using NecatiAkpinar.Managers;
using NecatiAkpinar.GameElements.Blockers;
using UnityEngine;

namespace NecatiAkpinar.GameElements.Blockers
{
    public class BalloonBlockerMono : BlockerMono, IDroppableElement, IActivatable
    {
        private GFXBalloonBlockerController _gfxController;

        public override void Start()
        {
            base.Start();
            _gfxController = new GFXBalloonBlockerController(this);
        }

        public IEnumerator Activate(bool isInGoals)
        {
            if (isInGoals)
                EventManager.OnGoalUpdatedUI?.Invoke((GoalType)ElementType);

            StartCoroutine(_gfxController.Activate());

            ReturnToPool();
            yield return null;
        }

        public override void ReturnToPool()
        {
            base.ReturnToPool();
            TileElementPoolManager.Instance.ReturnToPool(ElementType, this);
        }
    }
}