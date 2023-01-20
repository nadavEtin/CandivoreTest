using Assets.Scripts.ScriptableObjects;
using Assets.Scripts.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.GameplayObjects.GameplayObjSubclasses
{
    public class PinataPrizeGenerator : IPinataPrizeGenerator
    {
        private AssetReference _assetRef;

        public PinataPrizeGenerator(AssetReference assetRef, GameParameters gameParams)
        {
            _assetRef = assetRef;
        }

        public List<ObjectTypes> GetPinataPrizes()
        {
            var selectedPrizeTypes = GetRandomPrizes(Randomizer.GetNumberInRange(3, 6));
            return GeneratePrizeData(selectedPrizeTypes, 3, 10);
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
            //randomize the prize types list then take the first N items
            var prizeList = new List<ObjectTypes>(_assetRef.PinataPrizes);
            prizeList = Randomizer.ShuffleList(prizeList).Take(amount).ToList();
            return prizeList;
        }
    }
}
