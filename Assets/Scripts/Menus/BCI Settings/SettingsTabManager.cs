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
    public TextMeshProUGUI resetCategoryHeader;

    [Header("ui prefabs")]
    public GameObject inputFieldPrefab;
    public GameObject toggleFieldPrefab;

    SettingsProxy activeTab;

    List<SettingField> activeFields;

    void Start()
    {
        activeFields = new();
        SelectTab(startingTabIndex);
        SettingsManager.SettingsFileChanged += OnSettingsFileChanged;
    }

    public void SelectTab(int tabIndex)
    {
        activeTab?.ApplySettings();

        DestroyAllChildren();

        activeTab = tabs[tabIndex];
        GenerateActiveTabContent();

        resetCategoryHeader.text = $"Reset {activeTab.Name}?";
    }

    public void DestroyAllChildren()
    {
        foreach (Transform child in transform)
            Destroy(child.gameObject);
    }

    public void GenerateActiveTabContent()
    {
        activeFields.Clear();
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

        SettingField newField = inputFieldInstance.GetComponent<SettingField>();
        newField.Init(setting, SetDescription);
        activeFields.Add(newField);
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
        foreach (SettingField s in activeFields)
            s.ResetToDefault(false);

        SettingsManager.Save();
    }

    void OnSettingsFileChanged(object sender, System.EventArgs args)
    {
        foreach (SettingField s in activeFields)
            s.UpdateFromSetting();
    }
}
