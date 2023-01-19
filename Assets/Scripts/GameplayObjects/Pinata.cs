using Assets.Scripts;
using Assets.Scripts.GameplayObjects.GameplayObjUtility;
using Assets.Scripts.Managers;
using Assets.Scripts.ScriptableObjects;
using Assets.Scripts.Utility;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.GameplayObjects
{
    public class Pinata : MonoBehaviour, IPinata
    {
        [SerializeField] private GameObject _idle, _idleCracked, _hit;
        [SerializeField] private GameObject _head, _front, _tail;
        [SerializeField] private Transform _ropeAnchorPoint, _prizeSpawnPoint;

        private IAudioManager _audioManager;
        private IAnimationManager _animationManager;
        private IObjectPool _objectPool;
        private IPinataPrizeManager _prizeManager;
        private AssetReference _assetReference;
        private GameParameters _gameParams;
        
        private List<IColliderScript> _childColliders;
        private List<GameObject> _childObjects;
        private Vector3 _pinataStartingPos;
        private bool _movementAnimPlaying;
        private List<PrizeData> _pinataPrizes;

        public void Init(IAudioManager audioManager, IAnimationManager animationManager, 
            IObjectPool objectPool, AssetReference assetReference, GameParameters gameParameters)
        {
            _audioManager = audioManager;
            _animationManager = animationManager;
            _assetReference = assetReference;
            _gameParams = gameParameters;
            _objectPool = objectPool;
            
            _prizeManager = new PinataPrizeManager(assetReference, gameParameters);
            _pinataPrizes = _prizeManager.GetPinataPrizes();
        }

        private void Start()
        {
            _childColliders = new List<IColliderScript> { _idle.GetComponent<IColliderScript>(), _idleCracked.GetComponent<IColliderScript>() };
            _childObjects = new List<GameObject> { _idle, _idleCracked, _hit, _head, _front, _tail };
            for (int i = 0; i < _childColliders.Count; i++)
            {
                _childColliders[i].init(PinataClick);
            }
            EnableDisableChildObjects(false);
            _idle.SetActive(true);
            Instantiate(_assetReference.PinataRope).GetComponent<IRope>().Init(_ropeAnchorPoint);
            PositionPinata();
        }

        private void PositionPinata()
        {
            transform.position = new Vector3(0, 0 + GeneralData.HalfScreenHeight * _gameParams.IdlePinataScreenHeight, 0);
            _pinataStartingPos = transform.position;
        }

        private void EnableDisableChildObjects(bool enable)
        {
            for (int i = 0; i < _childObjects.Count; i++)
            {
                _childObjects[i].SetActive(enable);
            }
        }

        private void PinataClick()
        {
            if (_movementAnimPlaying)
                return;
            var particleFx = _objectPool.GetObjectFromPool(ObjectTypes.ConfettiParticle);
            particleFx.GetComponent<IParticleScript>().Init(_objectPool.AddObjectToPool, ObjectTypes.ConfettiParticle);
            particleFx.transform.position = _prizeSpawnPoint.position;
            particleFx.GetComponent<ParticleSystem>().Play();
            _movementAnimPlaying = true;
            EnableDisableChildObjects(false);
            _hit.SetActive(true);

            _audioManager.PlaySound(AudioTypes.PinataSmallHitSound);
            _animationManager.PinataHitMovement(gameObject, _hit, _idleCracked, _pinataStartingPos, PinataMovementAnimEnd);
        }

        private void PinataMovementAnimEnd()
        {
            _movementAnimPlaying = false;
        }
    }
}