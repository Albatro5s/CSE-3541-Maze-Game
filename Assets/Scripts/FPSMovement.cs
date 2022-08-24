using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RomanTristan.Lab3
{
    public class FPSMovement : MonoBehaviour
    {
        [SerializeField] private CharacterController controller;
        [SerializeField] private float speed = 11f;
        [SerializeField] private float gravity = -30f;

        [SerializeField] private float jumpHeight = 3.5f;
        bool jump;

        [SerializeField] private Transform groundCheck;
        [SerializeField] private float groundDistance = 0.4f;
        [SerializeField] private LayerMask groundMask;

        Vector2 horizontalInput;
        Vector3 velocityY;
        bool isGrounded;

        private void Update()
        {
            Vector3 move = (transform.right * horizontalInput.x + transform.forward * horizontalInput.y);
            
            controller.Move(move * speed * Time.deltaTime);

            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

            if (isGrounded)
            {
                velocityY.y = 0;
            }

            if (jump)
            {
                if (isGrounded)
                {
                    velocityY.y = Mathf.Sqrt(-2f * jumpHeight * gravity);
                }
                jump = false;
            }

            velocityY.y += gravity * Time.deltaTime;

            controller.Move(velocityY * Time.deltaTime);
        }
        public void Input(Vector2 _horizontalInput)
        {
            horizontalInput = _horizontalInput;
            //Debug.Log(horizontalInput);
        }
        public void OnJumpPressed()
        {
            jump = true;
        }

    }
}

