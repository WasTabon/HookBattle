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

        private Vector3 _targetPosition;
        private float _throwTime = 1f;
        private float _returnTime = 1f;

        private float _startTime;
        private bool _isThrowing = false;

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
            float elapsedTime = Time.time - _startTime;
            float progress;

            if (elapsedTime <= _throwTime)
            {
                progress = elapsedTime / _throwTime;
                _hook.transform.position = Vector3.Lerp(_hand.position, _targetPosition, progress);
            }
            else if (elapsedTime <= _throwTime + _returnTime)
            {
                progress = (elapsedTime - _throwTime) / _returnTime;
                _hook.transform.position = Vector3.Lerp(_targetPosition, _hand.position, progress);
            }
            else 
            {
                _isThrowing = false;
                _lineRenderer.positionCount = 0; 
            }
        
            if (_lineRenderer.enabled) 
            {
                _lineRenderer.positionCount = 2;
                _lineRenderer.SetPosition(0, _hand.position);
                _lineRenderer.SetPosition(1, _hook.transform.position);
            }
        }
    }
}
