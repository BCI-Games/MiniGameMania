using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Submissions.StudioSomething;

public class StationSPO : SPO
{
    public GameObject LetterCanvas;
    public TMP_Text Label;

    public GameObject Zone;



    // Start is called before the first frame update
    public override float TurnOn()
    {
        // Make Big
        gameObject.GetComponent<Renderer>().material.color = onColour;


        // Don't touch this
        // Return time since stim
        return Time.time;

    }

    public override void TurnOff()
    {
        // Make Big
        gameObject.GetComponent<Renderer>().material.color = offColour;


    }

    public override void OnSelection()
    {
        BCIManagerSub.Instance.BCICoroutine = BCIManagerSub.Instance.BCICoFunction();
        StartCoroutine(BCIManagerSub.Instance.BCICoroutine);
        PlayerController.BCI.MoveCharacter(Zone.transform.position);
        StationManager.Instance.RemoveOutlines();
        GetComponent<Outline>().enabled = true;
    }


}
