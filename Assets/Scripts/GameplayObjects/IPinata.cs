using Assets.Scripts.Managers;
using Assets.Scripts.ScriptableObjects;
using Assets.Scripts.Utility;

namespace Assets.Scripts.GameplayObjects
{
    public interface IPinata
    {
        void Init(IAudioManager audioManager, IAnimationManager animationManager, IObjectPool objectPool, IPrizeShelfContainer prizeShelf, AssetReference assetReference, GameParameters gameParameters);
    }
}

