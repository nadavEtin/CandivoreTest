using DG.Tweening;
using System;
using UnityEngine;

namespace Assets.Scripts.Utility
{
    public static class AnimationManager
    {
        public static void PinataHitMovement(GameObject objectToMove, Vector3 startingPos, Action animationEndCb)
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
            TweenCallback tweenCallback = new TweenCallback(animationEndCb);
            Sequence movementSeq = DOTween.Sequence();
            movementSeq.Append(objectToMove.transform.DOMove(firstPos, 0.3f).SetEase(Ease.OutSine))
                .Append(objectToMove.transform.DOMove(secondPos, 0.3f).SetEase(Ease.InQuad))
                .Append(objectToMove.transform.DOMove(thirdPos, 0.2f).SetEase(Ease.InSine))
                .Append(objectToMove.transform.DOMove(startingPos, 0.17f).SetEase(Ease.InSine))
                .AppendCallback(tweenCallback);
            movementSeq.Play();
        }

        public static void SwitchObjects(GameObject currentObj, GameObject nextObj)
        {

        }
    }
}