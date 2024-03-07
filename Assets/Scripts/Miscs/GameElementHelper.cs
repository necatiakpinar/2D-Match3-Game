using System;
using System.Collections.Generic;
using System.Linq;
using NecatiAkpinar.Enums;

namespace NecatiAkpinar.Miscs
{
    public static class GameElementHelper
    {
        //The below collections are for the caching.
        private static GameElementType[] _colorCubes;
        private static GameElementType[] _powerUps;
        private static GameElementType[] _blockers;
        private static GameElementType[] _tiles;
        private static GameElementType[] _animals;

        private static Dictionary<ItemFolderType, GameElementType[]> _folderTypeMap;

        static GameElementHelper()
        {
            InitCache();
        }

        private static GameElementType[] SubTypeToGameElementTypeArray(Type enumType)
        {
            GameElementType[] types = Enum.GetValues(enumType).Cast<GameElementType>().ToArray();
            return types;
        }

        private static void InitCache()
        {
            _colorCubes = SubTypeToGameElementTypeArray(typeof(ColorCubeType));
            _powerUps = SubTypeToGameElementTypeArray(typeof(PowerUpType));
            _blockers = SubTypeToGameElementTypeArray(typeof(BlockerType));
            _tiles = SubTypeToGameElementTypeArray(typeof(TileType));
            _animals = SubTypeToGameElementTypeArray(typeof(AnimalType));

            InitFolderTypeDictionary();
        }

        private static void InitFolderTypeDictionary()
        {
            _folderTypeMap = new Dictionary<ItemFolderType, GameElementType[]>();
            _folderTypeMap.Add(ItemFolderType.ColorCube, _colorCubes);
            _folderTypeMap.Add(ItemFolderType.PowerUp, _powerUps);
            _folderTypeMap.Add(ItemFolderType.Tile, _tiles);
            _folderTypeMap.Add(ItemFolderType.DroppableAnimal, _animals);

            //Blockers
            List<GameElementType> allBlockers = new List<GameElementType>();
            allBlockers.AddRange(_blockers);
            _folderTypeMap.Add(ItemFolderType.Blocker, allBlockers.ToArray());
        }

        public static bool IsColorCube(GameElementType elementType) => _colorCubes.Contains(elementType);
        public static bool IsPowerUp(GameElementType elementType) => _powerUps.Contains(elementType);
        public static bool IsDroppableAnimal(GameElementType elementType) => _animals.Contains(elementType);
        public static bool IsPlayableItem(GameElementType elementType) => IsColorCube(elementType) || IsPowerUp(elementType) || IsDroppableAnimal(elementType);

        public static bool IsBlocker(GameElementType elementType)
        {
            GameElementType[] blockers = _folderTypeMap[ItemFolderType.Blocker];
            return blockers.Contains(elementType);
        }
    }
}