using UnityEngine;
using System.Collections;

public class Crosshair : MonoBehaviour
{
    public GUIStyle style;
    public Color color;

    // Use this for initialization
    void Start()
    {
        //No cursor on game.
        Screen.lockCursor = true;
    }

    void OnGUI()
    {
        style.normal.textColor = color;
        //Makes sure the crosshair is in the middle of the screen. + custom color
        GUI.Label(new Rect(Screen.width / 2, Screen.height / 2, 200, 200), "X", style);﻿
    }
}
