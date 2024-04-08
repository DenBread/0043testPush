using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace LuckyJet
{
    public class ItemMoveSystem : MonoBehaviour
    {
        public Action<bool, Vector3> OnMove;
        private RotationSystem _volchek;
        private Vector3 _volchekPos;
        private Rigidbody _rb;
        private float _speedMove = 1f;
        private bool _isMove;
        
        [Inject]
        private void Init()
        {
            Debug.Log("Add");
            _rb = GetComponent<Rigidbody>();
            OnMove += SetMovement;
        }

        private void FixedUpdate()
        {
            if (_isMove == true)
            {
                Debug.Log("Двигается " + name);
                _rb.MovePosition(Vector3.Lerp(transform.position, _volchekPos, _speedMove * Time.deltaTime));
            }
        }

        private void SetMovement(bool isMove, Vector3 posVolckek)
        {
            _isMove = isMove;
            _volchekPos = posVolckek;
        }
    }
}
