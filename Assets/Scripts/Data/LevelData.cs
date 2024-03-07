using System;
using System.Collections.Generic;
using NecatiAkpinar.Enums;
using NecatiAkpinar.Level;
using UnityEngine;
using UnityEngine.Serialization;

namespace NecatiAkpinar.Data
{
    [Serializable]
    public class GameElementSpawnSettings
    {
        [SerializeField] private GameElementType _elementType;
        [Range(0, 100f)] [SerializeField] private float _spawnRatio;

        public GameElementType ElementType => _elementType;
        public float SpawnRatio => _spawnRatio;
    }

    [CreateAssetMenu(fileName = "LevelData", menuName = "ScriptableObjects/LevelData", order = 1)]
    public class LevelData : ScriptableObject
    {
        [SerializeField] private int _levelIndex;
        [SerializeField] private List<GameGoal> _goals;
        [SerializeField] private int _moves;
        [SerializeField] private List<GameElementSpawnSettings> _spawnableElements;
        [SerializeField] private Vector2Int _gridSize;

        public int LevelIndex => _levelIndex;
        public List<GameGoal> Goals => _goals;
        public int Moves => _moves;
        public Vector2Int GridSize => _gridSize;

        public List<GameElementSpawnSettings> SpawnableElements => _spawnableElements;
    }
}