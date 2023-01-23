using Assets.Scripts.Managers;
using Assets.Scripts.ScriptableObjects;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.GameplayObjects
{
    //Object holding all data related to a prize position on the shelf
    public class ShelfPrizeData
    {
        public int prizeAmount;
        public GameObject prizeObj;
        public SpriteRenderer amountDisplay;
        public TextMeshPro amountText;
        public Transform shelfPos;
        public ObjectTypes type;

        public ShelfPrizeData(Transform pos, SpriteRenderer display, TextMeshPro text, GameObject prizeObj, int prizeAmount, ObjectTypes type)
        {
            shelfPos = pos;
            amountDisplay = display;
            amountText = text;
            this.prizeObj = prizeObj;
            this.prizeAmount = prizeAmount;
            this.type = type;
        }
    }

    [RequireComponent(typeof(SpriteRenderer))]
    public class PrizeShelf : MonoBehaviour, IPrizeShelf
    {
        [SerializeField] private List<Transform> _shelfPositions;
        [SerializeField] private List<GameObject> _amountDisplays;
        public List<ShelfPrizeData> _prizes { private set; get; }
        private IAnimationManager _animationManager;
        private AssetReference _assetRef;
        private SpriteRenderer _spriteRenderer;        

        public void Init(AssetReference assetReference, IAnimationManager animationManager)
        {
            _assetRef = assetReference;
            _animationManager = animationManager;
        }

        public ShelfPrizeData AddPrize(ObjectTypes prizeType, int amount, GameObject particleFx)
        {
            var matchingPrize = _prizes.FirstOrDefault(e => e.type == prizeType);

            //Prize type already exists on this shelf
            if (matchingPrize != null)
            {
                matchingPrize.prizeAmount += amount;
                _animationManager.MoveParticlesToShelf(particleFx.transform, matchingPrize.shelfPos.position);
                UpdatePrizeAmountText(matchingPrize.amountText, matchingPrize.prizeAmount);
                return matchingPrize;
            }

            //Add new prize type to the shelf
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
                _animationManager.MoveParticlesToShelf(particleFx.transform, emptyPrize, ShowPrize);

                if (_spriteRenderer.color.a == 0)
                    ShowShelf();

                return emptyPrize;
            }

            Debug.LogError("All prize positions taken");
            return null;
        }

        private void Start()
        {
            _prizes = new List<ShelfPrizeData>();
            _spriteRenderer = GetComponent<SpriteRenderer>();

            //Populate shelf positions with default empty shelf data objects for later use
            for (var i = 0; i < _shelfPositions.Count; i++)
            {
                var newPrize = new ShelfPrizeData(_shelfPositions[i], _amountDisplays[i].GetComponentInChildren<SpriteRenderer>(), _amountDisplays[i].GetComponentInChildren<TextMeshPro>(), null, 0, ObjectTypes.None);
                _prizes.Add(newPrize);
            }
        }

        private void ShowPrize(ShelfPrizeData shelfPrize)
        {
            _animationManager.FadeSpriteIn(shelfPrize.prizeObj.GetComponent<SpriteRenderer>(), 0.3f);
            _animationManager.FadeSpriteIn(shelfPrize.amountDisplay.GetComponentInChildren<SpriteRenderer>(), 0.3f);
            var text = shelfPrize.amountDisplay.GetComponentInChildren<TextMeshPro>();
            _animationManager.FadeTextIn(text, 0.3f);
            UpdatePrizeAmountText(text, shelfPrize.prizeAmount);
        }

        private void UpdatePrizeAmountText(TMP_Text text, float newAmount)
        {
            text.text = string.Format("x{0}", newAmount);
        }

        private void ShowShelf()
        {
            _spriteRenderer.enabled = true;
            _animationManager.FadeSpriteIn(_spriteRenderer, 0.4f);
        }
    }
}