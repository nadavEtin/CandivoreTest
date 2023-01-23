using Assets.Scripts.ScriptableObjects;
using Assets.Scripts.Utility;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.GameplayObjects.GameplayObjSubclasses
{
    public class PinataPrizeGenerator : IPinataPrizeGenerator
    {
        private readonly GameParameters _gameParams;
        private readonly AssetReference _assetRef;
        private List<ObjectTypes> _prizes;
        private readonly int prizesPerClick;

        public PinataPrizeGenerator(AssetReference assetRef, GameParameters gameParams)
        {
            _assetRef = assetRef;
            _gameParams = gameParams;
            _prizes = new List<ObjectTypes>();
            GeneratePinataPrizes();
            prizesPerClick = (int)(_prizes.Count / _gameParams.PinataClicksToDestroy * 0.9f);
        }

        //Return a list of prizes for the current hit
        public List<ObjectTypes> GetPinataPrizes(int hitPower, bool stillAlive)
        {
            var clickAmount = prizesPerClick * hitPower;
            var currentClickPrizes = stillAlive ? _prizes.Take(clickAmount).ToList() : _prizes;
            if (stillAlive)
                _prizes.RemoveRange(0, clickAmount);
            return currentClickPrizes;
        }

        //Randomly generate prize types and amounts for this pinata
        private void GeneratePinataPrizes()
        {
            var selectedPrizeTypes = GetRandomPrizes(Randomizer.GetNumberInRange(3, 6));
            _prizes = GeneratePrizeData(selectedPrizeTypes, 5, 10);
        }

        private List<ObjectTypes> GeneratePrizeData(List<ObjectTypes> types, int minAmt, int maxAmt)
        {
            var prizeList = new List<ObjectTypes>();
            for (int i = 0; i < types.Count; i++)
            {
                prizeList.AddRange(Enumerable.Repeat(types[i], Randomizer.GetNumberInRange(minAmt, maxAmt)));
            }
            prizeList = (List<ObjectTypes>)Randomizer.ShuffleList(prizeList);
            return prizeList;
        }

        private List<ObjectTypes> GetRandomPrizes(int amount)
        {
            //Randomize the prize types list then take the first N items
            var prizeList = new List<ObjectTypes>(_assetRef.PinataPrizes);
            prizeList = Randomizer.ShuffleList(prizeList).Take(amount).ToList();
            return prizeList;
        }
    }
}
