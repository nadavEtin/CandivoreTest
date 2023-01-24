using Assets.Scripts.GameplayObjects;
using Assets.Scripts.ScriptableObjects;
using Assets.Scripts.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Managers
{
    public class SceneManager : MonoBehaviour
    {
        [SerializeField] private Button _restartBtn;

        private AssetReference _assetRef;
        private GameParameters _gameParams;
        private IAudioManager _audioManager;
        private IAnimationManager _animationManager;
        private IObjectPool _objectPool;
        private IPinata _pinata;
        private IPrizeShelfContainer _shelfContainer;
        private GameObject _shelfContainerObj;

        private void Awake()
        {
            _assetRef = Resources.Load<AssetReference>("AssetReference");
            _gameParams = Resources.Load<GameParameters>("GameParams");
            EventBus.Subscribe(GameplayEvent.GameStart, GameStart);
            EventBus.Subscribe(GameplayEvent.GameEnd, GameEnd);
            GeneralData.InitValues();
        }

        private void Start()
        {
            _assetRef.Init();
            _audioManager = new AudioManager();
            _animationManager = new AnimationManager();
            _objectPool = new ObjectPool(_assetRef);
            InitPinataGame();
            EventBus.Publish(GameplayEvent.GameStart, BaseEventParams.Empty);
        }

        private void InitPinataGame()
        {
            CreatePrizeShelves();
            CreatePinata();
        }

        private void CreatePinata()
        {
            _pinata = Instantiate(_assetRef.PrefabTypes[ObjectTypes.Pinata]).GetComponent<IPinata>();
            _pinata.Init(_audioManager, _animationManager, _objectPool, _shelfContainer, _assetRef, _gameParams);
        }

        private void CreatePrizeShelves()
        {
            _shelfContainerObj = Instantiate(_assetRef.PrefabTypes[ObjectTypes.PrizeShelfContainer]);
            _shelfContainerObj.transform.position = new Vector3(0, _gameParams.ShelfContainerHeightPos, 0);
            _shelfContainer = _shelfContainerObj.GetComponent<IPrizeShelfContainer>();
            _shelfContainer.Init(_assetRef, _animationManager, _objectPool);
        }

        private void GameStart(BaseEventParams eventParams)
        {
            _audioManager.PlaySound(AudioTypes.PinataIntro);
        }

        private void GameEnd(BaseEventParams eventParams)
        {
            StartCoroutine(GameEndActions());
        }

        private void PlayFireworks()
        {
            var fireworksList = Instantiate(_assetRef.PrefabTypes[ObjectTypes.Fireworks]).GetComponent<FireworksScript>().Effects;
            StartCoroutine(Fireworks(fireworksList));
            
        }

        private void OnDestroy()
        {
            EventBus.Unsubscribe(GameplayEvent.GameStart, GameStart);
            EventBus.Unsubscribe(GameplayEvent.GameEnd, GameEnd);
        }

        IEnumerator GameEndActions()
        {
            //Wait for pinata movement animations to finish
            yield return new WaitForSeconds(1.5f);
            var endShelfContainerPos = new Vector3(0, _gameParams.GameEndShelfContainerPos, 0);
            _animationManager.MoveTransform(_shelfContainerObj.transform, endShelfContainerPos, 0.5f);
            yield return new WaitForSeconds(0.5f);
            _animationManager.FadeIn(_restartBtn.image, 0.5f);
            _shelfContainer.PlayGameEndAnimations();
            PlayFireworks();
        }

        IEnumerator Fireworks(List<ParticleSystem> fireworks)
        {
            yield return new WaitForSeconds(5);
            for (int i = 0; i < fireworks.Count; i++)
            {
                fireworks[i].gameObject.SetActive(true);
                fireworks[i].Play();
                yield return new WaitForSeconds(0.5f);
            }
        }
    }
}