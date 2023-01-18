using System;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public interface IAnimationManager
    {
        void PinataHitMovement(GameObject objectToMove, GameObject spriteOff, GameObject spriteOn, Vector3 startingPos, Action animationEndCb);
    }
}
