using Code.System;
using UnityEngine;

namespace Code.Player
{
    [RequireComponent(typeof(LineRenderer))]
    public class ThrowHook : MonoBehaviour
    {
        [SerializeField] private LineRenderer _lineRenderer;
        [SerializeField] private GameObject _hook;
        [SerializeField] private Transform _hand;

        [SerializeField] private float _throwForce;
        
        private Vector3 _targetPosition;
        private Vector3 _startPos;

        private float _startTime;
        private bool _isThrowing = false;
        private bool _reachedTarget = false;
        private bool _reachedStartPos = false;

        private void Start()
        {
            _lineRenderer = GetComponent<LineRenderer>();
        }

        private bool _isTap;
        private float _tapTimeThreshold = 0.1f;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _isTap = true;
                _startTime = Time.time;
            }

            if (_isTap && Input.GetMouseButtonUp(0))
            {
                if (Time.time - _startTime < _tapTimeThreshold)
                {
                    _targetPosition = InputManager.Instance.TouchPosition();
                    _startTime = Time.time;
                    _isThrowing = true;
                    _startPos = transform.position;

                    _targetPosition.y = 1f;
                    _startPos.y = 1f;
                }
                _isTap = false;
            }

            if (_isThrowing)
            {
                UpdateHookPosition();
            }
        }

        private void UpdateHookPosition()
        {
            _lineRenderer.positionCount = 2;
            
            _lineRenderer.SetPosition(0, _hand.position);
            _lineRenderer.SetPosition(1, _hook.transform.position);

            if (!_reachedTarget)
            {
                Vector3 throwDirection = (_targetPosition - _hook.transform.position).normalized;
                _hook.transform.Translate(throwDirection * _throwForce * Time.deltaTime, Space.World);

                if (Vector3.Distance(_hook.transform.position, _targetPosition) < 0.1f)
                    _reachedTarget = true;
            }
            else if (_reachedTarget && !_reachedStartPos)
            {
                Vector3 returnDirection = (_startPos - _hook.transform.position).normalized;
                _hook.transform.Translate(returnDirection * _throwForce * Time.deltaTime, Space.World);

                if (Vector3.Distance(_hook.transform.position, _startPos) < 0.1f)
                    _reachedStartPos = true;
            }

            if (_reachedStartPos)
            {
                SetBoolsOff();
            }
        }

        private void SetBoolsOff()
        {
            _reachedStartPos = false;
            _reachedTarget = false;
            _lineRenderer.positionCount = 0;
            _isThrowing = false;
            
            Debug.Log(_reachedStartPos);
            Debug.Log(_isThrowing);
            Debug.Log(_reachedTarget);
        }
    }
}
