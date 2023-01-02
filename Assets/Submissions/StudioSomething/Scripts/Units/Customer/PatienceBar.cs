using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PatienceBar : MonoBehaviour
{
    public Slider SliderUI;
    public Gradient Gradient;
    public Image Fill;

    public void SetMaxPatience(float maxValue)
    {
        SliderUI.maxValue = maxValue;
        SliderUI.value = maxValue;
        Fill.color = Gradient.Evaluate(1);
    }

    public void SetPatience(float patience)
    {
        SliderUI.value = patience;
        Fill.color = Gradient.Evaluate(SliderUI.normalizedValue);
    }
}
