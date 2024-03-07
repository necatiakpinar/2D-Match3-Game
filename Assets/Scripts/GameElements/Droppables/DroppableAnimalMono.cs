using System.Collections;
using NecatiAkpinar.Enums;
using NecatiAkpinar.GFX;
using NecatiAkpinar.Interfaces;
using NecatiAkpinar.Managers;
using NecatiAkpinar.Controllers;
using NecatiAkpinar.Miscs;
using UnityEngine;

namespace NecatiAkpinar.GameElements.Droppables
{
    public class DroppableAnimalMono : DroppableElementMono, IDroppableElement, ICollectable
    {
        private GFXAnimalController _gfxController;

        public override void Start()
        {
            base.Start();
            _gfxController = new GFXAnimalController(this);
        }

        public IEnumerator Collect(bool isInGoals)
        {
            if (isInGoals)
                EventManager.OnGoalUpdatedUI?.Invoke((GoalType)ElementType);

            StartCoroutine(_gfxController.Collect());
            ReturnToPool();
            yield return null;
        }

        public override void ReturnToPool()
        {
            base.ReturnToPool();
            //Send this object to pool! Destroy until pool is implemented
            TileElementPoolManager.Instance.ReturnToPool(ElementType, this);
        }
    }
}