using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameplayObjects
{
    public class Pinata : MonoBehaviour, IPinata
    {
        [SerializeField] private Transform _idle, _hit;
        [SerializeField] private Transform _head, _front, _tail;

        [SerializeField] private List<ColliderScript> _childColliders;

        private void Start()
        {
            for (int i = 0; i < _childColliders.Count; i++)
            {
                _childColliders[i].init(PinataClick);
            }
        }

        private void PinataClick()
        {
            
        }
    }
}