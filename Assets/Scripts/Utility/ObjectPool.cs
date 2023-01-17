using System;
using System.Collections.Generic;
using Assets.Scripts.ScriptableObjects;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets.Scripts.Utility
{
    public class ObjectPool : IObjectPool
    {
        private readonly AssetReference _assetRefs;
        private Dictionary<ObjectTypes, List<GameObject>> _objectPool;

        public ObjectPool(AssetReference assetRefs)
        {
            _assetRefs = assetRefs;
            _objectPool = new Dictionary<ObjectTypes, List<GameObject>>();
        }

        public void AddObjectToPool(GameObject obj, ObjectTypes type)
        {
            if(_objectPool.ContainsKey(type) == false)
                _objectPool.Add(type, new List<GameObject>());
            
            _objectPool[type].Add(obj);
        }

        public GameObject GetObjectFromPool(ObjectTypes type)
        {
            if (_objectPool.ContainsKey(type) && _objectPool[type].Count > 0)
            {
                var returnObj = _objectPool[type][0];
                _objectPool[type].RemoveAt(0);
                return returnObj;
            }
            else if (_assetRefs.PrefabTypes.ContainsKey(type))
            {
                return CreateObject(type);
            }
            else
            {
                Debug.LogError("incorrect prefab type: " + Enum.GetName(typeof(ObjectTypes), type));
                return null;
            }
        }

        private GameObject CreateObject(ObjectTypes type)
        {
            return Object.Instantiate(_assetRefs.PrefabTypes[type]);
        }
    }
}
