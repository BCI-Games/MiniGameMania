using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class SettingInputField : SettingField
{
    public TMP_InputField inputField;
    public TextMeshProUGUI rangeLabel;

    string previousValueString;

    public override void InitializeField(SettingBase setting)
    {
        UnityAction<string> SetValue = (string valueString) =>
        {
            setting.SetValueFromString(valueString);
            if (valueString != previousValueString)
            {
                previousValueString = valueString;
                SettingsManager.Save();
            }
        };
        inputField.onEndEdit.AddListener(SetValue);

        rangeLabel.text = setting.GetRange().range;
    }

    public override void UpdateFromSetting()
    {
        string value = targetSetting.GetValueString();
        previousValueString = value;
        inputField.text = value;
    }
}
