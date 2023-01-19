using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.GameplayObjects
{
    struct PrizeParams
    {
        public Transform pos;
        public GameObject prize;
        
    }
    
    public class PrizeShelf : MonoBehaviour, IPrizeShelf
    {
        private int _maxRewardCap;
        [SerializeField] private Transform _leftPos, _midPos, _rightPos;

        public void Init(int maxRewardCap) 
        {
            _maxRewardCap = maxRewardCap;
        }

        private void Start()
        {
            //_heldPrizes = new List<IPrize>();
        }
    }
}
