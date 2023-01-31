using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class SettingToggleField : SettingField
{
    public Toggle toggleField;

    public override void InitializeField(SettingBase setting)
    {
        ToggleSetting toggleSetting = setting as ToggleSetting;
        UnityAction<bool> SetValue = (bool value) =>
        {
            toggleSetting.value = value;
        };

        toggleField.isOn = toggleSetting.value;
    }
}
