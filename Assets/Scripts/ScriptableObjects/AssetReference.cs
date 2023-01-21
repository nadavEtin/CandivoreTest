using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.ScriptableObjects
{
    public enum ObjectTypes
    {
        None, HeartPrize, BombPrize, LightningPrize,
        StickerPrize, BoosterPrize, CrystalPrize,
        ConfettiParticle, PrizeShelfContainer, PinataRope, Pinata,
        BombParticle, HeartParticle, LightningParticle, StickerParticle, BoosterParticle, CrystalParticle
    }

    [CreateAssetMenu(fileName = "AssetReference", menuName = "ScriptableObjects/Asset Reference")]
    public class AssetReference : ScriptableObject
    {
        public Dictionary<ObjectTypes, GameObject> PrefabTypes { private set; get; }
        public List<ObjectTypes> PinataPrizes { private set; get; }

        public void Init()
        {
            PrefabTypes = new Dictionary<ObjectTypes, GameObject>
            {
                { ObjectTypes.PrizeShelfContainer, PrizeShelfContainer }, { ObjectTypes.ConfettiParticle, ConfettiParticle }, { ObjectTypes.Pinata, Pinata },
                { ObjectTypes.PinataRope, PinataRope}, { ObjectTypes.BombPrize, Bomb }, { ObjectTypes.BoosterPrize, Booster },
                { ObjectTypes.CrystalPrize, Crystal }, { ObjectTypes.HeartPrize, Heart}, { ObjectTypes.LightningPrize, Lightning }, 
                { ObjectTypes.StickerPrize, Sticker }, { ObjectTypes.BombParticle, BombParticle }, { ObjectTypes.BoosterParticle, BoosterParticle },
                { ObjectTypes.CrystalParticle, CrystalParticle }, { ObjectTypes.HeartParticle, HeartParticle}, 
                { ObjectTypes.LightningParticle, LightningParticle }, { ObjectTypes.StickerParticle, StickerParticle }
            };

            PinataPrizes = new List<ObjectTypes> { ObjectTypes.None, ObjectTypes.BombPrize, ObjectTypes.BoosterPrize,
                ObjectTypes.CrystalPrize, ObjectTypes.HeartPrize, ObjectTypes.LightningPrize, ObjectTypes.StickerPrize};
        }

        [Header("Sprites")]
        [Space(3)]
        [SerializeField] private List<Sprite> bombs = new List<Sprite>();
        [SerializeField] private List<Sprite> hearts = new List<Sprite>();
        [SerializeField] private List<Sprite> crystals = new List<Sprite>();
        [SerializeField] private List<Sprite> lightning = new List<Sprite>();
        [SerializeField] private List<Sprite> stickers = new List<Sprite>();
        [SerializeField] private List<Sprite> boosters = new List<Sprite>();

        [Space(10)]
        [Header("Gameplay objects")]
        [Space(3)]
        [SerializeField] private GameObject PrizeShelfContainer;
        [SerializeField] private GameObject Pinata;
        [SerializeField] private GameObject ConfettiParticle;
        [SerializeField] private GameObject PinataRope;

        [Space(10)]
        [Header("Prizes")]
        [Space(3)]
        [SerializeField] private GameObject Bomb;
        [SerializeField] private GameObject Heart, Lightning, Sticker, Booster, Crystal;
        [SerializeField] private GameObject BombParticle, HeartParticle, LightningParticle, StickerParticle, BoosterParticle, CrystalParticle;
    }
}


