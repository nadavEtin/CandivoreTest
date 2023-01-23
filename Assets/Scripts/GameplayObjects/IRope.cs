using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts
{
    public interface IRope
    {
        void Init(Transform anchorPoint, IAnimationManager animManager);
    }
}
