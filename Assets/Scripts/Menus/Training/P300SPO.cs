using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class P300SPO : SPO
{
    public GameObject targetIndicator;
    Image image;

    public float selectedScale = 1.25f;
    public float selectedFlashCount = 3;
    public float selectedFlashOnTime = 0.15f;
    public float selectedFlashOffTime = 0.2f;

    private void Start()
    {
        image = GetComponent<Image>();
        targetIndicator.SetActive(false);
        TurnOff();
    }

    // Turn the stimulus on
    public override float TurnOn()
    {
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
        StartCoroutine(QuickFlash());
    }

    public new IEnumerator QuickFlash()
    {
        for (int i = 0; i < selectedFlashCount; i++)
        {
            TurnOn();
            transform.localScale = Vector3.one * selectedScale;
            yield return new WaitForSecondsRealtime(selectedFlashOnTime);
            TurnOff();
            transform.localScale = Vector3.one;
            yield return new WaitForSecondsRealtime(selectedFlashOffTime);
        }
    }

    // What to do when targeted for training selection
    public override void OnTrainTarget()
    {
        TurnOn();
        targetIndicator.SetActive(true);

        //float scaleValue = 1.4f;
        //Vector3 objectScale = transform.localScale;
        //transform.localScale = new Vector3(objectScale.x * scaleValue, objectScale.y * scaleValue, objectScale.z * scaleValue);
    }

    // What to do when untargeted
    public override void OffTrainTarget()
    {
        TurnOff();
        targetIndicator.SetActive(false);

        //float scaleValue = 1.4f;
        //Vector3 objectScale = transform.localScale;
        //transform.localScale = new Vector3(objectScale.x / scaleValue, objectScale.y / scaleValue, objectScale.z / scaleValue);
    }
}
