using Assets.Scripts.ScriptableObjects;
using UnityEngine;

namespace Assets.Scripts.GameplayObjects
{
    public interface IPrizeShelfContainer
    {
        ShelfPrize ReceivePrize(ObjectTypes prizeType, int amount);

        void Init(AssetReference assetReference);
    }
}
