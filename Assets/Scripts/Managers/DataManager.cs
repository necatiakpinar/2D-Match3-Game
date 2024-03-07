using System;
using System.Collections;
using System.Collections.Generic;
using NecatiAkpinar.Data;
using NecatiAkpinar.GameElements.Blockers;
using NecatiAkpinar.GameElements.Droppables;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.U2D;

namespace NecatiAkpinar.Managers
{
    public class DataManager : MonoBehaviour
    {
        [SerializeField] private TileMonoData _tileMonoData;
        [SerializeField] private GridData _gridData;
        [SerializeField] private LevelDataContainer _levelDataContainer;
        [SerializeField] private SFXDataContainer _sfxDataContainer;
        [SerializeField] private VFXDataContainer _vfxDataContainer;

        [Header("Sprite Atlasses")] [SerializeField]
        private SpriteAtlas _goalSpriteAtlas;

        public TileMonoData TileMonoData => _tileMonoData;
        public GridData GridData => _gridData;
        public LevelDataContainer LevelDataContainer => _levelDataContainer;

        public SFXDataContainer SFXDataContainer => _sfxDataContainer;
        public VFXDataContainer VFXDataContainer => _vfxDataContainer;

        public SpriteAtlas GoalSpriteAtlas => _goalSpriteAtlas;

        public static DataManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
                Destroy(this);
            else
                Instance = this;
        }
    }
}