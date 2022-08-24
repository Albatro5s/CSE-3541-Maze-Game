using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            Debug.Log("Exit found!");
            other.GetComponent<PlayerScript>().gameOverText = "EXIT DISCOVERED!! GAME OVER";
        }
    }
}
