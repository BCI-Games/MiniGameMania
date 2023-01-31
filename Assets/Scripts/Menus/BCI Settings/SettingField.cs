using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TMPro;

public class SettingField : MonoBehaviour, IPointerEnterHandler
{
    public TextMeshProUGUI label;

    UnityAction<string> OnHighlight;
    SettingBase targetSetting;

    public virtual void Init(SettingBase setting, UnityAction<string> SetDescription)
    {
        targetSetting = setting;
        OnHighlight = SetDescription;

        name = setting.name;
        label.text = setting.name;

        InitializeField(setting);
    }

    public virtual void InitializeField(SettingBase setting) { }

    public void OnPointerEnter(PointerEventData data)
    {
        OnHighlight(targetSetting.description);
    }
}
