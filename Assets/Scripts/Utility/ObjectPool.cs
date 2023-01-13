using System;
using UnityEngine;

namespace Assets.Scripts.Utility
{
    public class ObjectPool : IObjectPool
    {
        public void AddObjectToPool(GameObject obj, ObjectTypes type)
        {
            
        }

        public GameObject GetObjectFromPool(ObjectTypes type)
        {
            throw new NotImplementedException();
        }
    }
}
