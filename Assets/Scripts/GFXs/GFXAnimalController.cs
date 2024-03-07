using System.Collections;
using NecatiAkpinar.Abstracts;
using NecatiAkpinar.Interfaces;
using NecatiAkpinar.Managers;
using NecatiAkpinar.Miscs;

namespace NecatiAkpinar.GFX
{
    public class GFXAnimalController : BaseGfxController, IGFXCollectable 
    {
        private BaseTileElement _tileElement;
        public GFXAnimalController(BaseTileElement tileElement)
        {
            _tileElement = tileElement;
        }
        
        public IEnumerator Collect()
        {
            GFXManager.Instance.PlaySFX(Constants.SFX_DuckCollected);
            yield break;
        }
    }
}