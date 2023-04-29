using Managers;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

namespace Actors.Player
{
    public class PlayerInputGetter : MonoBehaviour
    {
        [SerializeField] private PlayerInput playerInput;
        [SerializeField] private FixedJoystick screenJoystick;
        [SerializeField] private float horizontalAxis;
        [SerializeField] private Vector2 movementAxis;
        [SerializeField] private bool controlByScreen;
        [SerializeField] private bool jumpButton;
        [SerializeField] private bool jumpButtonDown;
        [SerializeField] private UserSettings playerSettings;

        private void Start()
        {
            controlByScreen = ScreenControlsManager.Instance.ControlByScreen;
        }

        private void Update()
        {
            if (controlByScreen)
            {
                if (playerSettings.Control == 1) horizontalAxis = screenJoystick.Horizontal;
            }
        }

        public void GetJumpInput(CallbackContext context)
        {
            jumpButton = context.ReadValueAsButton();
        }

        public void GetMovementInput(CallbackContext context)
        {
            movementAxis = context.ReadValue<Vector2>();
            horizontalAxis = movementAxis.x;
        }

        public bool GetJumpButton()
        {
            return jumpButton;
        }

        public bool GetJumpButtonDown()
        {
            if (playerInput.actions["Jump"].WasPressedThisFrame()) return true;
            else return false;
        }

        public bool GetJumpButtonUp()
        {
            if (playerInput.actions["Jump"].WasReleasedThisFrame()) return true;
            else return false;
        }
        public float GetHorizontalAxis()
        {
            return horizontalAxis;
        }
    }
}