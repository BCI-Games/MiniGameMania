using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrongSPO : SPO
{
    public GameObject SelectOption;
    public GameObject GamePoint;
    public GameObject Timer;
    public override void OnSelection()
    {

        StartCoroutine(QuickFlash());
        TurnOff();
        SelectOption.SetActive(false);
        GamePoint.SetActive(true);
    }
}
