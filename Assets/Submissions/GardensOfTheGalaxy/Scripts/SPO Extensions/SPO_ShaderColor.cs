using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPO_ShaderColor : SPO
{
    private Color originalColor;

    private void Start()
    {
        originalColor = GetComponent<Renderer>().material.GetColor("_MainColor");
    }

    // Turn off/reset the SPO
    public override void TurnOff()
    {
        this.GetComponent<Renderer>().material.SetColor("_MainColor", originalColor);
    }
}
