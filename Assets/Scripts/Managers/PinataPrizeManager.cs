using Assets.Scripts.ScriptableObjects;
using Assets.Scripts.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.GameplayObjects
{

    public enum PrizeTypes
    {
        Bomb, Lightning, Heart,
        Crystal, Sticker, Booster
    }

    public class PrizeData
    {
        public PrizeTypes PrizeType;
        public int PrizeAmount;

        public PrizeData(PrizeTypes prizeType, int prizeAmount)
        {
            PrizeType = prizeType;
            PrizeAmount = prizeAmount;
        }
    }

    public class PinataPrizeManager : IPinataPrizeManager
    {
        private AssetReference _assetRef;
        private List<PrizeTypes> _prizeTypes;
        public Dictionary<PrizeTypes, ObjectTypes> _prizeObjectRefs { private set; get; }

        public PinataPrizeManager(AssetReference assetRef, GameParameters gameParams)
        {
            _assetRef = assetRef;
            _prizeTypes = ((PrizeTypes[])Enum.GetValues(typeof(PrizeTypes))).ToList();
            _prizeObjectRefs = new Dictionary<PrizeTypes, ObjectTypes> { { PrizeTypes.Bomb, ObjectTypes.BombPrize },
                { PrizeTypes.Lightning, ObjectTypes.LightningPrize },{ PrizeTypes.Heart, ObjectTypes.HeartPrize },
                { PrizeTypes.Crystal, ObjectTypes.CrystalPrize }, { PrizeTypes.Sticker, ObjectTypes.StickerPrize },
                { PrizeTypes.Booster, ObjectTypes.BoosterPrize }
            };
        }

        public List<PrizeData> GetPinataPrizes()
        {
            var selectedPrizeTypes = GetRandomPrizes(Randomizer.GetNumberInRange(3, 6));
            return GeneratePrizeData(selectedPrizeTypes, 3, 10);
        }

        private List<PrizeData> GeneratePrizeData(List<PrizeTypes> types, int minAmt, int maxAmt)
        {
            var prizeList = new List<PrizeData>();
            for (int i = 0; i < types.Count; i++)
            {
                prizeList.Add(new PrizeData(types[i], Randomizer.GetNumberInRange(minAmt, maxAmt + 1)));
            }
            return prizeList;
        }

        private List<PrizeTypes> GetRandomPrizes(int amount)
        {
            //randomize the prize types list then take the first N items
            return (List<PrizeTypes>)Randomizer.ShuffleList(_prizeTypes).Take(amount);
        }
    }
}
