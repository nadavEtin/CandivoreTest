using UnityEngine;

namespace Assets.Scripts.Utility
{
    public static class Randomizer
    {
        public static float GetNumberInRange(float lowEnd, float highEnd)
        {
            return Random.Range(lowEnd, highEnd);
        }

        public static int GetPositiveOrNegative()
        {
            var rnd = Random.Range(0, 2);
            if (rnd == 0)
                return -1;
            else
                return 1;
        }
    }
}
