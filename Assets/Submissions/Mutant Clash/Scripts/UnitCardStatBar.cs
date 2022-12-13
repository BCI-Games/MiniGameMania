using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitCardStatBar : MonoBehaviour
{
    Image bar;

    public void SetValue(int amount)
    {
        if (!bar)
            bar = GetComponent<Image>();
        bar.fillAmount = amount / 4f;
    }
}
