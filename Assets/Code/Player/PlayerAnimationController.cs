using Code.System;
using UnityEngine;

namespace Code.Player
{
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimationController : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        private const string MovementState = "IsMoving";

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            SetMovementAnimation();
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
