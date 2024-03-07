using System;
using System.Collections;
using System.Collections.Generic;
using NecatiAkpinar.Abstracts;
using NecatiAkpinar.Enums;
using NecatiAkpinar.Interfaces;
using NecatiAkpinar.Managers;
using UnityEngine;

namespace NecatiAkpinar.VFX
{
    public class CubeVFX : BaseVFXMono, IVFXPoolable
    {
        [SerializeField] private List<GameObject> _cubes;
        [SerializeField] private Vector2 _randomForceMin = new Vector2(-1.0f, 1.0f);
        [SerializeField] private Vector2 _randomForceMax = new Vector2(1.0f, 1.0f);
        [SerializeField] private float _lifeTimeDuration = 1.5f;

        private List<Vector2> _cubesStartingPosition = new List<Vector2>();
        private WaitForSeconds _vfxWaitForSeconds;

        private Dictionary<ColorCubeType, Color> _colors = new()
        {
            { ColorCubeType.BlueCube, Color.blue },
            { ColorCubeType.GreenCube, Color.green },
            { ColorCubeType.RedCube, Color.red },
            { ColorCubeType.PurpleCube, Color.magenta },
            { ColorCubeType.YellowCube, Color.yellow },
        };

        public override void Init(string vfxKey)
        {
            base.Init(vfxKey);

            Vector2 vfxStartingPosition;
            for (int i = 0; i < _cubes.Count; i++)
            {
                vfxStartingPosition = _cubes[i].transform.localPosition;
                _cubesStartingPosition.Add(vfxStartingPosition);
            }

            _vfxWaitForSeconds = new WaitForSeconds(_lifeTimeDuration);
        }

        public override IEnumerator Play(BaseTileElement colorCubeType)
        {
            yield return base.Play();

            gameObject.SetActive(true);
            SpriteRenderer spriteRenderer;
            Rigidbody2D rigidBody;
            Color color;
            for (int i = 0; i < _cubes.Count; i++)
            {
                spriteRenderer = _cubes[i].GetComponent<SpriteRenderer>();
                rigidBody = _cubes[i].GetComponent<Rigidbody2D>();

                if (_colors.TryGetValue((ColorCubeType)colorCubeType.ElementType, out color))
                {
                    spriteRenderer.color = color;
                }

                float randomXForce = UnityEngine.Random.Range(_randomForceMin.x, _randomForceMax.x);
                float randomYForce = UnityEngine.Random.Range(_randomForceMin.y, _randomForceMax.y);
                Vector2 randomForce = new Vector2(randomXForce, randomYForce);
                rigidBody.AddForce(randomForce, ForceMode2D.Impulse);
            }

            yield return _vfxWaitForSeconds;

            ReturnToPool();
        }

        public void Reset()
        {
            for (int i = 0; i < _cubes.Count; i++)
                _cubes[i].transform.localPosition = _cubesStartingPosition[i];

            gameObject.SetActive(false);
        }

        public void ReturnToPool()
        {
            Reset();
            GFXManager.Instance.VFXReturnToPool(_vfxKey, this);
        }
    }
}