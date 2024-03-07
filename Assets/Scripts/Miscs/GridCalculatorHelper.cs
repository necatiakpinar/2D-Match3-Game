using System.Collections.Generic;
using NecatiAkpinar.Mono;
using NecatiAkpinar.Miscs;
using UnityEngine;

namespace NecatiAkpinar.Miscs
{
    public static class GridCalculatorHelper
    {
        public static List<TileMono> GetVerticalTilesForRocket(TileMono rocketTile)
        {
            TileMono[,] _tiles = GameReferences.Instance.GridController.Tiles;
            TileMono matchedTile;
            List<TileMono> resultTiles = new List<TileMono>();
            
            int selectedTileX = rocketTile.Coordinates.x;
            int upTileIndex = rocketTile.Coordinates.y + 1;
            int downTileIndex = rocketTile.Coordinates.y - 1;
           

            //Do not add spawners?
            while (downTileIndex > -1 || upTileIndex < _tiles.GetLength(1) - 2)
            {
                if (downTileIndex > -1)
                {
                    if (_tiles[selectedTileX, downTileIndex])
                    {
                        matchedTile = _tiles[selectedTileX, downTileIndex];
                        resultTiles.Add(matchedTile);
                        downTileIndex--;
                    }
                }

                if (upTileIndex < _tiles.GetLength(1) - 2)
                {
                    if (_tiles[selectedTileX, upTileIndex])
                    {
                        matchedTile = _tiles[selectedTileX, upTileIndex];
                        resultTiles.Add(matchedTile);
                        upTileIndex++;
                    }
                }
            }

            return resultTiles;
        }
        
        public static List<TileMono> GetHorizontalTilesForRocket(TileMono rocketTile)
        {
            TileMono[,] _tiles = GameReferences.Instance.GridController.Tiles;
            TileMono foundTile;
            List<TileMono> resultTiles = new List<TileMono>();
            
            int selectedTileY = rocketTile.Coordinates.y;
            int leftTileIndex = rocketTile.Coordinates.x - 1;
            int rightTileIndex = rocketTile.Coordinates.x + 1;

            while (leftTileIndex > -1 || rightTileIndex < _tiles.GetLength(0))
            {
                if (leftTileIndex > -1)
                {
                    if (_tiles[leftTileIndex, selectedTileY])
                    {
                        foundTile = _tiles[leftTileIndex, selectedTileY];
                        resultTiles.Add(foundTile);
                        leftTileIndex--;
                    }
                }

                if (rightTileIndex < _tiles.GetLength(0))
                {
                    if (_tiles[rightTileIndex, selectedTileY])
                    {
                        foundTile = _tiles[rightTileIndex, selectedTileY];
                        resultTiles.Add(foundTile);
                        rightTileIndex++;
                    }
                }
            }

            return resultTiles;
        }

        public static List<TileMono> GetDroppableElementBottomTiles()
        {
            TileMono[,] _tiles = GameReferences.Instance.GridController.Tiles;
            List<TileMono> resultTiles = new List<TileMono>();
            TileMono droppableElement;
            for (int i = 0; i < _tiles.GetLength(0); i++)
            {
                droppableElement = _tiles[i, 0];
                if (droppableElement.TileElement && GameElementHelper.IsDroppableAnimal(droppableElement.TileElement.ElementType))
                    resultTiles.Add(droppableElement);
            }
            
            return resultTiles;
        }
    }
}