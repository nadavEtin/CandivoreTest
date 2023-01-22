using Assets.Scripts.Managers;
using Assets.Scripts.ScriptableObjects;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.GameplayObjects
{
    public interface IPrizeShelf
    {
        GameObject gameObject { get; }

        List<ShelfPrize> _prizes { get; }

        void Init(AssetReference assetReference, IAnimationManager animationManager);

        ShelfPrize AddPrize(ObjectTypes prizeType, int amount, GameObject particleFx);
    }
}
