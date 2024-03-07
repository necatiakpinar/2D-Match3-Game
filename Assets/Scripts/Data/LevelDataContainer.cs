using System.Collections.Generic;
using UnityEngine;

namespace NecatiAkpinar.Data
{
    [CreateAssetMenu(fileName = "LevelContainer", menuName = "ScriptableObjects/Containers/LevelContainer", order = 7)]
    public class LevelDataContainer : ScriptableObject
    {
        [SerializeField] private List<LevelData> _allLevels;
        public List<LevelData> AllLevels => _allLevels;
        
    }
}