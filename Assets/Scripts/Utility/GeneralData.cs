using UnityEngine;

namespace Assets.Scripts.Utility
{
    public static class GeneralData
    {
        public static float HalfScreenHeight { private set; get; }
        public static float ScreenWidth { private set; get; }
        public static Vector3 TopLeftCorner { private set; get; }
        public static Vector3 TopRightCorner { private set; get; }

        public static void InitValues()
        {
            HalfScreenHeight = Camera.main.orthographicSize;
            ScreenWidth = (HalfScreenHeight / Screen.height) * Screen.width;
            TopLeftCorner = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0));
            TopLeftCorner = new Vector3(TopLeftCorner.x, TopLeftCorner.y, 0);
            TopRightCorner = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
            TopRightCorner = new Vector3(TopRightCorner.x, TopRightCorner.y, 0);
        }
    }
}