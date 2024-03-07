using System;
using System.Collections.Generic;
using NecatiAkpinar.Enums;
using NecatiAkpinar.Controllers;
using NecatiAkpinar.Interfaces;
using NecatiAkpinar.Miscs;
using UnityEngine;

namespace NecatiAkpinar.Abstracts
{
    public abstract class BaseTileElement : MonoBehaviour
    {
        [SerializeField] private GameElementType _elementType;
        public GameElementType ElementType => _elementType;

        protected SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public virtual void Start()
        {
        }

        public virtual void Reset()
        {
            transform.parent = null;
            gameObject.SetActive(false);
            transform.position = Vector3.zero;
            transform.rotation = Quaternion.identity;
        }

        public void SetSortingOrder(int sortingOrder)
        {
            if (_spriteRenderer != null)
                _spriteRenderer.sortingOrder = sortingOrder;
        }


        public virtual void ReturnToPool()
        {
        }
    }
}