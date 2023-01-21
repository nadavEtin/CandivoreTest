using UnityEngine;

namespace Assets.Scripts.ScriptableObjects
{
    [System.Serializable]
    public class SerializableTuple
    {
        public float minVal;
        public float maxVal;

        public SerializableTuple() { }
        public SerializableTuple(float minVal, float maxVal)
        {
            this.minVal = minVal;
            this.maxVal = maxVal;
        }
    }

    [CreateAssetMenu(fileName = "AnimationParams", menuName = "ScriptableObjects/Animation Params")]
    public class AnimationParameters : ScriptableObject
    {
        [Header("Pinata movement")]
        [Space(3)]
        public SerializableTuple firstPosWidth;
        public SerializableTuple firstPosHeight, secondPosWidth, secondPosHeight, thirdPosWidth, thirdPosHeight;
        public float firstPosTime, secondPosTime, thirdPosTime, fourthPosTime, firstPosJumpP, secondPosJumpP, thirdPosJumpP;
    }
}
