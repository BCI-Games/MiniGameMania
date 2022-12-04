using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuLookSPO : SPO
{
    public GameObject BG;
    public GameObject HaveYouFound;
    public GameObject timer;
    public override void OnSelection()
    {
        StartCoroutine(QuickFlash());
        TurnOff();
        BG.SetActive(true);
        HaveYouFound.SetActive(false);
        timer.SetActive(true);
    }
}
