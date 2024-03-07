using System.Collections;
using System.Collections.Generic;
using NecatiAkpinar.Miscs;
using UnityEngine;

namespace NecatiAkpinar.Miscs
{
    public class CameraAdjuster : MonoBehaviour
    {
        private Vector2 _gridSize;
        private float _cameraX, _cameraY;

        void Start()
        {
            _gridSize = GameReferences.Instance.LevelController.CurrentLevel.GridSize;
            _cameraX = (_gridSize.x / 2) - 0.5f;
            _cameraY = (_gridSize.y / 2) + 0.5f;
            transform.position = new Vector3(_cameraX, _cameraY, transform.position.z);
        }
    }    
}
