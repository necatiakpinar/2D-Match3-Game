using System.Collections;
using NecatiAkpinar.Abstracts;
using NecatiAkpinar.Interfaces;
using NecatiAkpinar.Managers;
using NecatiAkpinar.Miscs;
using UnityEngine;

namespace NecatiAkpinar.GFX
{
    public class GFXColorStoneController : BaseGfxController, IGFXActivatable, IGFXCollectable
    {
        private BaseTileElement _tileElement;

        public GFXColorStoneController(BaseTileElement tileElement)
        {
            _tileElement = tileElement;
        }

        public IEnumerator Activate()
        {
            GFXManager.Instance.PlayVFX(GFXHelper.GetActivatableElementActivateVFXKey(_tileElement.ElementType), _tileElement, Quaternion.identity);

            yield break;
        }

        public IEnumerator Collect()
        {
            GFXManager.Instance.PlaySFX(GFXHelper.GetCollectableElementSFXKey(_tileElement.ElementType));
            yield break;
        }
    }
}