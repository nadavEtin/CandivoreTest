using Assets.Scripts.ScriptableObjects;
using Assets.Scripts.Utility;
using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class AnimationManager : IAnimationManager
    {
        private AnimationParameters _animParams;

        private readonly Dictionary<ObjectTypes, ObjectTypes> _pinataPrizeParticles;

        public AnimationManager()
        {
            _animParams = Resources.Load<AnimationParameters>("AnimationParams");

            _pinataPrizeParticles = new Dictionary<ObjectTypes, ObjectTypes> { { ObjectTypes.BombPrize, ObjectTypes.BombParticle },
                { ObjectTypes.BoosterPrize, ObjectTypes.BoosterParticle }, { ObjectTypes.CrystalPrize, ObjectTypes.CrystalParticle },
                { ObjectTypes.HeartPrize, ObjectTypes.HeartParticle}, { ObjectTypes.LightningPrize, ObjectTypes.LightningParticle },
                { ObjectTypes.StickerPrize, ObjectTypes.StickerParticle }, { ObjectTypes.ConfettiParticle, ObjectTypes.ConfettiParticle } };
        }

        public ObjectTypes GetPrizeParticleType(ObjectTypes prizeType)
        {
            return _pinataPrizeParticles[prizeType];
        }

        public void PinataHitMovement(GameObject objectToMove, GameObject spriteOff, GameObject spriteOn, Vector3 startingPos, Action animationEndCb)
        {
            var movement = Randomizer.GetPositiveOrNegative() == 1
                ? WideBounce(objectToMove, spriteOff, spriteOn, startingPos, animationEndCb)
                : TallBounce(objectToMove, spriteOff, spriteOn, startingPos, animationEndCb);
            movement.Play();
        }

        public void MoveParticlesToShelf(Transform obj, Vector3 destination, Action<SpriteRenderer, float> animationEndCb, SpriteRenderer cbTarget)
        {
            Sequence movementSeq = DOTween.Sequence();
            movementSeq.AppendInterval(1.1f)
                .Append(obj.DOMove(destination, 0.25f))
                .InsertCallback(1.1f + 0.15f, () => animationEndCb(cbTarget, 0.3f));
            movementSeq.Play();
        }

        public void MoveParticlesToShelf(Transform obj, Vector3 destination)
        {
            Sequence movementSeq = DOTween.Sequence();
            movementSeq.AppendInterval(1.1f)
                .Append(obj.DOMove(destination, 0.25f));
            movementSeq.Play();
        }

        public void PinataIntro(Transform pinata, Vector3 destination)
        {
            
        }

        public void FadeIn(SpriteRenderer spriteRenderer, float duration)
        {
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0);
            spriteRenderer.DOFade(1, duration).Play();
        }

        public void FadeOut(SpriteRenderer spriteRenderer, float duration)
        {
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 255);
            spriteRenderer.DOFade(0, duration).Play();
        }

        private void SwitchObjects(GameObject turnOff, GameObject turnOn)
        {
            turnOff.SetActive(false);
            turnOn.SetActive(true);
        }


        #region Pinata movement variations

        private Sequence TallBounce(GameObject objectToMove, GameObject spriteOff, GameObject spriteOn, Vector3 startingPos, Action animationEndCb)
        {
            var screenHalfHeight = GeneralData.HalfScreenHeight * 2;
            var screenWidth = GeneralData.ScreenWidth;
            var xMovDir = Randomizer.GetPositiveOrNegative();
            var firstPos = new Vector3(objectToMove.transform.position.x + screenWidth * Randomizer.GetNumberInRange(0.09f, 0.115f) * xMovDir,
                startingPos.y + screenHalfHeight * Randomizer.GetNumberInRange(0.005f, 0.015f), 0);
            var secondPos = new Vector3(firstPos.x + screenWidth * Randomizer.GetNumberInRange(0.13f, 0.145f) * -xMovDir, startingPos.y, 0);
            var thirdPos = startingPos;
            TweenCallback tweenCallback = new(animationEndCb);
            Sequence movementSeq = DOTween.Sequence();
            movementSeq.Append(objectToMove.transform.DOJump(firstPos, 0.75f, 1, 0.3f).SetEase(Ease.OutSine))
                .AppendCallback(() => SwitchObjects(spriteOff, spriteOn))
                .Append(objectToMove.transform.DOJump(secondPos, 0.35f, 1, 0.2f))
                .Append(objectToMove.transform.DOMove(thirdPos, 0.25f).SetEase(Ease.InSine))
                .AppendCallback(tweenCallback);
            return movementSeq;
        }

        private Sequence WideBounce(GameObject objectToMove, GameObject spriteOff, GameObject spriteOn, Vector3 startingPos, Action animationEndCb)
        {
            var screenHalfHeight = GeneralData.HalfScreenHeight * 2;
            var screenWidth = GeneralData.ScreenWidth;
            var xMovDir = Randomizer.GetPositiveOrNegative();
            var firstPos = new Vector3(objectToMove.transform.position.x + screenWidth * Randomizer.GetNumberInRange(0.125f, 0.145f) * xMovDir,
                startingPos.y + screenHalfHeight * Randomizer.GetNumberInRange(0.01f, 0.015f), 0);
            var secondPos = new Vector3(firstPos.x + screenWidth * Randomizer.GetNumberInRange(0.155f, 0.17f) * -xMovDir,
                startingPos.y + screenHalfHeight * Randomizer.GetNumberInRange(0.005f, 0.01f), 0);
            var thirdPos = new Vector3(firstPos.x + screenWidth * Randomizer.GetNumberInRange(0.02f, 0.03f), startingPos.y, 0);
            var fourthPos = startingPos;
            TweenCallback tweenCallback = new(animationEndCb);
            Sequence movementSeq = DOTween.Sequence();
            movementSeq.Append(objectToMove.transform.DOJump(firstPos, 0.65f, 1, 0.25f))
                .AppendCallback(() => SwitchObjects(spriteOff, spriteOn))
                .Append(objectToMove.transform.DOJump(secondPos, 0.3f, 1, 0.13f))
                .Append(objectToMove.transform.DOJump(thirdPos, 0.2f, 1, 0.13f))
                .Append(objectToMove.transform.DOMove(fourthPos, 0.16f).SetEase(Ease.InSine))
                .AppendCallback(tweenCallback);
            return movementSeq;
        }

        #endregion
    }
}