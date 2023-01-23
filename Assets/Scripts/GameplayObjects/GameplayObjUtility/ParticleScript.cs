using Assets.Scripts.ScriptableObjects;
using System;
using UnityEngine;

namespace Assets.Scripts.GameplayObjects.GameplayObjUtility
{
    [RequireComponent(typeof(ParticleSystem))]
    public class ParticleScript : MonoBehaviour, IParticleScript
    {
        public ParticleSystem ParticleSystem { get; private set; }
        private Action<GameObject, ObjectTypes> _endCb;
        private ObjectTypes _type;

        public void Init(Action<GameObject, ObjectTypes> endCb, ObjectTypes type)
        {
            _endCb = endCb;
            _type = type;
            ParticleSystem = GetComponent<ParticleSystem>();
            var mainModule = ParticleSystem.main;
            mainModule.stopAction = ParticleSystemStopAction.Callback;
        }

        private void OnParticleSystemStopped()
        {
            //Send the inactive particle system to the object pool for later reuse
            _endCb?.Invoke(gameObject, _type);
        }
    }
}
