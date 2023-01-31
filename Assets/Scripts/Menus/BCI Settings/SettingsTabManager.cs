using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class SettingsTabManager: MonoBehaviour
{
    [Header("tabs")]
    SettingsProxy[] tabs = new SettingsProxy[]
    {
        new BCIControllerSettingsProxy<P300Settings, P300ControllerBehavior>(),
        new BCIControllerSettingsProxy<MotorImagerySettings, MIControllerBehavior>(),
        new BCIControllerSettingsProxy<SSVEPSettings, SSVEPControllerBehavior>(),
        new LSLSettingsProxy()
    };
    public int startingTabIndex = 0;

    [Header("references")]
    public TextMeshProUGUI descriptionText;

    [Header("ui prefabs")]
    public GameObject inputFieldPrefab;
    public GameObject toggleFieldPrefab;

    SettingsProxy activeTab;

    void Start()
    {
        DestroyAllChildren();

        activeTab = tabs[startingTabIndex];
        GenerateActiveTabContent();
    }

    private void OnDisable()
    {
        // save settings to file when closing menu
        SettingsManager.instance.SaveSettingsToFile();
    }

    public void SelectTab(int tabIndex)
    {
        activeTab.ApplySettings();

        DestroyAllChildren();

        activeTab = tabs[tabIndex];
        GenerateActiveTabContent();
    }

    public void DestroyAllChildren()
    {
        foreach (Transform child in transform)
            Destroy(child.gameObject);
    }

    public void GenerateActiveTabContent()
    {
        foreach (SettingBase setting in activeTab)
        {
            if (setting is ToggleSetting)
                CreateSettingField(toggleFieldPrefab, setting);
            else
                CreateSettingField(inputFieldPrefab, setting);
        }
    }

    void CreateSettingField(GameObject prefab, SettingBase setting)
    {
        GameObject inputFieldInstance = Instantiate(prefab);
        inputFieldInstance.transform.SetParent(transform, false);
        inputFieldInstance.GetComponent<SettingField>().Init(setting, SetDescription);
    }

    protected void SaveSettings()
    {
        SettingsManager.instance.SaveSettingsToFile();
    }

    void SetDescription(string description)
    {
        descriptionText.text = description;
    }

    public void OpenSettingsFile()
    {
        SettingsManager.instance.OpenSettingsFileInExplorer();
    }

    public void ResetActiveTabToDefault()
    {
        // TODO: add default functionality for each category proxy
    }
}
