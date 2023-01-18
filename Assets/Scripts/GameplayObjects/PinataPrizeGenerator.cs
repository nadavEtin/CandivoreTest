using Assets.Scripts.Utility;
using System.Collections.Generic;

namespace Assets.Scripts.GameplayObjects
{

    public enum PrizeTypes
    {
        Bomb, Lightning, Heart,
        Crystal, Sticker, Booster
    }

    public class PinataPrize
    {
        public PrizeTypes PrizeType;
        public int PrizeAmount;
    }

    public class PinataPrizeGenerator : IPinataPrizeGenerator
    {

        private int _rewardAmountMax = 6;

        public PinataPrizeGenerator()
        {

        }

        public List<PinataPrize> GetPinataPrizes()
        {
            var prizeList = new List<PinataPrize>();
            var prizeAmount = Randomizer.GetNumberInRange(3, 7);

            return prizeList;
        }
    }
}
