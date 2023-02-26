using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SignalSPO : SPO
{
    [Header("references")]
    public GameObject targetIndicator;

    Action<SPO> onSelection;
    Action<SPO> onTrainTarget;
    Action<SPO> onTurnedOn;

    public void SetEvents(Action<SPO> selection, Action<SPO> trainTarget, Action<SPO> turnedOn)
    {
        onSelection = selection;
        onTrainTarget = trainTarget;
        onTurnedOn = turnedOn;

        targetIndicator.SetActive(false);
    }

    public override void OnSelection()
    {
        onSelection(this);
    }

    public override void OnTrainTarget()
    {
        onTrainTarget(this);
        targetIndicator.SetActive(true);
    }

    public void DisableTargetIndicator() => targetIndicator.SetActive(false);

    public override float TurnOn()
    {
        onTurnedOn(this);
        return Time.time;
    }
}
