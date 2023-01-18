using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.ScriptableObjects
{
    public enum ObjectTypes
    {
        HeartReward, BombReward, ConfettiParticle,
        PrizeShelf, PinataRope
    }
    
    [CreateAssetMenu(fileName = "AssetReference", menuName = "ScriptableObjects/Asset Reference")]
    public class AssetReference : ScriptableObject
    {
        public Dictionary<ObjectTypes, GameObject> PrefabTypes { private set; get; }

        public void Init()
        {
            PrefabTypes = new Dictionary<ObjectTypes, GameObject>
            {
                {ObjectTypes.PrizeShelf, PrizeShelf}, { ObjectTypes.ConfettiParticle, ConfettiParticle }
            };
        }
        
        [Header("Sprites")]
        [Space(3)]
        public List<Sprite> bombs = new List<Sprite>();
        public List<Sprite> hearts = new List<Sprite>();
        public List<Sprite> crystals = new List<Sprite>();
        public List<Sprite> lightning = new List<Sprite>();
        public List<Sprite> stickers = new List<Sprite>();
        public List<Sprite> boosters = new List<Sprite>();

        [Space(10)]
        [Header("Prefabs")]
        [Space(3)]
        public GameObject PrizeShelf;
        public GameObject Pinata;
        public GameObject ConfettiParticle;
        public GameObject PinataRope;
    }
}


