using Assets.Scripts.GameplayObjects;
using Assets.Scripts.ScriptableObjects;
using Assets.Scripts.Utility;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class SceneManager : MonoBehaviour
    {
        private AssetReference _assetRef;
        private GameParameters _gameParams;
        private IAudioManager _audioManager;
        private IAnimationManager _animManager;
        private IObjectPool _objectPool;
        private IPinata _pinata;
        private IPrizeShelfContainer _shelfContainer;

        private void Awake()
        {
            _assetRef = Resources.Load<AssetReference>("AssetReference");
            _gameParams = Resources.Load<GameParameters>("GameParams");
        }

        private void Start()
        {
            GeneralData.InitValues();
            _assetRef.Init();
            _audioManager = new AudioManager();
            _animManager = new AnimationManager();
            _objectPool = new ObjectPool(_assetRef);
            InitPinataGame();
        }

        private void InitPinataGame()
        {
            PrizeShelf();
            CreatePinata();            
        }

        private void CreatePinata()
        {
            _pinata = Instantiate(_assetRef.PrefabTypes[ObjectTypes.Pinata]).GetComponent<IPinata>();
            _pinata.Init(_audioManager, _animManager, _objectPool, _shelfContainer, _assetRef, _gameParams);

        }

        private void PrizeShelf()
        {
            var shelfContainer = Instantiate(_assetRef.PrefabTypes[ObjectTypes.PrizeShelfContainer]);
            shelfContainer.transform.position = new Vector3(0, _gameParams.ShelfContainerHeightPos, 0);
            _shelfContainer = shelfContainer.GetComponent<IPrizeShelfContainer>();
            _shelfContainer.Init(_assetRef);
        }
    }
}