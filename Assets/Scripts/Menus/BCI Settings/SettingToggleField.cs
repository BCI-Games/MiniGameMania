using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class SettingToggleField : SettingField
{
    public Toggle toggleField;

    bool previousValue;

    public override void InitializeField(SettingBase setting)
    {
        ToggleSetting toggleSetting = setting as ToggleSetting;
        UnityAction<bool> SetValue = (bool value) =>
        {
            toggleSetting.value = value;
            if (value != previousValue)
            {
                previousValue = value;
                SettingsManager.Save();
            }
        };
        toggleField.onValueChanged.AddListener(SetValue);
    }

    public override void UpdateFromSetting()
    {
        bool value = (targetSetting as ToggleSetting).value;
        previousValue = value;
        toggleField.isOn = value;
    }
}
