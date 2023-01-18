using Assets.Scripts.Utility;
using DG.Tweening;
using System;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class AnimationManager : IAnimationManager
    {
        public void PinataHitMovement(GameObject objectToMove, GameObject spriteOff, GameObject spriteOn, Vector3 startingPos, Action animationEndCb)
        {
            var screenHeight = Camera.main.orthographicSize * 2;
            var screenWidth = Camera.main.orthographicSize * Camera.main.aspect;
            var xMovDir = Randomizer.GetPositiveOrNegative();
            var firstPos = new Vector3(objectToMove.transform.position.x + screenWidth * Randomizer.GetNumberInRange(0.025f, 0.035f) * xMovDir,
                objectToMove.transform.position.y + screenHeight * Randomizer.GetNumberInRange(0.05f, 0.07f), 0);
            var secondPos = new Vector3(firstPos.x + screenWidth * Randomizer.GetNumberInRange(0.005f, 0.015f) * xMovDir,
                startingPos.y - screenHeight * Randomizer.GetNumberInRange(0.005f, 0.015f), 0);
            var thirdPos = new Vector3(secondPos.x + Randomizer.GetNumberInRange(0.013f, 0.023f) * -xMovDir,
                startingPos.y + screenHeight * Randomizer.GetNumberInRange(0.009f, 0.015f), 0);
            TweenCallback tweenCallback = new(animationEndCb);
            Sequence movementSeq = DOTween.Sequence();
            movementSeq.Append(objectToMove.transform.DOMove(firstPos, 0.3f).SetEase(Ease.OutSine))
                .Append(objectToMove.transform.DOMove(secondPos, 0.3f).SetEase(Ease.InQuad))
                .AppendCallback(()=>SwitchObjects(spriteOff, spriteOn))
                .Append(objectToMove.transform.DOMove(thirdPos, 0.2f).SetEase(Ease.InSine))
                .Append(objectToMove.transform.DOMove(startingPos, 0.17f).SetEase(Ease.InSine))
                .AppendCallback(tweenCallback);
            movementSeq.Play();
        }

        private void SwitchObjects(GameObject turnOff, GameObject turnOn)
        {
            turnOff.SetActive(false);
            turnOn.SetActive(true);
        }


        #region Pinata movement versions

        private void TallBounce()
        {

        }

        private void WideBounce()
        {

        }

        #endregion
    }
}