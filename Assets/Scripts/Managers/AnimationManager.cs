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

        public Dictionary<ObjectTypes, ObjectTypes> PinataPrizeParticles { private set; get; }

        public AnimationManager()
        {
            _animParams = Resources.Load<AnimationParameters>("AnimationParams");

            PinataPrizeParticles = new Dictionary<ObjectTypes, ObjectTypes> { { ObjectTypes.BombPrize, ObjectTypes.BombParticle },
                { ObjectTypes.BoosterPrize, ObjectTypes.BoosterParticle }, { ObjectTypes.CrystalPrize, ObjectTypes.CrystalParticle },
                { ObjectTypes.HeartPrize, ObjectTypes.HeartParticle}, { ObjectTypes.LightningPrize, ObjectTypes.LightningParticle },
                { ObjectTypes.StickerPrize, ObjectTypes.StickerParticle }, { ObjectTypes.ConfettiParticle, ObjectTypes.ConfettiParticle } };
        }

        public void PinataHitMovement(GameObject objectToMove, GameObject spriteOff, GameObject spriteOn, Vector3 startingPos, Action animationEndCb)
        {
            /*var screenHeight = Camera.main.orthographicSize * 2;
            var screenWidth = Camera.main.orthographicSize * Camera.main.aspect;
            var anotherScreenW = GeneralData.ScreenWidth;
            var xMovDir = Randomizer.GetPositiveOrNegative();
            var firstPos = new Vector3(objectToMove.transform.position.x + screenWidth * 
                Randomizer.GetNumberInRange(_animParams.firstPosWidth.minVal, _animParams.firstPosWidth.maxVal) * xMovDir,
                startingPos.y + screenHeight * Randomizer.GetNumberInRange(_animParams.firstPosHeight.minVal, _animParams.firstPosHeight.maxVal), 0);

            var secondPos = new Vector3(firstPos.x + screenWidth * 
                Randomizer.GetNumberInRange(_animParams.secondPosWidth.minVal, _animParams.secondPosWidth.maxVal) * -xMovDir,
                startingPos.y - screenHeight * Randomizer.GetNumberInRange(_animParams.secondPosHeight.minVal, _animParams.secondPosHeight.maxVal), 0);

            var thirdPos = new Vector3(secondPos.x + Randomizer.GetNumberInRange(_animParams.thirdPosWidth.minVal, _animParams.thirdPosWidth.maxVal) * -xMovDir,
                startingPos.y + screenHeight * Randomizer.GetNumberInRange(_animParams.thirdPosHeight.minVal, _animParams.thirdPosHeight.maxVal), 0);

            TweenCallback tweenCallback = new(animationEndCb);
            Sequence movementSeq = DOTween.Sequence();
            movementSeq.Append(objectToMove.transform.DOJump(firstPos, _animParams.firstPosJumpP, 1, _animParams.firstPosTime).SetEase(Ease.OutSine))
                .AppendCallback(() => SwitchObjects(spriteOff, spriteOn))
                .Append(objectToMove.transform.DOJump(secondPos, _animParams.secondPosJumpP, 1, _animParams.secondPosTime))
                .Append(objectToMove.transform.DOMove(startingPos, _animParams.thirdPosTime).SetEase(Ease.InSine))
                .AppendCallback(tweenCallback);*/


            /*movementSeq.Append(objectToMove.transform.DOMove(firstPos, _animParams.firstPosTime).SetEase(Ease.OutSine))
                .Append(objectToMove.transform.DOMove(secondPos, _animParams.secondPosTime).SetEase(Ease.InQuad))
                .AppendCallback(()=>SwitchObjects(spriteOff, spriteOn))
                .Append(objectToMove.transform.DOMove(thirdPos, _animParams.thirdPosTime).SetEase(Ease.InSine))
                .Append(objectToMove.transform.DOMove(startingPos, _animParams.fourthPosTime).SetEase(Ease.InSine))
                .AppendCallback(tweenCallback);*/
            TallBounce(objectToMove, spriteOff, spriteOn, startingPos, animationEndCb).Play();
            //movementSeq.Play();
        }

        public void MoveParticlesToShelf(Transform obj, Vector3 destination, Action<SpriteRenderer> animationEndCb, SpriteRenderer cbTarget)
        {
            TweenCallback<SpriteRenderer> tweenCallback = new(animationEndCb);
            Sequence movementSeq = DOTween.Sequence();
            movementSeq.AppendInterval(Randomizer.GetNumberInRange(1.1f, 1.3f))
                .Append(obj.DOMove(destination, 0.25f))
                .AppendCallback(() => animationEndCb(cbTarget));
            movementSeq.Play();
        }

        public void MoveParticlesToShelf(Transform obj, Vector3 destination)
        {
            //TweenCallback<SpriteRenderer> tweenCallback = new(animationEndCb);
            Sequence movementSeq = DOTween.Sequence();
            movementSeq.AppendInterval(Randomizer.GetNumberInRange(1.1f, 1.3f))
                .Append(obj.DOMove(destination, 0.25f));
                //.AppendCallback(() => animationEndCb(cbTarget));
            movementSeq.Play();
        }

        public void FadeIn(SpriteRenderer spriteRenderer)
        {
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0);
            spriteRenderer.DOFade(1, 0.3f).Play();
        }

        public void FadeOut(SpriteRenderer spriteRenderer)
        {
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 255);
            spriteRenderer.DOFade(0, 0.3f).Play();
        }

        private void SwitchObjects(GameObject turnOff, GameObject turnOn)
        {
            turnOff.SetActive(false);
            turnOn.SetActive(true);
        }


        #region Pinata movement versions

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
            movementSeq.Append(objectToMove.transform.DOJump(firstPos, 0.7f, 1, 0.3f).SetEase(Ease.OutSine))
                .AppendCallback(() => SwitchObjects(spriteOff, spriteOn))
                .Append(objectToMove.transform.DOJump(secondPos, 0.35f, 1, 0.2f))
                .Append(objectToMove.transform.DOMove(startingPos, 0.25f).SetEase(Ease.InSine))
                .AppendCallback(tweenCallback);
            return movementSeq;
        }

        private void WideBounce()
        {

        }

        #endregion
    }
}