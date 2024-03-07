using UnityEngine;

namespace NecatiAkpinar.Data
{
    [CreateAssetMenu(fileName = "TileMonoData", menuName = "ScriptableObjects/TileMonoData", order = 1)]
    public class TileMonoData : ScriptableObject
    {
        [SerializeField] private float _tileElementDropSpeed = 0.05f;

        public float TileElementDropSpeed => _tileElementDropSpeed;
    }
}