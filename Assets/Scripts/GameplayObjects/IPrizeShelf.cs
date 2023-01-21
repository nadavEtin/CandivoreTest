using Assets.Scripts.ScriptableObjects;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.GameplayObjects
{
    public interface IPrizeShelf
    {
        GameObject gameObject { get; }

        List<ShelfPrize> _prizes { get; }

        void Init(AssetReference assetReference);

        ShelfPrize AddPrize(ObjectTypes prizeType, int amount);
    }
}
