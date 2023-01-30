using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SettingsTabManager: MonoBehaviour
{
    [Header("tabs")]
    public SettingsProxy[] tabs = new SettingsProxy[]
    {
        new BCIControllerSettingsProxy<P300Settings, P300ControllerBehavior>(),
        new BCIControllerSettingsProxy<SSVEPSettings, SSVEPControllerBehavior>(),
        new BCIControllerSettingsProxy<MotorImagerySettings, MIControllerBehavior>(),
        new LSLSettingsProxy()
    };
    public int startingTabIndex = 0;

    [Header("ui prefabs")]
    public GameObject inputFieldPrefab;
    public GameObject toggleFieldPrefab;

    SettingsProxy activeTab;

    void Start()
    {
        // clear children

        // generate content for starting tab
    }

    private void OnDisable()
    {
        // save settings to file when closing menu
        SettingsManager.instance.SaveSettingsToFile();
    }

    public void SelectTab(int tabIndex)
    {
        // apply settings for previous tab

        // clear children

        // generate content for new tab
    }

    public void GenerateActiveTabContent()
    {
        foreach (SettingBase setting in activeTab)
        {
            if (setting is ToggleSetting)
                CreateToggleField(setting as ToggleSetting);
            else
                CreateInputField(setting);
        }
    }

    void CreateInputField(SettingBase setting)
    {
        UnityAction<string> SetValue = (string valueString) =>
        {
            setting.SetValueFromString(valueString);
        };

        // TODO: clean up and move mroe advanced functions to script on prefab
        GameObject inputFieldInstance = Instantiate(inputFieldPrefab);
        inputFieldInstance.name = setting.name;

        inputFieldInstance.GetComponent<InputField>().onSubmit.AddListener(SetValue);
    }
    void CreateToggleField(ToggleSetting setting)
    {
        UnityAction<bool> SetValue = (bool value) =>
        {
            setting.value = value;
        };

        // TODO: clean up and move mroe advanced functions to script on prefab
        GameObject toggleFieldInstance = Instantiate(toggleFieldPrefab);
        toggleFieldInstance.name = setting.name;

        toggleFieldInstance.GetComponent<Toggle>().onValueChanged.AddListener(SetValue);
    }
    protected void SaveSettings()
    {
        SettingsManager.instance.SaveSettingsToFile();
    }
}
