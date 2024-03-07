using System.Collections;
using NecatiAkpinar.Abstracts;
using NecatiAkpinar.Interfaces;
using NecatiAkpinar.Miscs;
using NecatiAkpinar.Managers;

namespace NecatiAkpinar.GFX
{
    public class GFXBalloonBlockerController : BaseGfxController, IGFXActivatable
    {
        private BaseTileElement _tileElement;
        public GFXBalloonBlockerController(BaseTileElement tileElement)
        {
            _tileElement = tileElement;
        }

        public IEnumerator Activate()
        {
            //GFXManager.Instance.PlaySFX(Constants.SFX_BalloonPopped);
            yield break;
        }
    }
}