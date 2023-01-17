using UnityEngine;

namespace Assets.Scripts.GameplayObjects
{
    public interface IPrizeShelf
    {
        GameObject gameObject { get; }

        void Init(int maxRewardCap);
    }
}
