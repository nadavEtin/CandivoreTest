using UnityEngine;

namespace Assets.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "GameParams", menuName = "ScriptableObjects/Game Parameters")]
    public class GameParameters : ScriptableObject
    {
        [Header("Prize shelves")]
        [Space(3)]
        public float ShelfContainerHeightPos;
        public float GameEndShelfContainerPos;

        [Space(10)]
        [Header("Pinata")]
        [Space(3)]
        public float IdlePinataScreenHeight;
        public int PinataClicksToDestroy, ShortClickPower, LongClickPower;
        //Minimum time of holding down the click to become a long click
        public float LongClickThreshold;

        [Space(10)]
        [Header("Audio")]
        [Space(3)]
        public float MusicVolume;
        public float SoundVolume;
    }
}

