using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SettingsTabManager: MonoBehaviour
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

    [Header("ui prefabs")]
    public GameObject inputFieldPrefab;
    public GameObject toggleFieldPrefab;

    SettingsProxy activeCategory;

    List<SettingField> activeFields;
    bool hasBeenActive;


    public void Init()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode loadMode)
    {
        print(scene.name + ", " + scene.buildIndex + ", " + scene);
        if (scene.name == "Initialize")
        {
            ApplySettings();
        }
    }

    void Start()
    {
        activeFields = new();
        SelectTab(0);
        SettingsManager.SettingsFileChanged += OnSettingsFileChanged;
        hasBeenActive = true;
    }

    private void OnDisable()
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
        activeCategory?.ApplySettings();

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
