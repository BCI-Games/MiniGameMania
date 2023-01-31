using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// TODO - improve diff checking to use date changed whenever the game gets focus
public class SettingsManager : MonoBehaviour
{
    public static SettingsManager instance;
    public static EventHandler SettingsFileChanged;

    public SettingsBlock[] categories;

    static bool warningPrinted = false;

    List<string> SettingList
    {
        get
        {
            var result = new List<string>();

            foreach (SettingsBlock category in categories)
                foreach (SettingBase setting in category)
                    result.Add(setting.GetValueString());

            return result;
        }
    }

    string filepath;
    SettingsReader reader;
    SettingsWriter writer;


    void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(this);
        instance = this;

        filepath = $"{Application.persistentDataPath}/settings.txt";
        reader = new SettingsReader(filepath);
        writer = new SettingsWriter(filepath);

        print($"Attempting to read Settings save file from {filepath}");
        if (reader.Read(categories))
        {
            print("Saving freshly read settings to file to fill out any differences");
            SaveSettingsToFile();
        }
        else
            ResetToDefault();
    }

    public static SettingsBlock GetCategory(string categoryName)
    {
        foreach (SettingsBlock category in instance.categories)
            if (category.name == categoryName)
                return category;

        Debug.LogWarning($"Unable to find settings category {categoryName}");
        return null;
    }

    public static T GetCategory<T>() where T : ScriptableObject
    {
        try
        {
            foreach (SettingsBlock category in instance.categories)
                if (category is T)
                    return (category as T);

            Debug.LogWarning($"Unable to find category of type {typeof(T)}");
            return null;
        }
        catch (NullReferenceException e)
        {
            if (!warningPrinted)
            {
                Debug.LogWarning("Null reference in settingsManager, likely using it outside of intended context\n" + e.Message);
                warningPrinted = true;
            }
            return null;
        }
    }

    public static void Save() => instance?.SaveSettingsToFile();
    public void SaveSettingsToFile()
    {
        print($"Saving settings to file...");
        writer.Save(categories);
        ResetPeriodicDiffCheck();
    }

    public void OpenSettingsFileInExplorer()
    {
        System.Diagnostics.Process.Start("explorer.exe", "/select," + filepath.Replace(@"/", @"\"));
    }

    public void ResetToDefault()
    {
        foreach (SettingsBlock category in categories)
            foreach (SettingBase setting in category)
                setting.SetDefault();

        SaveSettingsToFile();
    }

    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
            return;
        if (writer.HasWriteTimeChanged())
        {
            print("different modified time found after gaining focus, reading from settings");
            reader.Read(categories);
            ResetPeriodicDiffCheck();

            if (SettingsFileChanged != null)
                SettingsFileChanged(this, new EventArgs());
        }
    }

    void ResetPeriodicDiffCheck()
    {
        StopAllCoroutines();
        StartCoroutine(CheckForSettingChanges(SettingList));
    }

    IEnumerator CheckForSettingChanges(List<string> previousSettings)
    {
        yield return new WaitForSecondsRealtime(5);

        var currentSettings = SettingList;

        if (!currentSettings.SequenceEqual(previousSettings))
        {
            print("Changes found in settings at runtime, likely alterations to a scriptable object");
            SaveSettingsToFile();
        }

        StartCoroutine(CheckForSettingChanges(currentSettings));
    }
}
