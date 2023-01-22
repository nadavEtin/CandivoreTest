using Assets.Scripts.ScriptableObjects;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public interface IAnimationManager
    {
        //Dictionary<ObjectTypes, ObjectTypes> _pinataPrizeParticles { get; }
        void MoveParticlesToShelf(Transform obj, Vector3 destination, Action<SpriteRenderer, float> animationEndCb, SpriteRenderer cbTarget);
        void MoveParticlesToShelf(Transform obj, Vector3 destination);
        void PinataHitMovement(GameObject objectToMove, GameObject spriteOff, GameObject spriteOn, Vector3 startingPos, Action animationEndCb);
        void FadeIn(SpriteRenderer spriteRenderer, float duration);
        void FadeOut(SpriteRenderer spriteRenderer, float duration);
        ObjectTypes GetPrizeParticleType(ObjectTypes prizeType);
    }
}
