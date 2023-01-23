using Assets.Scripts.GameplayObjects.GameplayObjSubclasses;
using Assets.Scripts.GameplayObjects.GameplayObjUtility;
using Assets.Scripts.Managers;
using Assets.Scripts.ScriptableObjects;
using Assets.Scripts.Utility;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.GameplayObjects
{
    public class Pinata : MonoBehaviour, IPinata
    {
        [SerializeField] private GameObject _idleSprite, _idleCrackedSprite, _hitSprite;
        [SerializeField] private GameObject _headSprite, _bodySprtie, _tailSprite;
        [SerializeField] private Transform _ropeAnchorPoint, _prizeSpawnPoint;

        private IAudioManager _audioManager;
        private IAnimationManager _animationManager;
        private IObjectPool _objectPool;
        private IPinataPrizeGenerator _prizeManager;
        private AssetReference _assetReference;
        private GameParameters _gameParams;

        private List<IColliderScript> _childColliders;
        private List<GameObject> _childObjects;
        private Vector3 _pinataStartingPos;
        private bool _movementAnimPlaying;
        private IPinataHitPoints _hitPoints;
        private IPrizeShelfContainer _prizeShelfContainer;
        private Transform _particleContainer;

        private void Awake()
        {
            EventBus.Subscribe(GameplayEvent.GameEnd, GameEnd);
            EventBus.Subscribe(GameplayEvent.GameStart, GameStart);
        }

        public void Init(IAudioManager audioManager, IAnimationManager animationManager,
            IObjectPool objectPool, IPrizeShelfContainer prizeShelf, AssetReference assetReference, GameParameters gameParameters)
        {
            _audioManager = audioManager;
            _animationManager = animationManager;
            _assetReference = assetReference;
            _gameParams = gameParameters;
            _objectPool = objectPool;
            _prizeShelfContainer = prizeShelf;

            _prizeManager = new PinataPrizeGenerator(assetReference, gameParameters);
            _hitPoints = new PinataHitPoints(_gameParams);
            _particleContainer = new GameObject("ParticleFxContainer").transform;            
            AdditionalSetup();
        }

        private void AdditionalSetup()
        {
            _childColliders = new List<IColliderScript> { _idleSprite.GetComponent<IColliderScript>(), _idleCrackedSprite.GetComponent<IColliderScript>() };
            _childObjects = new List<GameObject> { _idleSprite, _idleCrackedSprite, _hitSprite, _headSprite, _bodySprtie, _tailSprite };
            for (int i = 0; i < _childColliders.Count; i++)
            {
                _childColliders[i].init(PinataClick);
            }
            EnableDisableChildObjects(false);
            _idleSprite.SetActive(true);
            Instantiate(_assetReference.PrefabTypes[ObjectTypes.PinataRope]).GetComponent<IRope>().Init(_ropeAnchorPoint, _animationManager);
            PositionPinata();
        }

        private void PositionPinata()
        {
            _pinataStartingPos = new Vector3(0, 0 + GeneralData.HalfScreenHeight * _gameParams.IdlePinataScreenHeight, 0);
            transform.position = new Vector3(0, GeneralData.HalfScreenHeight * 2f, 0);
        }

        private void GameStart(BaseEventParams eventParams)
        {
            //Lowers the pinata into view
            _animationManager.PinataIntro(transform, _pinataStartingPos);
        }

        private void EnableDisableChildObjects(bool enable)
        {
            for (int i = 0; i < _childObjects.Count; i++)
            {
                _childObjects[i].SetActive(enable);
            }
        }

        private void GameEnd(BaseEventParams eventParams)
        {
            EnableDisableChildObjects(false);
            _headSprite.SetActive(true);
            _bodySprtie.SetActive(true);
            _tailSprite.SetActive(true);
            _audioManager.PlaySound(AudioTypes.PinataFinalSmashSound);
            _audioManager.PlaySound(AudioTypes.PinataFallDownSound);
            SpawnParticle(ObjectTypes.BigConfettiParticle);
            _animationManager.PinataExplosion(_headSprite.transform, _bodySprtie.transform, _tailSprite.transform);
        }

        private void PinataClick(float clickDuration)
        {
            //Prevent pinata hits until the animations are done
            if (_movementAnimPlaying)
                return;

            var stillAlive = _hitPoints.PinataClick(clickDuration, out int clickPower);
            if(stillAlive == false)
                _prizeSpawnPoint.localPosition = new Vector3(_prizeSpawnPoint.localPosition.x, _prizeSpawnPoint.localPosition.y + 0.8f, 0);            

            var clickPrizes = _prizeManager.GetPinataPrizes(clickPower, stillAlive);
            _audioManager.PinataClick(clickPower == _gameParams.ShortClickPower);
            PrepareClickPrizes(clickPrizes);

            if (stillAlive)
                ClickAnimationAndParticles();
            else
                EventBus.Publish(GameplayEvent.GameEnd, BaseEventParams.Empty);
        }

        private void PrepareClickPrizes(List<ObjectTypes> prizes)
        {
            //Sort the prizes by type and send them one by one
            var orderedPrizes = prizes.OrderByDescending(x => (int)x).ToList();
            while (orderedPrizes.Count > 0)
            {
                var count = prizes.Where(x => x == orderedPrizes[0]).Count();
                SendPrize(orderedPrizes[0], count);
                orderedPrizes.RemoveRange(0, count);
            }
        }

        //Pass the necessary data to the shelf container
        private void SendPrize(ObjectTypes type, int amount)
        {
            var prizeParticle = SpawnParticle(type);
            _prizeShelfContainer.ReceivePrize(type, amount, prizeParticle);
        }

        private GameObject SpawnParticle(ObjectTypes type)
        {
            var particle = _objectPool.GetObjectFromPool(_animationManager.GetPrizeParticleType(type));
            particle.GetComponent<IParticleScript>().Init(_objectPool.AddObjectToPool, _animationManager.GetPrizeParticleType(type));
            particle.transform.position = _prizeSpawnPoint.position;
            particle.GetComponent<ParticleSystem>().Play();
            particle.transform.SetParent(_particleContainer, true);
            return particle;
        }

        //Pinata movement animation and confetti particles
        private void ClickAnimationAndParticles()
        {
            SpawnParticle(ObjectTypes.ConfettiParticle);
            _movementAnimPlaying = true;
            EnableDisableChildObjects(false);
            _hitSprite.SetActive(true);
            _animationManager.PinataHitMovement(gameObject, _hitSprite, _idleCrackedSprite, _pinataStartingPos, PinataMovementAnimEnd);
        }

        private void PinataMovementAnimEnd()
        {
            StartCoroutine(AnimationDelay());
        }

        //Small delay to let prize and particle animations finish
        IEnumerator AnimationDelay()
        {
            yield return new WaitForSeconds(0.5f);
            _movementAnimPlaying = false;
        }

        private void OnDestroy()
        {
            EventBus.Unsubscribe(GameplayEvent.GameStart, GameStart);
            EventBus.Unsubscribe(GameplayEvent.GameEnd, GameEnd);
        }
    }
}