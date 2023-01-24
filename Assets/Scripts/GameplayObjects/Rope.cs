using Assets.Scripts.Managers;
using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.GameplayObjects
{
    public class Rope : MonoBehaviour, IRope
    {
        [SerializeField] private SpriteRenderer _sprite;
        private Transform _pinataAnchorPoint;        
        private IAnimationManager _animationManager;

        public void Init(Transform anchorPoint, IAnimationManager animManager)
        {
            _animationManager = animManager;
            _pinataAnchorPoint = anchorPoint;
            transform.position = _pinataAnchorPoint.position;
        }

        private void Awake()
        {
            EventBus.Subscribe(GameplayEvent.GameEnd, GameEnd);
        }

        private void Update()
        {
            transform.position = _pinataAnchorPoint.position;
        }

        private void GameEnd(BaseEventParams eventParams)
        {
            _animationManager.FadeOut(_sprite, 0.3f);
        }

        private void OnDestroy()
        {
            EventBus.Unsubscribe(GameplayEvent.GameEnd, GameEnd);
        }
    }
}

