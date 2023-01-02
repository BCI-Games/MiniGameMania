using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ProcessBar : MonoBehaviour
{
    public Slider SliderUI;
    public Image Fill;

    public void SetMaxProcess(float maxValue)
    {
        SliderUI.maxValue = maxValue;
        SliderUI.value = maxValue;

    }

    public void SetProcess(float process)
    {
        SliderUI.value = process;
    }
}
