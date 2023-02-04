using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Base class for the Stimulus Presenting Objects (SPOs)

public class SPO_Event : SPO
{
    public UnityEvent unityEvent;

    // What to do on selection
    public override void OnSelection()
    {
        // This is free form, do whatever you want on selection

        unityEvent.Invoke();

        StartCoroutine(QuickFlash());

        // Reset
        TurnOff();
    }
}
