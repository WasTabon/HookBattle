using Code.System;
using UnityEngine;

namespace Code.Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class JoystickMovement : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _rotateSpeed = 10f;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            Move();
            Rotate();
        }

        private void Move()
        {
            Vector3 velocity = _rigidbody.velocity;

            velocity.x = InputManager.Instance.HorizontalInput * _moveSpeed * Time.fixedDeltaTime;
            velocity.z = InputManager.Instance.VerticalInput * _moveSpeed * Time.fixedDeltaTime;

            _rigidbody.velocity = velocity;
        }

        private void Rotate()
        {
            if (InputManager.Instance.IsInput)
            {
                Vector3 dir = new Vector3(InputManager.Instance.HorizontalInput, 0, InputManager.Instance.VerticalInput);

                dir.Normalize();

                Quaternion targetRotation = Quaternion.LookRotation(dir);
                
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotateSpeed * Time.fixedDeltaTime);   
            }
        }
    }
}