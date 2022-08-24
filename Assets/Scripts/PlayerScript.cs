using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public int points = 0;
    public string gameOverText;
    private void OnGUI()
    {
        GUI.Label(new Rect(20, 20, 100, 20), "Score: " + points);
        GUI.Label(new Rect(20, 40, 1000, 20), gameOverText);
    }
}
