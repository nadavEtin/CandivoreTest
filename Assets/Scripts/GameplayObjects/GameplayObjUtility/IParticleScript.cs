using Assets.Scripts.ScriptableObjects;
using System;
using UnityEngine;

namespace Assets.Scripts.GameplayObjects.GameplayObjUtility
{
    internal interface IParticleScript
    {
        void Init(Action<GameObject, ObjectTypes> endCb, ObjectTypes type);
    }
}
