using System;
using UnityEngine;

namespace Assets.Scripts.GameplayObjects
{
    public class ColliderScript : MonoBehaviour, IColliderScript
    {
        private Action<float> _onClickCb;
        private bool _checkLongClick;
        private float _timeClickHeld;
    
        public void init(Action<float> onclickCallback)
        {
            _onClickCb = onclickCallback;
        }
    
        private void OnMouseDown()
        {
            _timeClickHeld = 0;
            _checkLongClick = true;
        }

        private void Update()
        {
            if (_checkLongClick)
            {
                _timeClickHeld += Time.deltaTime;
                if (Input.GetKeyUp(KeyCode.Mouse0))
                {
                    _onClickCb?.Invoke(_timeClickHeld);
                    _checkLongClick = false;
                }
            }
        }
    }
}


