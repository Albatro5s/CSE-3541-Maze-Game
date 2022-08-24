using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RomanTristan.Lab3
{
    class FirstPersonCamera : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private Vector3 offset;
        private void Update()
        {
            transform.position = target.position + offset;
            // ...
        }
    }
}
