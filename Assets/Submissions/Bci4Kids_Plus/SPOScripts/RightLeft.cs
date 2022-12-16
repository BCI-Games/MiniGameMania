using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightLeft : SPO
{
    public void Awake()
    {
        GameObject player = GameObject.Find("Player");
    }
    // Start is called before the first frame update
    public override float TurnOn()
    {
        // Make Big

        // Don't touch this
        // Return time since stim
        return Time.time;

    }

    public override void TurnOff()
    {
        // Make Small

    }

    public override void OnSelection()
    {
        Debug.Log("I am moving");
        // Blow Up
        // If transform.gameObject.name == "left" then move left
        // move player left




        // If transform.gameObject.name == "right" then move right
        // move player right
    }
}
