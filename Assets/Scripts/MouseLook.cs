using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace RomanTristan.Lab3
{
    public class MouseLook : MonoBehaviour
    {
        [SerializeField] private float sensitivity = 8f;

        [SerializeField] private Transform playerCamera;
        [SerializeField] private float xClamp = 85f;

        float mouseX, mouseY;
        float xRotation = 0f;

        private void Start()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Update()
        {
            xRotation += mouseX;
            //xRotation = Mathf.Clamp(xRotation, -xClamp, xClamp);

            transform.localRotation = Quaternion.Euler(0f, xRotation, 0f);
            playerCamera.Rotate(Vector3.up * mouseX);
        }
        public void Input(Vector2 mouseInput)
        {
            mouseX = mouseInput.x * sensitivity * Time.deltaTime;
            mouseY = mouseInput.y * sensitivity * Time.deltaTime;
        }
    }
}

