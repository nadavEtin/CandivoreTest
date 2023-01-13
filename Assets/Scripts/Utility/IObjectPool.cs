using UnityEngine;

namespace Assets.Scripts.Utility
{
    public enum ObjectTypes
    {
        HeartReward, BombReward
    }

    public interface IObjectPool
    {
        void AddObjectToPool(GameObject obj, ObjectTypes type);

        GameObject GetObjectFromPool(ObjectTypes type);
    }
}
