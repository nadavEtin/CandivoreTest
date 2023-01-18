using Assets.Scripts.GameplayObjects;
using Assets.Scripts.ScriptableObjects;
using Assets.Scripts.Utility;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class PinataGameManager : MonoBehaviour
    {
        private AssetReference _assetRef;
        private GameParameters _gameParams;
        private IAudioManager _audioManager;
        private IAnimationManager _animManager;
        private IObjectPool _objectPool;
        private IPinata _pinata;
        private List<IPrizeShelf> _prizeShelves;

        private void Awake()
        {
            _assetRef = Resources.Load<AssetReference>("AssetReference");
            _gameParams = Resources.Load<GameParameters>("GameParams");
        }

        private void Start()
        {
            _prizeShelves = new List<IPrizeShelf>();
            GeneralData.InitValues();
            _assetRef.Init();
            _audioManager = new AudioManager();
            _animManager = new AnimationManager();
            _objectPool = new ObjectPool(_assetRef);
            InitPinataGame();
        }

        private void InitPinataGame()
        {
            CreatePinata();
            PrizeShelf();
        }

        private void CreatePinata()
        {
            _pinata = Instantiate(_assetRef.Pinata).GetComponent<IPinata>();
            _pinata.Init(_audioManager, _animManager, _objectPool, _assetRef, _gameParams);

        }

        private void PrizeShelf()
        {
            var prizeShelf = _assetRef.PrizeShelf;
            if (prizeShelf == null)
                return;

            for (int i = 0; i < _gameParams.InitialShelfCount && i < _gameParams.PrizeShelfHeight.Count; i++)
            {
                var newShelf = Instantiate(prizeShelf).GetComponent<IPrizeShelf>();
                newShelf.gameObject.transform.position = new Vector3(0, 0 - GeneralData.HalfScreenHeight * _gameParams.PrizeShelfHeight[i], 0);
                newShelf.Init(_gameParams.PrizeShelfMaxCap);
                _prizeShelves.Add(newShelf);
                newShelf.gameObject.SetActive(false);
            }
        }
    }
}