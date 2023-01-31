using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class SettingInputField : SettingField
{
    public InputField inputField;
    public TextMeshProUGUI rangeLabel;

    public override void InitializeField(SettingBase setting)
    {
        UnityAction<string> SetValue = (string valueString) =>
        {
            setting.SetValueFromString(valueString);
        };
        inputField.onEndEdit.AddListener(SetValue);

        inputField.text = setting.GetValueString();

        rangeLabel.text = setting.GetRange().range;
    }
}
