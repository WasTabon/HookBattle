using Code.System;
using UnityEngine;

namespace Code.Player
{
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimationController : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        public GameObject a;
        
        private const string MovementState = "IsMoving";

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            SetMovementAnimation();

            if (Input.GetMouseButtonDown(0))
            {
               a.transform.position = InputManager.Instance.TouchPosition();
            }
        }

        private void SetMovementAnimation()
        {
            if (InputManager.Instance.IsInput)
                _animator.SetBool(MovementState, true);
            else
                _animator.SetBool(MovementState, false);
        }
    }
}
