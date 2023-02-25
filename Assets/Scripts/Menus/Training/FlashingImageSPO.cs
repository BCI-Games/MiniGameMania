using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashingImageSPO : SignalSPO
{
    Image image;

    [Header("Selected Flash Settings")]
    public float selectedScale = 1.25f;
    public float selectedFlashCount = 3;
    public float selectedFlashOnTime = 0.15f;
    public float selectedFlashOffTime = 0.2f;

    void Start()
    {
        image = GetComponent<Image>();
    }

    // Turn the stimulus on
    public override float TurnOn()
    {
        base.TurnOn();

        image.color = onColour;
        return Time.time;
    }

    // Turn off/reset the SPO
    public override void TurnOff()
    {
        image.color = offColour;
    }

    // What to do on selection
    public override void OnSelection()
    {
        base.OnSelection();

        StartCoroutine(QuickFlash());
    }

    public new IEnumerator QuickFlash()
    {
        for (int i = 0; i < selectedFlashCount; i++)
        {
            image.color = onColour;
            transform.localScale = Vector3.one * selectedScale;
            yield return new WaitForSecondsRealtime(selectedFlashOnTime);
            image.color = offColour;
            transform.localScale = Vector3.one;
            yield return new WaitForSecondsRealtime(selectedFlashOffTime);
        }
    }

    // What to do when targeted for training selection
    public override void OnTrainTarget()
    {
        base.OnTrainTarget();

        image.color = onColour;
    }

    // What to do when untargeted
    public override void OffTrainTarget()
    {
        image.color = offColour;
    }
}
