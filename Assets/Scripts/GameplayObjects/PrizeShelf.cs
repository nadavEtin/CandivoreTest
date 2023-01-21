using Assets.Scripts.ScriptableObjects;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.GameplayObjects
{
    //object holding all data related to a prize position on the shelf
    public class ShelfPrize
    {
        public Transform shelfPos;
        public GameObject prizeObj;
        public int prizeAmount;
        public ObjectTypes type;

        public ShelfPrize(Transform pos, GameObject prizeObj, int prizeAmount, ObjectTypes type)
        {
            shelfPos = pos;
            this.prizeObj = prizeObj;
            this.prizeAmount = prizeAmount;
            this.type = type;
        }
    }

    [RequireComponent(typeof(SpriteRenderer))]
    public class PrizeShelf : MonoBehaviour, IPrizeShelf
    {
        public List<ShelfPrize> _prizes { private set; get; }
        [SerializeField] private List<Transform> _shelfPositions;
        private AssetReference _assetRef;
        private SpriteRenderer _spriteRenderer;

        public void Init(AssetReference assetReference)
        {
            _assetRef = assetReference;
        }

        public ShelfPrize AddPrize(ObjectTypes prizeType, int amount)
        {
            _spriteRenderer.enabled = true;

            var matchingPrize = _prizes.FirstOrDefault(e => e.type == prizeType);
            if(matchingPrize != null)
            {
                matchingPrize.prizeAmount += amount;
                //TODO: update the display
                return matchingPrize;
            }
            else if (_prizes.FirstOrDefault(e => e.type == ObjectTypes.None) != null)
            {
                var emptyPrize = _prizes.FirstOrDefault(e => e.type == ObjectTypes.None);
                emptyPrize.type = prizeType;
                emptyPrize.prizeAmount = amount;
                emptyPrize.prizeObj = Instantiate(_assetRef.PrefabTypes[prizeType]);                
                emptyPrize.prizeObj.transform.position = emptyPrize.shelfPos.position;
                emptyPrize.prizeObj.transform.SetParent(transform, true);
                return emptyPrize;
            }
            else
            {
                Debug.LogError("All prize positions taken");
                return null;
            }
        }

        private void Start()
        {
            _prizes = new List<ShelfPrize>();
            _spriteRenderer = GetComponent<SpriteRenderer>();

            for (int i = 0; i < _shelfPositions.Count; i++)
            {
                var newPrize = new ShelfPrize(_shelfPositions[i], null, 0, ObjectTypes.None);
                _prizes.Add(newPrize);
            }
        }
    }
}
