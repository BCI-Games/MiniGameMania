using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Base class for the Stimulus Presenting Objects (SPOs)

public class SPO_Button : SPO
{
    public float delay = 0;
    // What to do on selection
    public override void OnSelection()
    {
        // This is free form, do whatever you want on selection

        Invoke("OnSelectEvent", delay);

        StartCoroutine(ButtonQuickFlash());

        // Reset
        TurnOff();
    }
    private void OnSelectEvent()
    {
        GetComponent<Button>().onClick.Invoke();
    }

    // Turn the stimulus on
    public override float TurnOn()
    {
        ColorBlock colorBlock = GetComponent<Button>().colors;
        colorBlock.normalColor = onColour;
        GetComponent<Button>().colors = colorBlock;

        //Return time since stim
        return Time.time;
    }

    // Turn off/reset the SPO
    public override void TurnOff()
    {
        ColorBlock colorBlock = GetComponent<Button>().colors;
        colorBlock.normalColor = offColour;
        GetComponent<Button>().colors = colorBlock;
    }


    // Quick Flash
    public IEnumerator ButtonQuickFlash()
    {
        for (int i = 0; i < 3; i++)
        {
            TurnOn();
            yield return new WaitForSecondsRealtime(0.2F);
            TurnOff();
            yield return new WaitForSecondsRealtime(0.2F);
        }
    }
}
