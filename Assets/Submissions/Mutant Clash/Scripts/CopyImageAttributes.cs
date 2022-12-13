using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CopyImageAttributes : MonoBehaviour
{
    public Image copyFrom;

    [System.Flags]
    public enum Attributes { Nothing = 0, Colour = 1, Position = 2, Scale = 4, FillAmount = 8}
    public Attributes attributesToCopy;

    Image image;


    void Start()
    {
        image = GetComponent<Image>();
    }


    void Update()
    {
        if ((attributesToCopy & Attributes.Colour) != 0)
            image.color = copyFrom.color;
        if ((attributesToCopy & Attributes.Position) != 0)
            transform.position = copyFrom.transform.position;
        if ((attributesToCopy & Attributes.Scale) != 0)
            transform.localScale = copyFrom.transform.localScale;
        if ((attributesToCopy & Attributes.FillAmount) != 0)
            image.fillAmount = copyFrom.fillAmount;
    }
}
