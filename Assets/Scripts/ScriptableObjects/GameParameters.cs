using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "GameParams", menuName = "ScriptableObjects/Game Parameters")]
    public class GameParameters : ScriptableObject
    {
        [Header("Prize shelves")]
        [Space(3)]
        public List<float> PrizeShelfHeight;
        public int InitialShelfCount, PrizeShelfMaxCap;

        [Space(10)]
        [Header("Pinata")]
        [Space(3)]
        public float IdlePinataScreenHeight;

        [Space(10)]
        [Header("Audio")]
        [Space(3)]
        public float MusicVolume;
        public float SoundVolume;
    }
}

