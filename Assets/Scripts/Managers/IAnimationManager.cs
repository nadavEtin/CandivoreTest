using Assets.Scripts.GameplayObjects;
using Assets.Scripts.ScriptableObjects;
using System;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public interface IAnimationManager
    {
        void MoveParticlesToShelf(Transform obj, ShelfPrizeData shelfPrize, Action<ShelfPrizeData> animationEndCb);
        void MoveParticlesToShelf(Transform obj, Vector3 destination);
        void PinataHitMovement(GameObject objectToMove, GameObject spriteOff, GameObject spriteOn, Vector3 startingPos, Action animationEndCb);
        void FadeSpriteIn(SpriteRenderer spriteRenderer, float duration);
        void FadeSpriteOut(SpriteRenderer spriteRenderer, float duration);
        ObjectTypes GetPrizeParticleType(ObjectTypes prizeType);
        void PinataIntro(Transform pinata, Vector3 destination);
        void FadeTextIn(TextMeshPro text, float duration);
    }
}
