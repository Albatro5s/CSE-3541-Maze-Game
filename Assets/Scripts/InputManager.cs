using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RomanTristan.Lab3
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private FPSMovement movement;
        [SerializeField] private MouseLook mouseLook;

        private CSE3541Inputs inputScheme;
        private CSE3541Inputs.PlayerActions input;

        Vector2 horizontalInput;
        Vector2 mouseInput;

        private void Awake()
        {
            inputScheme = new CSE3541Inputs();

            inputScheme.Player.Move.performed += ctx => horizontalInput = ctx.ReadValue<Vector2>();

            inputScheme.Player.Move.canceled += ctx => horizontalInput = ctx.ReadValue<Vector2>();

            inputScheme.Player.Jump.performed += _ => movement.OnJumpPressed();

            inputScheme.Player.Look.performed += ctx => mouseInput = ctx.ReadValue<Vector2>();

            inputScheme.Player.Look.canceled += ctx => mouseInput = ctx.ReadValue<Vector2>();
        }

        private void Update()
        {
            movement.Input(horizontalInput);
            mouseLook.Input(mouseInput);
        }

        private void OnEnable()
        {
            inputScheme.Enable();
            var _ = new QuitHandler(inputScheme.Player.Quit);

        }

        private void OnDestroy()
        {
            inputScheme.Disable();
        }
    }
}

