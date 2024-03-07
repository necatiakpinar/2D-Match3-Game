using System;
using System.Collections.Generic;
using System.Linq;
using NecatiAkpinar.Abstracts;
using NecatiAkpinar.Enums;
using UnityEngine;

namespace NecatiAkpinar.Data
{
    [System.Serializable]
    public class GameVFX
    {
        [SerializeField] private string key;
        [SerializeField] private BaseVFXMono _vfxObject;
        [SerializeField] private bool _isPoolObject;
        [SerializeField] private int _poolSize;

        public string Key => key;
        public BaseVFXMono VfxObject => _vfxObject;
        public bool IsPoolObject => _isPoolObject;
        public int PoolSize => _poolSize;
    }

    [CreateAssetMenu(fileName = "VFXContainer", menuName = "ScriptableObjects/Containers/VFXContainer", order = 9)]
    public class VFXDataContainer : ScriptableObject
    {
        [SerializeField] private List<GameVFX> _match2VFXes;

        public List<GameVFX> Match2VFXex => _match2VFXes;
    }
}