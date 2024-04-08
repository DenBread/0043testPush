using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace LuckyJet
{
    public class RotationSystem : MonoBehaviour
    {
        public float _timerMove;
        public float _saveTimerMove;
        public float _speed;
        private Rigidbody _rb;
        private Vector3 _savePos;
        [SerializeField] private PingPongUI _pingPongUI;

        private DefeatUI _defeatUI;

        private List<ItemTriggerSystem> _itemTriggerSystems;
        private bool _isMove;

        [Inject]
        private void Init(DefeatUI defeatUI, ListItemTrigger itemTriggerSystems)
        {
            _itemTriggerSystems = itemTriggerSystems.ListTrigge;
            _defeatUI = defeatUI;
            _saveTimerMove = _timerMove;
            
            _savePos = transform.position;
            _rb = GetComponent<Rigidbody>();
            _timerMove = _speed / _timerMove;

            transform.DOLocalRotate(new Vector3(0, 30f, 0), 0.25f, RotateMode.Fast)
                .SetLoops(-1, LoopType.Yoyo);
        }

        private void FixedUpdate()
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                Push();
            }
            
            if(_isMove == true)
                _rb.angularVelocity = new Vector3(0,  _speed * 90 * Time.fixedDeltaTime, 0);
            
            
        }

        public void Push()
        {
            transform.DOKill();
            _isMove = true;
            var power = _pingPongUI.GetSpeed();
            Debug.Log("Power: " + power);
            _rb.AddForce(transform.forward * power * 10, ForceMode.Impulse);
            StartCoroutine(Timer());
        }

        private IEnumerator Timer()
        {
            while (_speed > 0 && Application.isPlaying)
            {
                yield return new WaitForSeconds(1f);
                //Debug.Log(_speed);
                _speed-=_timerMove;
            }
            
            _isMove = false;
            _speed = 0;
            _rb.constraints = RigidbodyConstraints.None;
            _rb.useGravity = true;

            foreach (var item in _itemTriggerSystems)
            {
                item.OnDisable?.Invoke(false);
            }

            yield return new WaitForSeconds(3f);
            _defeatUI.gameObject.SetActive(true);
            yield break;
        }

        public void ResetPostion()
        {
            
            transform.position = _savePos;
            transform.eulerAngles = Vector3.zero;
            _speed = 100;
            _rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            _rb.useGravity = false;
            _pingPongUI.enabled = true;
            _timerMove = _speed / _saveTimerMove;
            transform.DOLocalRotate(new Vector3(0, 30f, 0), 0.25f, RotateMode.Fast)
                .SetLoops(-1, LoopType.Yoyo);
        }
    }
}
