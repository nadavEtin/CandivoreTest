using Assets.Scripts.Managers;
using Assets.Scripts.ScriptableObjects;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.GameplayObjects
{
    public class PrizeShelfContainer : MonoBehaviour, IPrizeShelfContainer
    {
        [SerializeField] private List<PrizeShelf> _prizeShelves;
        private Dictionary<ObjectTypes, PrizeShelf> _prizePositions;
        private IAnimationManager _animationManager;

        public void Init(AssetReference assetReference, IAnimationManager animationManager)
        {
            _animationManager = animationManager;
            for (int i = 0; i < _prizeShelves.Count; i++)
            {
                _prizeShelves[i].Init(assetReference, _animationManager);
            }
        }

        public ShelfPrize ReceivePrize(ObjectTypes prizeType, int amount, GameObject particleFx)
        {
            if (_prizePositions.ContainsKey(prizeType))
            {
                return _prizePositions[prizeType].AddPrize(prizeType, amount, particleFx);
            }
            else
            {
                var freeShelf = _prizeShelves.FirstOrDefault(x => x._prizes.Any(y => y.type == ObjectTypes.None));
                if (freeShelf != null)
                {
                    var prizePos = freeShelf.AddPrize(prizeType, amount, particleFx);
                    _prizePositions.Add(prizeType, freeShelf);
                    return prizePos;
                }
                else
                {
                    Debug.LogError("all shelves are full");
                }
            }

            return null;
        }

        private void Start()
        {
            _prizePositions = new Dictionary<ObjectTypes, PrizeShelf>();
        }
    }
}
