using System;
using System.Collections.Generic;
using System.Linq;
using NecatiAkpinar.Abstracts;
using NecatiAkpinar.Data;
using NecatiAkpinar.VFX;
using UnityEngine;

namespace NecatiAkpinar.Managers
{
    public class GFXManager : MonoBehaviour
    {
        [SerializeField] private GameObject _audioSourcePrefab;

        private SFXDataContainer _sfxDataContainer;
        private VFXDataContainer _vfxDataContainer;

        private Dictionary<string, AudioClip> _sfxDictionary;
        private Dictionary<string, BaseVFXMono> _vfxDictionary;

        private Queue<AudioSource> _audioSources = new Queue<AudioSource>();
        private readonly int _sfxPoolSize = 5;
        private readonly int _vfxPoolSize = 30;

        private Dictionary<string, Queue<BaseVFXMono>> _vfxPool = new Dictionary<string, Queue<BaseVFXMono>>();


        public static GFXManager Instance;

        private void Awake()
        {
            if (Instance != null && Instance != this)
                Destroy(this);
            else
                Instance = this;
        }

        private void Start()
        {
            _sfxDataContainer = DataManager.Instance.SFXDataContainer;
            _vfxDataContainer = DataManager.Instance.VFXDataContainer;

            _sfxDictionary = _sfxDataContainer.Match2SFXes.ToDictionary(sfx => sfx.Name, sfx => sfx.Clip);
            _vfxDictionary = _vfxDataContainer.Match2VFXex.ToDictionary(vfx => vfx.Key, vfx => vfx.VfxObject);

            CreateGFXPools();
        }

        private void CreateGFXPools()
        {
            //SFX
            AudioSource source;
            for (int i = 0; i < _sfxPoolSize; i++)
            {
                source = Instantiate(_audioSourcePrefab).GetComponent<AudioSource>();
                source.gameObject.SetActive(false);
                _audioSources.Enqueue(source);
            }

            //VFX
            foreach (GameVFX vfxData in _vfxDataContainer.Match2VFXex)
            {
                if (!vfxData.IsPoolObject)
                    continue;

                var queue = new Queue<BaseVFXMono>();
                for (int i = 0; i < vfxData.PoolSize; i++)
                {
                    var vfx = Instantiate(vfxData.VfxObject);
                    vfx.Init(vfxData.Key);
                    queue.Enqueue(vfx);
                }

                _vfxPool.Add(vfxData.Key, queue);
            }
        }

        public GFXManager PlaySFX(string audioKey, float volume = 1)
        {
            if (_audioSources.Count == 0) return this;

            AudioSource source;
            _sfxDictionary.TryGetValue(audioKey, out var clip);
            if (clip)
            {
                source = _audioSources.Dequeue();
                source.clip = clip;
                source.volume = volume;
                source.gameObject.SetActive(true);
                source.Play();
                StartCoroutine(ReturnToPool(source));
            }

            return this;
        }

        private IEnumerator<YieldInstruction> ReturnToPool(AudioSource source)
        {
            yield return new WaitForSeconds(source.clip.length);
            source.gameObject.SetActive(false);
            _audioSources.Enqueue(source);
        }

        public GFXManager PlayVFX(string name, BaseTileElement tileElement, Quaternion vfxRotation)
        {
            if (_vfxPool.TryGetValue(name, out var queue))
            {
                BaseVFXMono vfx;
                if (queue.Count > 0)
                {
                    vfx = queue.Dequeue();
                    StartCoroutine(vfx.Play(tileElement));
                }
                else
                {
                    _vfxDictionary.TryGetValue(name, out var vfxPrefab);
                    vfx = Instantiate(vfxPrefab);
                    StartCoroutine(vfx.Play(tileElement));
                }

                vfx.transform.position = tileElement.transform.position;
                vfx.transform.rotation = vfxRotation;
            }

            return this;
        }

        public GFXManager PlayVFX(string name, Vector3 vfxPosition, Quaternion vfxRotation)
        {
            if (_vfxPool.TryGetValue(name, out var queue))
            {
                BaseVFXMono vfx;
                if (queue.Count > 0)
                {
                    vfx = queue.Dequeue();
                    StartCoroutine(vfx.Play());
                }
                else
                {
                    _vfxDictionary.TryGetValue(name, out var vfxPrefab);
                    vfx = Instantiate(vfxPrefab);
                    StartCoroutine(vfx.Play());
                }

                vfx.transform.position = vfxPosition;
                vfx.transform.rotation = vfxRotation;
            }

            return this;
        }

        public void VFXReturnToPool(string key, BaseVFXMono vfx)
        {
            _vfxPool[key].Enqueue(vfx);
        }
    }
}