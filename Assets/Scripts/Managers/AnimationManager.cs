using Assets.Scripts.GameplayObjects;
using Assets.Scripts.ScriptableObjects;
using Assets.Scripts.Utility;
using DG.Tweening;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class AnimationManager : IAnimationManager
    {
        //Used for quickly tweaking animation parameters
        private readonly AnimationParameters _animParams;
        //Associates the correct particle effect with its prize type
        private readonly Dictionary<ObjectTypes, ObjectTypes> _pinataPrizeParticles;
        private float screenHalfHeight, screenFullHeight, screenWidth;

        public AnimationManager()
        {
            screenHalfHeight = GeneralData.HalfScreenHeight;
            screenFullHeight = GeneralData.HalfScreenHeight * 2;
            screenWidth = GeneralData.ScreenWidth;
            _animParams = Resources.Load<AnimationParameters>("AnimationParams");

            _pinataPrizeParticles = new Dictionary<ObjectTypes, ObjectTypes> { { ObjectTypes.BombPrize, ObjectTypes.BombParticle },
                { ObjectTypes.BoosterPrize, ObjectTypes.BoosterParticle }, { ObjectTypes.CrystalPrize, ObjectTypes.CrystalParticle },
                { ObjectTypes.HeartPrize, ObjectTypes.HeartParticle}, { ObjectTypes.LightningPrize, ObjectTypes.LightningParticle },
                { ObjectTypes.StickerPrize, ObjectTypes.StickerParticle }, { ObjectTypes.ConfettiParticle, ObjectTypes.ConfettiParticle },
                { ObjectTypes.BigConfettiParticle, ObjectTypes.BigConfettiParticle }
            };
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

        public void MoveParticlesToShelf(Transform obj, ShelfPrizeData shelfPrize, Action<ShelfPrizeData> animationEndCb)
        {
            Sequence movementSeq = DOTween.Sequence();
            movementSeq.AppendInterval(1.1f)
                .Append(obj.DOMove(shelfPrize.shelfPos.position, 0.25f))
                .InsertCallback(1.25f, () => animationEndCb(shelfPrize));
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
            pinata.DOMove(destination, 1f).SetEase(Ease.OutBounce);
        }

        public void PinataExplosion(Transform head, Transform body, Transform tail)
        {
            PinataExplosionAnimation(head, body, tail).Play();
        }

        public void MoveTransform(Transform objToMove, Vector3 destination, float duration)
        {
            objToMove.DOMove(destination, duration);
        }

        public void FadeSpriteIn(SpriteRenderer spriteRenderer, float duration)
        {
            spriteRenderer.color = SetColorAlpha(spriteRenderer.color, false);
            spriteRenderer.DOFade(1, duration).Play();
        }

        public void FadeSpriteOut(SpriteRenderer spriteRenderer, float duration)
        {
            spriteRenderer.color = SetColorAlpha(spriteRenderer.color, true);
            spriteRenderer.DOFade(0, duration).Play();
        }

        public void FadeTextIn(TextMeshPro text, float duration)
        {
            text.color = SetColorAlpha(text.color, false);
            text.DOFade(1, duration).Play();
        }

        private Color SetColorAlpha(Color curColor, bool visible)
        {
            return new Color(curColor.r, curColor.g, curColor.b, visible ? 255 : 0);
        }

        private void SwitchObjects(GameObject turnOff, GameObject turnOn)
        {
            turnOff.SetActive(false);
            turnOn.SetActive(true);
        }


        #region Pinata movement variations

        private Sequence PinataExplosionAnimation(Transform head, Transform body, Transform tail)
        {
            Sequence fullAnim = DOTween.Sequence();
            Sequence headMovement = DOTween.Sequence();
            Sequence bodyMovement = DOTween.Sequence();
            Sequence tailMovement = DOTween.Sequence();
            var headDestination = new Vector3(0 - screenWidth * Randomizer.GetNumberInRange(0.5f, 0.65f), -screenFullHeight, 0);
            var headBounceTarget = new Vector3(headDestination.x * Randomizer.GetNumberInRange(0.75f, 0.85f),
                head.position.y * Randomizer.GetNumberInRange(0.8f, 0.9f), 0);
            var bodyDestination = new Vector3(0 - screenWidth * Randomizer.GetNumberInRange(0.4f, 0.55f), -screenFullHeight, 0);
            var bodyBounceTarget = new Vector3(bodyDestination.x * Randomizer.GetNumberInRange(0.75f, 0.85f),
                body.position.y * Randomizer.GetNumberInRange(0.8f, 0.9f), 0);
            var tailDestination = new Vector3(0 + screenWidth * Randomizer.GetNumberInRange(0.4f, 0.55f), -screenFullHeight, 0);
            var tailBounceTarget = new Vector3(tailDestination.x * Randomizer.GetNumberInRange(0.75f, 0.85f),
                tail.position.y * Randomizer.GetNumberInRange(0.8f, 0.9f), 0);
            headMovement.Append(head.DOJump(headBounceTarget, 1.3f, numJumps: 1, duration: 1.2f)
                .Join(head.DORotate(new Vector3(0, 0, -35f), 1.2f))
                .Append(head.DOMove(headDestination, 2f))
                .Join(head.DORotate(new Vector3(0, 0, -60f), 1.7f)));
            bodyMovement.Append(body.DOJump(bodyBounceTarget, 1.5f, numJumps: 1, duration: 1.2f)
                .Join(body.DORotate(new Vector3(0, 0, -20f), 1.2f))
                .Append(body.DOMove(bodyDestination, 2f))
                .Join(body.DORotate(new Vector3(0, 0, -35f), 1.7f)));
            tailMovement.Append(tail.DOJump(tailBounceTarget, 1.5f, numJumps: 1, duration: 1.2f)
                .Join(tail.DORotate(new Vector3(0, 0, 20f), 1.2f))
                .Append(tail.DOMove(tailDestination, 2f))
                .Join(tail.DORotate(new Vector3(0, 0, 35f), 1.7f)));
            fullAnim.Append(headMovement).Join(bodyMovement).Join(tailMovement);

            return fullAnim;
        }

        private Sequence TallBounce(GameObject objectToMove, GameObject spriteOff, GameObject spriteOn, Vector3 startingPos, Action animationEndCb)
        {
            var xMovDir = Randomizer.GetPositiveOrNegative();
            var firstPos = new Vector3(objectToMove.transform.position.x + screenWidth * Randomizer.GetNumberInRange(0.09f, 0.115f) * xMovDir,
                startingPos.y + screenFullHeight * Randomizer.GetNumberInRange(0.005f, 0.015f), 0);
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
            var xMovDir = Randomizer.GetPositiveOrNegative();
            var firstPos = new Vector3(objectToMove.transform.position.x + screenWidth * Randomizer.GetNumberInRange(0.125f, 0.145f) * xMovDir,
                startingPos.y + screenFullHeight * Randomizer.GetNumberInRange(0.01f, 0.015f), 0);
            var secondPos = new Vector3(firstPos.x + screenWidth * Randomizer.GetNumberInRange(0.155f, 0.17f) * -xMovDir,
                startingPos.y + screenFullHeight * Randomizer.GetNumberInRange(0.005f, 0.01f), 0);
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