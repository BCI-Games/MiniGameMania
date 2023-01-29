using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsInterface<SettingsType, ControllerType> : MonoBehaviour
    where SettingsType : SettingsBlock where ControllerType : MonoBehaviour
{
    protected SettingsType settings;
    protected ControllerType target;

    void Start()
    {
        settings = SettingsManager.GetCategory<SettingsType>();
        target = FindObjectOfType<ControllerType>();

        ApplySettings();
    }

    protected virtual void ApplySettings() { }
    protected void SaveSettings()
    {
        SettingsManager.instance.SaveSettingsToFile();
    }
}
