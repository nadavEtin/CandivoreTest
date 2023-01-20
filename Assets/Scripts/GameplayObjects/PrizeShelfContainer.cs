using Assets.Scripts.ScriptableObjects;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.GameplayObjects
{
    public class PrizeShelfContainer : MonoBehaviour, IPrizeShelfContainer
    {
        [SerializeField] private List<PrizeShelf> _prizeShelves;
        private Dictionary<ObjectTypes, ShelfPrize> _prizePositions;

        public Transform ReceivePrize(ObjectTypes prizeType)
        {
            if (_prizePositions.ContainsKey(prizeType))
            {

                return _prizePositions[prizeType].pos;
            }
            return null;
        }
    }
}
