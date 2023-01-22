using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Managers;
using Assets.Scripts.ScriptableObjects;
using UnityEngine;

namespace Assets.Scripts.GameplayObjects
{
    //object holding all data related to a prize position on the shelf
    public class ShelfPrize
    {
        public int prizeAmount;
        public GameObject prizeObj;
        public Transform shelfPos;
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
        [SerializeField] private List<Transform> _shelfPositions;
        private IAnimationManager _animationManager;
        private AssetReference _assetRef;
        private SpriteRenderer _spriteRenderer;

        private void Start()
        {
            _prizes = new List<ShelfPrize>();
            _spriteRenderer = GetComponent<SpriteRenderer>();

            for (var i = 0; i < _shelfPositions.Count; i++)
            {
                var newPrize = new ShelfPrize(_shelfPositions[i], null, 0, ObjectTypes.None);
                _prizes.Add(newPrize);
            }
        }

        public List<ShelfPrize> _prizes { private set; get; }

        public void Init(AssetReference assetReference, IAnimationManager animationManager)
        {
            _assetRef = assetReference;
            _animationManager = animationManager;
        }

        public ShelfPrize AddPrize(ObjectTypes prizeType, int amount, GameObject particleFx)
        {
            var matchingPrize = _prizes.FirstOrDefault(e => e.type == prizeType);

            //if the shelf already holds the prize type
            if (matchingPrize != null)
            {
                matchingPrize.prizeAmount += amount;
                _animationManager.MoveParticlesToShelf(particleFx.transform, matchingPrize.shelfPos.position);
                //TODO: update the display
                return matchingPrize;
            }

            if (_prizes.FirstOrDefault(e => e.type == ObjectTypes.None) != null)
            {
                var emptyPrize = _prizes.FirstOrDefault(e => e.type == ObjectTypes.None);
                emptyPrize.type = prizeType;
                emptyPrize.prizeAmount = amount;
                emptyPrize.prizeObj = Instantiate(_assetRef.PrefabTypes[prizeType]);
                var spriteRen = emptyPrize.prizeObj.GetComponent<SpriteRenderer>();
                spriteRen.color = new Color(255, 255, 255, 0);
                emptyPrize.prizeObj.transform.position = emptyPrize.shelfPos.position;
                emptyPrize.prizeObj.transform.SetParent(transform, true);
                _animationManager.MoveParticlesToShelf(particleFx.transform, emptyPrize.shelfPos.position,
                    _animationManager.FadeIn,
                    emptyPrize.prizeObj.GetComponent<SpriteRenderer>());

                if (_spriteRenderer.color.a == 0)
                    ShowShelf();

                return emptyPrize;
            }

            Debug.LogError("All prize positions taken");
            return null;
        }

        private void ShowShelf()
        {
            _spriteRenderer.enabled = true;
            _animationManager.FadeIn(_spriteRenderer, 0.4f);
        }
    }
}