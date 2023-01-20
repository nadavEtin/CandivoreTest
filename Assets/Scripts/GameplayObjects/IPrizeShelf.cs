using Assets.Scripts.ScriptableObjects;
using UnityEngine;

namespace Assets.Scripts.GameplayObjects
{
    public interface IPrizeShelf
    {
        GameObject gameObject { get; }

        void Init(AssetReference assetReference);
    }
}
