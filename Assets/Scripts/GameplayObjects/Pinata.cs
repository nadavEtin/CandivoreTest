using Assets.Scripts.GameplayObjects.GameplayObjSubclasses;
using Assets.Scripts.GameplayObjects.GameplayObjUtility;
using Assets.Scripts.Managers;
using Assets.Scripts.ScriptableObjects;
using Assets.Scripts.Utility;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.GameplayObjects
{
    public class Pinata : MonoBehaviour, IPinata
    {
        [SerializeField] private GameObject _idleSprite, _idleCrackedSprite, _hitSprite;
        [SerializeField] private GameObject _headSprite, _frontSprtie, _tailSprite;
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
        private List<ObjectTypes> _pinataPrizes;
        private IPinataHitPoints _hitPoints;
        private IPrizeShelfContainer _prizeShelfContainer;

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
        }

        private void Start()
        {
            _childColliders = new List<IColliderScript> { _idleSprite.GetComponent<IColliderScript>(), _idleCrackedSprite.GetComponent<IColliderScript>() };
            _childObjects = new List<GameObject> { _idleSprite, _idleCrackedSprite, _hitSprite, _headSprite, _frontSprtie, _tailSprite };
            for (int i = 0; i < _childColliders.Count; i++)
            {
                _childColliders[i].init(PinataClick);
            }
            EnableDisableChildObjects(false);
            _idleSprite.SetActive(true);
            Instantiate(_assetReference.PrefabTypes[ObjectTypes.PinataRope]).GetComponent<IRope>().Init(_ropeAnchorPoint);
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

        private void PinataClick(float clickDuration)
        {
            if (_movementAnimPlaying)
                return;

            int clickPower;
            var stillAlive = _hitPoints.PinataClick(clickDuration, out clickPower);
            var clickPrizes = _prizeManager.GetPinataPrizes(clickPower, stillAlive);
            _audioManager.PinataClick(clickPower == _gameParams.ShortClickPower);
            PrepareClickPrizes(clickPrizes);
            //TODO: UNCOMMENT THIS
            /*if (stillAlive)*/
                ClickAnimationAndParticles();
        }

        private void PrepareClickPrizes(List<ObjectTypes> prizes)
        {
            var orderedPrizes = prizes.OrderByDescending(x => (int)x).ToList();
            var count = prizes.Where(x => x == orderedPrizes[0]).Count();
            SendPrize(orderedPrizes[0], count);
            orderedPrizes.RemoveRange(0, count);
        }

        private void SendPrize(ObjectTypes type, int amount)
        {
            var prize = _prizeShelfContainer.ReceivePrize(type, amount);
            var prizeParticle = SpawnParticle(type);
            _animationManager.MoveParticles(prizeParticle.transform, prize.shelfPos.position, _animationManager.FadeIn,
                prize.prizeObj.GetComponent<SpriteRenderer>());
            //create particle fx for the prize and send it to its pos on the shelf via animation
        }

        private GameObject SpawnParticle(ObjectTypes type)
        {
            var particle = _objectPool.GetObjectFromPool(_animationManager.PinataPrizeParticles[type]);
            particle.GetComponent<IParticleScript>().Init(_objectPool.AddObjectToPool, type);
            particle.transform.position = _prizeSpawnPoint.position;
            particle.GetComponent<ParticleSystem>().Play();
            return particle;
        }

        private void ClickAnimationAndParticles()
        {
            /*var confettiFx = _objectPool.GetObjectFromPool(ObjectTypes.ConfettiParticle);
            confettiFx.GetComponent<IParticleScript>().Init(_objectPool.AddObjectToPool, ObjectTypes.ConfettiParticle);
            confettiFx.transform.position = _prizeSpawnPoint.position;
            confettiFx.GetComponent<ParticleSystem>().Play();*/
            SpawnParticle(ObjectTypes.ConfettiParticle);
            _movementAnimPlaying = true;
            EnableDisableChildObjects(false);
            _hitSprite.SetActive(true);
            _animationManager.PinataHitMovement(gameObject, _hitSprite, _idleCrackedSprite, _pinataStartingPos, PinataMovementAnimEnd);
        }

        private void PinataMovementAnimEnd()
        {
            _movementAnimPlaying = false;
        }
    }
}