using System;
using System.Collections;
using NecatiAkpinar.Abstracts;
using NecatiAkpinar.Interfaces;
using NecatiAkpinar.Managers;
using UnityEngine;

namespace NecatiAkpinar.VFX
{
    public class BasicParticleVFX : BaseVFXMono, IVFXPoolable
    {
        [SerializeField] private ParticleSystem _particleSystem;
        private WaitForSeconds _waitForSeconds;

        public override void Init(string vfxKey)
        {
            base.Init(vfxKey);
            _waitForSeconds = new WaitForSeconds(_particleSystem.main.startLifetime.constant);
        }

        public override IEnumerator Play()
        {
            gameObject.SetActive(true);
            _particleSystem.Play();
            yield return _waitForSeconds;
            ReturnToPool();
        }

        public void Reset()
        {
            gameObject.SetActive(false);
        }

        public void ReturnToPool()
        {
            Reset();
            GFXManager.Instance.VFXReturnToPool(_vfxKey, this);
        }
    }
}