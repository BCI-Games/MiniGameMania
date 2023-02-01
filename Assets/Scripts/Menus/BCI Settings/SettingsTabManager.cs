using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using BCIEssentials.Controllers;

public class SettingsTabManager: MonoBehaviour, IRequiresInit
{
    SettingsProxy[] categories = new SettingsProxy[]
    {
        new BCIControllerSettingsProxy<P300Settings, P300ControllerBehavior>(),
        new BCIControllerSettingsProxy<MotorImagerySettings, MIControllerBehavior>(),
        new BCIControllerSettingsProxy<SSVEPSettings, SSVEPControllerBehavior>(),
        new LSLSettingsProxy()
    };

    [Header("references")]
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI resetCategoryHeader;
    public Transform tabParent;

    [Header("ui prefabs")]
    public GameObject inputFieldPrefab;
    public GameObject toggleFieldPrefab;
    public GameObject tabPrefab;

    SettingsProxy activeCategory;

    List<SettingField> activeFields;
    List<SettingsTab> tabs;
    bool hasBeenActive;


    public void Init()
    {
        if (BCIController.Instance)
            ApplySettings();
    }

    void Start()
    {
        SettingsManager.SettingsFileChanged += OnSettingsFileChanged;
        hasBeenActive = true;

        activeFields = new();
        tabs = new();

        GenerateTabs();
        SelectTab(0);
    }

    void GenerateTabs()
    {
        foreach (Transform child in tabParent)
            Destroy(child.gameObject);

        for (int i = 0; i < categories.Length; i++)
            CreateTab(categories[i].Name, i);
    }

    void CreateTab(string title, int tabIndex)
    {
        GameObject tabInstance = Instantiate(tabPrefab);
        tabInstance.transform.SetParent(tabParent, false);

        SettingsTab newTab = tabInstance.GetComponent<SettingsTab>();
        newTab.Init(title, () => SelectTab(tabIndex));
        tabs.Add(newTab);
    }

    void OnDisable()
    {
        if (hasBeenActive)
        {
            ApplySettings();
        }
    }

    public void ApplySettings()
    {
        bool success = true;
        foreach (SettingsProxy tab in categories)
            success &= tab.ApplySettings();

        if (!success)
        {
            Debug.LogWarning("There was an issue applying BCI Settings");
            Debug.Log(FindObjectOfType<P300ControllerBehavior>());
        }
    }

    public void SelectTab(int tabIndex)
    {
        foreach (SettingsTab tab in tabs)
            tab.Deselect();
        tabs[tabIndex].Select();

        DestroyAllChildren();

        activeCategory = categories[tabIndex];
        GenerateActiveTabContent();

        resetCategoryHeader.text = $"Reset {activeCategory.Name}?";
    }

    public void DestroyAllChildren()
    {
        foreach (Transform child in transform)
            Destroy(child.gameObject);
    }

    public void GenerateActiveTabContent()
    {
        activeFields.Clear();
        foreach (SettingBase setting in activeCategory)
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
        ApplySettings();

        if (isActiveAndEnabled)
        {
            foreach (SettingField s in activeFields)
                s.UpdateFromSetting();
        }
    }

    public void DebugTest()
    {
        Debug.Log(FindObjectOfType<P300ControllerBehavior>());
    }
}
