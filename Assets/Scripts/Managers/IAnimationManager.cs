using Assets.Scripts.ScriptableObjects;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public interface IAnimationManager
    {
        Dictionary<ObjectTypes, ObjectTypes> PinataPrizeParticles { get; }
        void FadeIn(SpriteRenderer sprite);
        void FadeOut(SpriteRenderer sprite);
        void MoveParticlesToShelf(Transform obj, Vector3 destination, Action<SpriteRenderer> animationEndCb, SpriteRenderer cbTarget);
        void MoveParticlesToShelf(Transform obj, Vector3 destination);
        void PinataHitMovement(GameObject objectToMove, GameObject spriteOff, GameObject spriteOn, Vector3 startingPos, Action animationEndCb);
    }
}
