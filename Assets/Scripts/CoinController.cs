using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RomanTristan.Lab3
{
    public class CoinController : MonoBehaviour
    {
        public int rotateSpeed;

        public float intensity;

        public int coins;

        private void OnTriggerEnter(Collider other)
        {
            if (other.name == "Player")
            {
                other.GetComponent<PlayerScript>().points++;
                Destroy(gameObject);
            }
        }

        private void Update()
        {
            transform.Rotate(0, rotateSpeed, 0, Space.World);
            transform.position += Vector3.up * Mathf.Cos(Time.time) * .002f;
        }
    }
}
