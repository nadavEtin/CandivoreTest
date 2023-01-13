using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameplayObjects
{
    public class Rope : MonoBehaviour
    {
        [SerializeField] private Transform _pinataAnchorPoint;

        void Update()
        {
            transform.position = _pinataAnchorPoint.position;
        }
    }
}

