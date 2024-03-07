using NecatiAkpinar.Mono;
using UnityEngine;

namespace NecatiAkpinar.Data
{
    [CreateAssetMenu(fileName = "GridData", menuName = "ScriptableObjects/GridData", order = 1)]
    public class GridData : ScriptableObject
    {
        [Header("Tile Prefab")] 
        [SerializeField] private TileMono _tilePrefab;
        public TileMono TilePrefab => _tilePrefab;
    
    }
}