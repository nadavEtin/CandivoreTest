using Assets.Scripts.ScriptableObjects;
using UnityEngine;

namespace Assets.Scripts.Utility
{
    public interface IObjectPool
    {
        void AddObjectToPool(GameObject obj, ObjectTypes type);

        GameObject GetObjectFromPool(ObjectTypes type);
    }
}
