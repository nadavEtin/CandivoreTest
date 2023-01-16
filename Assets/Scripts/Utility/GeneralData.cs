using UnityEngine;

namespace Assets.Scripts.Utility
{
    
    public static class GeneralData
    {
        public static float OrthoScreenHeight { private set; get; }
        public static float ScreenWidth { private set; get; }

        public static void InitValues()
        {
            OrthoScreenHeight = Camera.main.orthographicSize * 2.0f;
            ScreenWidth = (OrthoScreenHeight / Screen.height) * Screen.width;
        }
    }
}