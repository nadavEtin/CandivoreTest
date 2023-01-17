using UnityEngine;

namespace Assets.Scripts.Utility
{
    
    public static class GeneralData
    {
        public static float HalfScreenHeight { private set; get; }
        public static float ScreenWidth { private set; get; }

        public static void InitValues()
        {
            HalfScreenHeight = Camera.main.orthographicSize;
            ScreenWidth = (HalfScreenHeight / Screen.height) * Screen.width;
        }
    }
}