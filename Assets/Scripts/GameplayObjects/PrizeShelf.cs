using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.GameplayObjects
{
    public class PrizeShelf : MonoBehaviour, IPrizeShelf
    {
        private int _maxRewardCap;
        //private List<IPrize> _heldPrizes;

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
