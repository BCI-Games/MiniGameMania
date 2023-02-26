using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MotorImageryButtonSPO : SPO
{
    [Header("Button SPO settings")]
    public Vector2 buttonOffset = new Vector2(-25, -25);
    public Color shadowColour;
    public float onScale = 1.15f;

    [Header("references")]
    public GameObject targetIndicator;
    public MotorImageryButtonSPO otherSPO;

    Vector2 startPosition;
    Image image;
    Shadow shadow;
    RectTransform rect;

    int trainTarget;
    int trainQouta;

    void Start()
    {
        image = GetComponent<Image>();
        rect = image.rectTransform;

        shadow = gameObject.AddComponent<Shadow>();
        shadow.effectColor = shadowColour;

        MotorImagerySettings settings = SettingsManager.GetCategory<MotorImagerySettings>();
        if(settings)
            trainTarget = settings.trainTarget;

        startPosition = rect.anchoredPosition;
    }

    // Turn the stimulus on
    public override float TurnOn()
    {
        base.TurnOn();

        image.color = onColour;
        print("turning spo on with index " + myIndex);
        return Time.time;
    }

    // Turn off/reset the SPO
    public override void TurnOff()
    {
        image.color = offColour;
        print("turning off spo with index " + myIndex);
    }

    //What to do on selection
    public override void OnSelection()
    {
        print($"spo with index {myIndex} selected");
        image.color = onColour;
        otherSPO.OnOtherSelected();

        if(trainQouta > 0)
        {
            trainQouta--;
            SetOffset((float)trainQouta / trainTarget);
        }
    }

    public void OnOtherSelected()
    {
        image.color = offColour;
    }

    // What to do when targeted for training selection

    public override void OnTrainTarget()
    {
        otherSPO.OnOtherTrainTarget();
        targetIndicator.SetActive(true);
        image.color = onColour;
        trainQouta = trainTarget;
        SetOffset(1);
    }

    public void OnOtherTrainTarget()
    {
        targetIndicator.SetActive(false);
        image.color = offColour;
    }

    public override void OffTrainTarget() { }

    void SetOffset(float lerpValue)
    {
        float scale = Mathf.LerpUnclamped(1, onScale, lerpValue);
        rect.localScale = Vector3.one * scale;

        Vector2 offset = buttonOffset * lerpValue;
        rect.anchoredPosition = startPosition + offset;
        shadow.effectDistance = -offset / scale;

        image.color = Color.Lerp(offColour, onColour, lerpValue);
    }
}
