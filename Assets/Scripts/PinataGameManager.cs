using System;
using Assets.Scripts.Utility;
using UnityEngine;

namespace DefaultNamespace
{
    public class PinataGameManager : MonoBehaviour
    {
        private AssetReferences _assetRef;
        
        private void Awake()
        {
            _assetRef = Resources.Load<AssetReferences>("AssetReference");
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
            
            var shelf1 = Instantiate(prizeShelf);
            var shelf2 = Instantiate(prizeShelf);
            shelf1.GetComponent<SpriteRenderer>().size = new Vector2(Camera.main.pixelWidth, 1);
        }
    }
}