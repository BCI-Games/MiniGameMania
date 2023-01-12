using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeterController : MonoBehaviour
{
    public Transform thresholdBar;
    public Transform valueBar;
    public Transform bgBar;

    float height = 5f;
    float width = 1f;
    
    void Start() {
        bgBar.localScale = new Vector3(width, height, 1);
        bgBar.localPosition = new Vector3(0, height/2, 2);
    }

    public void setThreshold(float threshold) {
        float normalized = height * (100-threshold)/100;
        Debug.Log(normalized);
        thresholdBar.localScale = new Vector3(width, normalized, 1);
        thresholdBar.localPosition = new Vector3(0, height-normalized/2, 1);
    }

    public void setValue(float value) {
        float normalized = height * value/100;
        valueBar.localScale = new Vector3(width, normalized, 1);
        valueBar.localPosition = new Vector3(0, normalized/2, 0);
    }
    
}
