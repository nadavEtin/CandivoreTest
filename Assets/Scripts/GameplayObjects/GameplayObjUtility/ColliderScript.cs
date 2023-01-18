using System;
using UnityEngine;

namespace Assets.Scripts.GameplayObjects
{
    public class ColliderScript : MonoBehaviour, IColliderScript
    {
        private Action _onClickCb;
    
        public void init(Action onclickCallback)
        {
            _onClickCb = onclickCallback;
        }
    
        private void OnMouseDown()
        {
            _onClickCb?.Invoke();
        }
    }
}


