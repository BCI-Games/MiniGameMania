using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BCIEssentials.Controllers;

public class BouncingP300SPO : SignalSPO
{
    [Header("Bounce Settings")]
    public Vector2 bounceOffset = new Vector2(-25, -25);
    public AnimationCurve bounceCurve;
    public Color shadowColour;
    public float onScale = 1.15f;

    Vector2 startPosition;
    Image image;
    Shadow shadow;
    RectTransform rect;

    float bounceLength;
    float bounceTimer;
    bool on;


    [Header("Selected Flash Settings")]
    public float selectedScale = 1.25f;
    public float selectedFlashCount = 3;
    public float selectedFlashOnTime = 0.15f;
    public float selectedFlashOffTime = 0.2f;

    private void Start()
    {
        image = GetComponent<Image>();
        rect = image.rectTransform;

        shadow = gameObject.AddComponent<Shadow>();
        shadow.effectColor = shadowColour;

        P300Settings settings;
        settings = SettingsManager.GetCategory<P300Settings>();

        if (settings)
            bounceLength = settings.flashOnTime;

        TurnOff();
    }

    private void Update()
    {
        if (on)
        {
            bounceTimer += Time.deltaTime;

            float bounceValue = bounceCurve.Evaluate(bounceTimer / bounceLength);

            float scale = Mathf.LerpUnclamped(1, onScale, bounceValue);
            rect.localScale = Vector3.one * scale;

            Vector2 offset = bounceOffset * bounceValue;
            rect.anchoredPosition = startPosition + offset;
            shadow.effectDistance = -offset / scale;
        }
    }

    // Turn the stimulus on
    public override float TurnOn()
    {
        base.TurnOn();

        on = true;
        image.color = onColour;
        bounceTimer = 0;

        return Time.time;
    }

    // Turn off/reset the SPO
    public override void TurnOff()
    {
        on = false;

        image.color = offColour;
        rect.anchoredPosition = startPosition;
        shadow.effectDistance = Vector2.zero;
        rect.localScale = Vector3.one;
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
