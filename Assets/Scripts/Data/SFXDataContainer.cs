using System;
using System.Collections.Generic;
using System.Linq;
using NecatiAkpinar.Enums;
using UnityEngine;

namespace NecatiAkpinar.Data
{
    [System.Serializable]
    public class GameSFX
    {
        [SerializeField] private string key;
        [SerializeField] private AudioClip _clip;
        public string Name => key;
        public AudioClip Clip => _clip;
    }

    [CreateAssetMenu(fileName = "SFXContainer", menuName = "ScriptableObjects/Containers/SFXContainer", order = 8)]
    public class SFXDataContainer : ScriptableObject
    {
        [SerializeField] private List<GameSFX> _match2SFXes;

        public List<GameSFX> Match2SFXes => _match2SFXes;
    }
}