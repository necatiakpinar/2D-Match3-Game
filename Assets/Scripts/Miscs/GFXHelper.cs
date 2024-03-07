using System.Collections.Generic;
using NecatiAkpinar.Enums;

namespace NecatiAkpinar.Miscs
{
    public static class GFXHelper
    {
        public static Dictionary<GameElementType, string> _elementActivationSFXes = new()
        {
            { GameElementType.YellowCube, Constants.SFX_CubeExplode },
            { GameElementType.GreenCube, Constants.SFX_CubeExplode },
            { GameElementType.BlueCube, Constants.SFX_CubeExplode },
            { GameElementType.RedCube, Constants.SFX_CubeExplode },
            { GameElementType.PurpleCube, Constants.SFX_CubeExplode },
            { GameElementType.BalloonBlocker, Constants.SFX_BalloonPopped },
            { GameElementType.Duck, Constants.SFX_DuckCollected },
        };

        public static Dictionary<GameElementType, string> _elementCollectionSFXes = new()
        {
            { GameElementType.YellowCube, Constants.SFX_CubeCollected },
            { GameElementType.GreenCube, Constants.SFX_CubeCollected },
            { GameElementType.BlueCube, Constants.SFX_CubeCollected },
            { GameElementType.RedCube, Constants.SFX_CubeCollected },
            { GameElementType.PurpleCube, Constants.SFX_CubeCollected },
            { GameElementType.Duck, Constants.SFX_DuckCollected }
        };
        
        public static Dictionary<GameElementType, string> _elementCollectionVFXes = new()
        {
            { GameElementType.YellowCube, Constants.VFX_CubeParticles },
            { GameElementType.GreenCube, Constants.VFX_CubeParticles },
            { GameElementType.BlueCube, Constants.VFX_CubeParticles },
            { GameElementType.RedCube, Constants.VFX_CubeParticles },
            { GameElementType.PurpleCube, Constants.VFX_CubeParticles }
        };

        public static string GetActivatableElementSFXKey(GameElementType elementType)
        {
            bool isKeyExist = _elementActivationSFXes.TryGetValue(elementType, out var key);
            if (isKeyExist)
                return _elementActivationSFXes[elementType];

            return null;
        }

        public static string GetCollectableElementSFXKey(GameElementType elementType)
        {
            bool isKeyExist = _elementCollectionSFXes.TryGetValue(elementType, out var key);
            if (isKeyExist)
                return _elementCollectionSFXes[elementType];

            return null;
        }
        
        public static string GetActivatableElementActivateVFXKey(GameElementType elementType)
        {
            bool isKeyExist = _elementCollectionVFXes.TryGetValue(elementType, out var key);
            if (isKeyExist)
                return _elementCollectionVFXes[elementType];

            return null;
        }
    }
}