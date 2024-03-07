using System;
using NecatiAkpinar.Controllers;
using NecatiAkpinar.Data;
using NecatiAkpinar.Managers;
using UnityEngine;

namespace NecatiAkpinar.Miscs
{
    public class GameReferences : MonoBehaviour
    {
        [SerializeField] private GridController _gridController;
        [SerializeField] private LevelController _levelController;
        public GridController GridController => _gridController;
        public LevelController LevelController => _levelController;
        
        public static GameReferences Instance;
        

        private void Awake()
        {
            if (Instance != null && Instance != this)
                Destroy(this);
            else
                Instance = this;
        }

        public void Start()
        {
            LevelData _currentLevel = DataManager.Instance.LevelDataContainer.AllLevels[0];

            _levelController = new LevelController(_currentLevel);
            _gridController = new GridController(DataManager.Instance);
        }
    }
}