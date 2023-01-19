using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Utility
{
    public static class Randomizer
    {
        public static float GetNumberInRange(float lowEnd, float highEnd)
        {
            return UnityEngine.Random.Range(lowEnd, highEnd);
        }
        
        public static int GetNumberInRange(int lowEnd, int highEnd)
        {
            return UnityEngine.Random.Range(lowEnd, highEnd);
        }

        public static int GetPositiveOrNegative()
        {
            var rnd = UnityEngine.Random.Range(0, 2);
            if (rnd == 0)
                return -1;
            else
                return 1;
        }

        public static IList<T> ShuffleList<T>(IList<T> list)
        {
            System.Random rng = new System.Random();
            return list.OrderBy(a => rng.Next()).ToList();            
        }
    }
}
