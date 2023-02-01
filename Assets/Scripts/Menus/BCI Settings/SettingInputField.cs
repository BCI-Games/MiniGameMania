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

    [Header("flash settings")]
    public Color errorColour = Color.red;
    public float flashLength = 0.5f;
    public int flashCount = 3;

    bool flashing;
    float flashTimer;
    string previousValueString;

    public override void InitializeField(SettingBase setting)
    {
        inputField.onEndEdit.AddListener(SetValue);

        if (setting is IntegerSetting)
            inputField.contentType = TMP_InputField.ContentType.IntegerNumber;
        else if (setting is FloatSetting)
            inputField.contentType = TMP_InputField.ContentType.DecimalNumber;

        rangeLabel.text = setting.GetRange().range;
    }

    void SetValue(string valueString)
    {
        if(targetSetting.SetValueFromString(valueString))
        {
            if(valueString != previousValueString)
            {
                previousValueString = valueString;
                SettingsManager.Save();
            }
        }
        else
        {
            flashing = true;
            flashTimer = 0;
        }
    }

    public override void UpdateFromSetting()
    {
        string value = targetSetting.GetValueString();
        previousValueString = value;
        inputField.text = value;
    }

    void Update()
    {
        if (flashing)
        {
            flashTimer += Time.deltaTime;

            if((int)(2 * flashTimer / flashLength) % 2 == 0)
            {
                inputField.targetGraphic.color = errorColour;
            }
            else
            {
                inputField.targetGraphic.color = inputField.colors.normalColor;
            }

            if (flashTimer > flashLength * flashCount)
            {
                inputField.targetGraphic.color = inputField.colors.normalColor;
                flashing = false;
                UpdateFromSetting();
            }
        }
    }
}
