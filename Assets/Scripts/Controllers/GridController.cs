using System;
using System.Collections;
using System.Collections.Generic;
using NecatiAkpinar.Abstracts;
using NecatiAkpinar.Data;
using NecatiAkpinar.Enums;
using NecatiAkpinar.GameElements.ColorCubes;
using NecatiAkpinar.Managers;
using NecatiAkpinar.Miscs;
using NecatiAkpinar.Mono;
using NecatiAkpinar.GameElements.Blockers;
using NecatiAkpinar.GameElements.Droppables;
using NecatiAkpinar.GameElements.PowerUps;
using UnityEngine;
using Random = UnityEngine.Random;

namespace NecatiAkpinar.Controllers
{
    public class GridController
    {
        private TileMonoData _tileMonoData;
        private GridData _gridData;
        private LevelDataContainer _levelDataContainer;

        private LevelData _currentLevelData;
        private List<ColorCubeType> _allowedColorCubes = new List<ColorCubeType>();

        private int _currentRow, _currentColumn;

        public TileMono[,] Tiles { get; private set; }

        public Dictionary<Vector2Int, TileMono> TilesDict { get; private set; }

        private Dictionary<TileDirectionType, Vector2Int> _tileDirectionCoordinates = new Dictionary<TileDirectionType, Vector2Int>()
        {
            { TileDirectionType.Up, new Vector2Int(0, 1) },
            { TileDirectionType.Right, new Vector2Int(1, 0) },
            { TileDirectionType.Down, new Vector2Int(0, -1) },
            { TileDirectionType.Left, new Vector2Int(-1, 0) },
        };

        private List<ColorCubeType> _colorCubeTypes = new List<ColorCubeType>()
        {
            ColorCubeType.YellowCube,
            ColorCubeType.BlueCube,
            ColorCubeType.GreenCube,
            ColorCubeType.RedCube,
            ColorCubeType.PurpleCube,
        };

        public GridController(DataManager monodDataManager)
        {
            _tileMonoData = monodDataManager.TileMonoData;
            _gridData = monodDataManager.GridData;
            _levelDataContainer = monodDataManager.LevelDataContainer;
            _currentLevelData = _levelDataContainer.AllLevels[0];

            Tiles = new TileMono[_currentLevelData.GridSize.x, _currentLevelData.GridSize.y + 1];
            TilesDict = new Dictionary<Vector2Int, TileMono>();
        }

        public void CreateGrid(Action OnGridCreated)
        {
            if (_currentLevelData == null)
                Debug.LogError("Level data could not be loaded");

            GameElementType spawnableElement;
            for (int i = 0; i < _currentLevelData.SpawnableElements.Count; i++)
            {
                spawnableElement = _currentLevelData.SpawnableElements[i].ElementType;
                if (GameElementHelper.IsColorCube(spawnableElement))
                    _allowedColorCubes.Add((ColorCubeType)spawnableElement);
            }

            TileMono tile;
            BaseTileElement tileElement;
            for (int y = 0; y < Tiles.GetLength(1); y++) //+1 for spawners
            {
                for (int x = 0; x < Tiles.GetLength(0); x++)
                {
                    tile = GameObject.Instantiate(_gridData.TilePrefab);
                    tile.Init(_tileMonoData, x, y);

                    Tiles[x, y] = tile;
                    TilesDict.Add(new Vector2Int(x, y), tile);

                    if (y == _currentLevelData.GridSize.y)
                    {
                        tile.TileType = TileType.Spawner;
                        continue;
                    }

                    tile.TileType = TileType.Playable;
                    tileElement = CreateAllowedGridElement(tile, y);


                    if (tileElement is ColorCubeMono)
                    {
                        ColorCubeMono colorCubeMono = tileElement as ColorCubeMono;
                        colorCubeMono.SetSortingOrder(y);
                    }

                    tile.SetTileElement(tileElement);
                }
            }

            CalculateTileNeighbours();
            
            OnGridCreated?.Invoke();
        }

        public void CalculateTileNeighbours()
        {
            Dictionary<TileDirectionType, TileMono> neighbours = new Dictionary<TileDirectionType, TileMono>();
            Vector2Int possibleNeighbourCoordinates;
            int directionCount = Enum.GetValues(typeof(TileDirectionType)).Length;

            foreach (TileMono tile in TilesDict.Values)
            {
                neighbours = new Dictionary<TileDirectionType, TileMono>();

                for (int i = 0; i < directionCount; i++)
                {
                    possibleNeighbourCoordinates = tile.Coordinates + _tileDirectionCoordinates[(TileDirectionType)i];
                    if (TilesDict.ContainsKey(possibleNeighbourCoordinates))
                        neighbours.Add((TileDirectionType)i, TilesDict[possibleNeighbourCoordinates]);

                    tile.SetNeighbours(neighbours);
                }
            }
        }

        public BaseTileElement CreateAllowedGridElement(TileMono parent, int tileHeight = -1)
        {
            GameElementType spawnableElementType = ChooseSpawnableElementType();

            if (tileHeight == 0)
                while (GameElementHelper.IsDroppableAnimal(spawnableElementType))
                    spawnableElementType = ChooseSpawnableElementType();

            if (spawnableElementType != GameElementType.None)
                return CreateTileElement(parent, spawnableElementType);

            return null;
        }

        public BaseTileElement CreatePowerUpElement(TileMono parent, GameElementType powerUpType)
        {
            return CreateTileElement(parent, powerUpType);
        }

        public GameElementType ChooseSpawnableElementType()
        {
            float total = 0;
            foreach (var spawnableElement in _currentLevelData.SpawnableElements)
                total += spawnableElement.SpawnRatio;

            float random = Random.Range(0, total);
            foreach (var colorRatio in _currentLevelData.SpawnableElements)
            {
                if (random < colorRatio.SpawnRatio)
                    return colorRatio.ElementType;

                random -= colorRatio.SpawnRatio;
            }

            return GameElementType.None;
        }

        public BaseTileElement CreateTileElement(TileMono tile, GameElementType elementType)
        {
            BaseTileElement tileElement = TileElementPoolManager.Instance.SpawnFromPool(elementType, Vector3.zero, Quaternion.identity);
            tileElement.transform.parent = tile.transform;
            tileElement.transform.localPosition = Vector3.zero;
            tileElement.gameObject.SetActive(true);
            return tileElement;
        }

        public BaseTileElement CreateRandomColorCube(TileMono parent)
        {
            int randomCubeIndex = Random.Range(0, _allowedColorCubes.Count);
            ColorCubeType colorCubeType = _allowedColorCubes[randomCubeIndex];
            return CreateTileElement(parent, (GameElementType)colorCubeType);
        }
    }
}