using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TMPro;

public class SettingsTab : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("transition")]
    public Color normalColour;
    public Color highlightedColour;
    public Color selectedColour;

    public Graphic targetGraphic;

    [Header("references")]
    public Button button;
    public TextMeshProUGUI label;

    bool selected = false;

    public void Init(string title, UnityAction onClick)
    {
        name = title;
        label.text = title;

        button.onClick.AddListener(onClick);
        targetGraphic.color = normalColour;
    }

    public void Select()
    {
        selected = true;
        targetGraphic.color = selectedColour;
    }
    public void Deselect()
    {
        selected = false;
        targetGraphic.color = normalColour;
    }
    public void OnPointerEnter(PointerEventData data)
    {
        if (selected)
            return;

        targetGraphic.color = highlightedColour;
    }

    public void OnPointerExit(PointerEventData data)
    {
        if (selected)
            return;

        targetGraphic.color = normalColour;
    }
}
