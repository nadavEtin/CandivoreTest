using System.Collections;
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
        //public float FirstShelfHeight;
        //public float SecondShelfHeight;
        public int InitialShelfCount, PrizeShelfMaxCap;
    }
}

