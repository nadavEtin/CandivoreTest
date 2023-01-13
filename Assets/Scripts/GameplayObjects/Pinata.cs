using System.Collections.Generic;
using UnityEngine;

namespace GameplayObjects
{
    public class Pinata : MonoBehaviour, IPinata
    {
        [SerializeField] private Transform _idle, _hit;
        [SerializeField] private Transform _head, _front, _tail;
    }
}