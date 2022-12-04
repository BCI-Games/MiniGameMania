using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorrectSPO : SPO
{
    public GameObject Win;
    public override float TurnOn()
    {
        this.GetComponent<SpriteRenderer>().color = onColour; 

        return Time.time;
    }

        // Turn off/reset the SPO
    public override void TurnOff()
    {
        //This is just for an object renderer (e.g. 3D object). Use <SpriteRenderer> for 2D
        { this.GetComponent<SpriteRenderer>().color = offColour; }
    }
    public override void OnSelection()
    {
        StartCoroutine(QuickFlash());
        TurnOff();
        Win.SetActive(true);
    }
}


