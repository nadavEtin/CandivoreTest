using Assets.Scripts.Utility;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameplayObjects
{
    public class Pinata : MonoBehaviour, IPinata
    {
        [SerializeField] private GameObject _idle, _idleCracked, _hit;
        [SerializeField] private GameObject _head, _front, _tail;

        private List<IColliderScript> _childColliders;
        private List<GameObject> _childObjects;
        private Vector3 _pinataStartingPos;

        private void Start()
        {
            _childColliders = new List<IColliderScript> { _idle.GetComponent<IColliderScript>(), _idleCracked.GetComponent<IColliderScript>() };
            _childObjects = new List<GameObject> { _idle, _idleCracked, _hit, _head, _front, _tail };
            for (int i = 0; i < _childColliders.Count; i++)
            {
                _childColliders[i].init(PinataClick);
            }
            EnableDisableChildObjects(false);
            _idle.SetActive(true);
            PositionPinata();
        }

        private void PositionPinata()
        {
            transform.position = new Vector3(0, 0 + GeneralData.HalfScreenHeight * 0.2f, 0);
            _pinataStartingPos = transform.position;
        }

        private void EnableDisableChildObjects(bool enable)
        {
            for (int i = 0; i < _childObjects.Count; i++)
            {
                _childObjects[i].SetActive(enable);
            }
        }

        private void PinataClick()
        {
            EnableDisableChildObjects(false);
            _hit.SetActive(true);
            AnimationManager.PinataHitMovement(gameObject, _pinataStartingPos, PinataMovementAnimEnd);
        }

        private void PinataMovementAnimEnd()
        {
            EnableDisableChildObjects(false);
            _idleCracked.SetActive(true);
        }
    }
}