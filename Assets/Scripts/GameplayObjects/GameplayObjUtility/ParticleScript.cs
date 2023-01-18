using Assets.Scripts.ScriptableObjects;
using System;
using UnityEngine;

namespace Assets.Scripts.GameplayObjects.GameplayObjUtility
{
    public class ParticleScript : MonoBehaviour, IParticleScript
    {
        private Action<GameObject, ObjectTypes> _endCb;
        private ObjectTypes _type;

        public void Init(Action<GameObject, ObjectTypes> endCb, ObjectTypes type)
        {
            _endCb = endCb;
            _type = type;
            var mainModule = GetComponent<ParticleSystem>().main;
            mainModule.stopAction = ParticleSystemStopAction.Callback;
        }

        private void OnParticleSystemStopped()
        {
            _endCb?.Invoke(gameObject, _type);
        }
    }
}
