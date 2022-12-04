using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorrectSPO : SPO
{
    public GameObject Win;
    public override void OnSelection()
    {
        StartCoroutine(QuickFlash());
        TurnOff();
        Win.SetActive(true);
    }
}


