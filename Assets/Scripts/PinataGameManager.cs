using System;
using System.Collections.Generic;
using Assets.Scripts.GameplayObjects;
using Assets.Scripts.ScriptableObjects;
using Assets.Scripts.Utility;
using UnityEngine;

namespace DefaultNamespace
{
    public class PinataGameManager : MonoBehaviour
    {
        private AssetReferences _assetRef;
        private GameParameters _gameParams;
        private List<IPrizeShelf> _prizeShelves;
        
        private void Awake()
        {
            _assetRef = Resources.Load<AssetReferences>("AssetReference");
            _gameParams = Resources.Load<GameParameters>("GameParams");
        }

        private void Start()
        {
            GeneralData.InitValues();
            InitPinataGame();
        }

        private void InitPinataGame()
        {
            PrizeShelf();
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