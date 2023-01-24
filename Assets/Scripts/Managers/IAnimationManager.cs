using Assets.Scripts.GameplayObjects;
using Assets.Scripts.ScriptableObjects;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Managers
{
    public interface IAnimationManager
    {
        void MoveParticlesToShelf(Transform obj, ShelfPrizeData shelfPrize, Action<ShelfPrizeData> animationEndCb);
        void MoveParticlesToShelf(Transform obj, Vector3 destination);
        void PinataHitMovement(GameObject objectToMove, GameObject spriteOff, GameObject spriteOn, Vector3 startingPos, Action animationEndCb);
        void FadeIn(SpriteRenderer spriteRenderer, float duration);
        void FadeOut(SpriteRenderer spriteRenderer, float duration);
        ObjectTypes GetPrizeParticleType(ObjectTypes prizeType);
        void PinataIntro(Transform pinata, Vector3 destination);
        void FadeIn(TextMeshPro text, float duration);
        void PinataExplosion(Transform head, Transform body, Transform tail);
        void MoveTransform(Transform objToMove, Vector3 destination, float duration);
        void MoveTransform(Transform objToMove, Vector3 destination, float duration, float delay);
        void FadeIn(Image image, float duration);
    }
}
