using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPickSPO : SPO
{
    public GameObject selecOption;
    public GameObject GamePoint;
    public GameObject Timer;
    public override void OnSelection()
    {
        StartCoroutine(QuickFlash());
        TurnOff();
        Timer.SetActive(true);
        selecOption.SetActive(true);
        GamePoint.SetActive(false);
    }
}

