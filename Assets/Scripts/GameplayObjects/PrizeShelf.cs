using Assets.Scripts.ScriptableObjects;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.GameplayObjects
{
    public class ShelfPrize
    {
        public Transform pos;
        public GameObject prizeObj;
        public int prizeAmount;
        public ObjectTypes type;

        public ShelfPrize(Transform pos, GameObject prizeObj, int prizeAmount, ObjectTypes type)
        {
            this.pos = pos;
            this.prizeObj = prizeObj;
            this.prizeAmount = prizeAmount;
            this.type = type;
        }
    }

    public class PrizeShelf : MonoBehaviour, IPrizeShelf
    {
        public List<ShelfPrize> _prizes { private set; get; }
        [SerializeField] private List<Transform> _shelfPositions;
        private AssetReference _assetRef;

        public void Init(AssetReference assetReference)
        {
            _assetRef = assetReference;
        }

        public Vector3 AddPrize(ObjectTypes prizeType, int amount)
        {
            var matchingPrize = _prizes.FirstOrDefault(e => e.type == prizeType);
            if(matchingPrize != null)
            {
                matchingPrize.prizeAmount += amount;
                //TODO: update the display
                return matchingPrize.pos.position;
            }
            else if (_prizes.FirstOrDefault(e => e.type == ObjectTypes.None) != null)
            {
                var emptyPrize = _prizes.FirstOrDefault(e => e.type == ObjectTypes.None);
                emptyPrize.type = prizeType;
                emptyPrize.prizeAmount = amount;
                emptyPrize.prizeObj = Instantiate(_assetRef.PrefabTypes[prizeType], transform);
                emptyPrize.prizeObj.transform.localPosition = emptyPrize.pos.localPosition;
                return emptyPrize.pos.position;
            }
            else
            {
                Debug.LogError("All prize positions taken");
                return Vector3.zero;
            }
        }

        private void Start()
        {
            _prizes = new List<ShelfPrize>();

            for (int i = 0; i < _shelfPositions.Count; i++)
            {
                var newPrize = new ShelfPrize(_shelfPositions[i], null, 0, ObjectTypes.None);
                _prizes.Add(newPrize);
            }
        }
    }
}
