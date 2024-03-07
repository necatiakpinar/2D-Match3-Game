using System;
using System.Collections.Generic;
using NecatiAkpinar.Abstracts;
using NecatiAkpinar.Enums;
using NecatiAkpinar.Miscs;
using UnityEngine;

namespace NecatiAkpinar.Managers
{
    [System.Serializable]
    public class Pool
    {
        [SerializeField] private GameElementType _elementType;
        [SerializeField] private BaseTileElement _tileElement;
        [SerializeField] private int _size;

        public GameElementType ElementType => _elementType;
        public BaseTileElement TileElement => _tileElement;
        public int Size => _size;
    }

    public class TileElementPoolManager : MonoBehaviour
    {
        [SerializeField] private List<Pool> pools;

        private Dictionary<GameElementType, Queue<BaseTileElement>> poolDictionary;

        public static TileElementPoolManager Instance;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }

        private void Start()
        {
            Init();
        }

        private void Init()
        {
            poolDictionary = new Dictionary<GameElementType, Queue<BaseTileElement>>();

            foreach (var pool in pools)
            {
                if (!GameReferences.Instance.LevelController.HasExistInSpawnableElements(pool.ElementType) &&
                    !GameElementHelper.IsPowerUp(pool.ElementType))
                    continue;

                Queue<BaseTileElement> objectPool = new Queue<BaseTileElement>();

                for (int i = 0; i < pool.Size; i++)
                {
                    BaseTileElement tileElement = Instantiate(pool.TileElement);
                    tileElement.gameObject.SetActive(false);
                    objectPool.Enqueue(tileElement);
                }

                poolDictionary.Add(pool.ElementType, objectPool);
            }
        }

        public BaseTileElement SpawnFromPool(GameElementType elementType, Vector3 position, Quaternion rotation)
        {
            if (!poolDictionary.ContainsKey(elementType) || poolDictionary[elementType].Count == 0)
                return null;

            BaseTileElement objectToSpawn = poolDictionary[elementType].Dequeue();

            objectToSpawn.gameObject.SetActive(true);
            objectToSpawn.transform.position = position;
            objectToSpawn.transform.rotation = rotation;

            return objectToSpawn;
        }

        public void ReturnToPool(GameElementType _objectType, BaseTileElement objectToReturn)
        {
            if (!poolDictionary.ContainsKey(_objectType))
                return;

            objectToReturn.Reset();
            poolDictionary[_objectType].Enqueue(objectToReturn);
        }
    }
}