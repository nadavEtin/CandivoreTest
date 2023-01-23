using Assets.Scripts.Managers;
using Assets.Scripts.ScriptableObjects;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.GameplayObjects
{
    public interface IPrizeShelf
    {
        GameObject gameObject { get; }

        List<ShelfPrizeData> _prizes { get; }

        void Init(AssetReference assetReference, IAnimationManager animationManager);

        ShelfPrizeData AddPrize(ObjectTypes prizeType, int amount, GameObject particleFx);
    }
}
