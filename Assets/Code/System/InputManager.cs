using UnityEngine;

namespace Code.System
{
    public class InputManager : MonoBehaviour
    {
        public static InputManager Instance { get; private set; }
        
        [SerializeField] private FloatingJoystick _joystick;

        public float HorizontalInput => _horizontalInput;
        public float VerticalInput => _verticalInput;
        
        public bool IsInput => _isInput;

        private float _horizontalInput;
        private float _verticalInput;
        
        private bool _isInput;

        private void Awake()
        {
            SetSingleton();
        }

        private void Update()
        {
            SetJoystickInput();
            CheckInput();
        }

        private void SetJoystickInput()
        {
            _horizontalInput = _joystick.Horizontal;
            _verticalInput = _joystick.Vertical;
        }

        private void CheckInput()
        {
            if (_verticalInput != 0 || _horizontalInput != 0)
                _isInput = true;
            else if (_verticalInput == 0 && _horizontalInput == 0)
                _isInput = false;
        }

        private void SetSingleton()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                return;
            }
        
            Destroy(gameObject);
        }
    }
    
}
