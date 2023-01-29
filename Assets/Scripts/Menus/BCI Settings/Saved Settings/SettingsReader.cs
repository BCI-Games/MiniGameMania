using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;

public class SettingsReader
{
    string filepath;
    Defaulter defaulter;

    public SettingsReader(string path)
    {
        filepath = path;
    }

    public bool Read(SettingsBlock[] settings)
    {
        if (!File.Exists(filepath))
        {
            Debug.LogWarning("Settings file does not exist");
            return false;
        }
        StreamReader inFile = new StreamReader(filepath);


        if (!SkipHeader(inFile))
        {
            inFile.Close();
            return false;
        }

        int maxLineCount = 10000;
        int lineCount = 0;

        string categoryName = "";
        string settingName = "";
        defaulter = new Defaulter(settings);

        while (inFile.Peek() != -1)
        {
            ReadLine(inFile.ReadLine(), ref categoryName, ref settingName, settings);

            if (lineCount++ > maxLineCount)
            {
                Debug.LogError("Went over max line count when attempting to read settings file");
                inFile.Close();
                return false;
            }
        }
        inFile.Close();
        defaulter.DefaultUndiscoveredSettings();
        return true;
    }

    bool SkipHeader(StreamReader inFile)
    {
        if (inFile.ReadLine() != ">>>> INFO <<<<")
        {
            Debug.LogWarning("File was found but format of first line was not as expected, aborting...");
            return false;
        }

        int lineCount = 0;

        while (!Regex.Match(inFile.ReadLine(), @">>>>.+<<<<").Success)
        {
            if (lineCount++ > 20)
                return false;
        }
        return true;
    }

    void ReadLine(string line, ref string categoryName, ref string settingName, SettingsBlock[] settings)
    {
        Match match;

        match = Regex.Match(line, @">>>> ([\w ]+) <<<<");
        if (match.Success)
        {
            string header = match.Groups[1].Value;
            if (!string.IsNullOrWhiteSpace(header))
                categoryName = header;
            return;
        }

        match = Regex.Match(line, @"---([\w ]+)---");
        if (match.Success)
        {
            settingName = match.Groups[1].Value;
            return;
        }

        match = Regex.Match(line, @"^Value:\s*([\S\s]+)$");
        if (match.Success)
        {
            //Debug.LogWarning($"Value {match.Groups[1].Value} found from line: {line}");
            SetValue(settings, categoryName, settingName, match.Groups[1].Value.Trim());
        }
    }

    void SetValue(SettingsBlock[] settings, string categoryName, string settingName, string value)
    {
        bool categoryFound = false;
        bool settingFound = false;

        foreach (SettingsBlock block in settings)
        {
            if (block.name == categoryName)
            {
                categoryFound = true;
                foreach (SettingBase blockItem in block)
                {
                    if (blockItem.name == settingName)
                    {
                        settingFound = true;
                        blockItem.SetValueFromString(value);
                        defaulter.OnValueDiscovered(categoryName, settingName);
                    }
                }
                if (!settingFound)
                    Debug.LogWarning($"No match found in settings for setting: {settingName}");
            }
        }
        if (!categoryFound)
            Debug.LogWarning($"No match found in settings for category: {categoryName}");
    }

    class Defaulter
    {
        Dictionary<string, Dictionary<string, SettingBase>> undiscoveredSettings;

        public Defaulter(SettingsBlock[] settings)
        {
            undiscoveredSettings = new Dictionary<string, Dictionary<string, SettingBase>>();

            foreach (SettingsBlock block in settings)
            {
                var settingsInCategory = new Dictionary<string, SettingBase>();
                foreach (SettingBase setting in block)
                {
                    settingsInCategory[setting.name] = setting;
                }

                undiscoveredSettings.Add(block.name, settingsInCategory);
            }
        }

        public void OnValueDiscovered(string categoryName, string settingName)
        {
            undiscoveredSettings[categoryName].Remove(settingName);
        }

        public void DefaultUndiscoveredSettings()
        {
            foreach (var category in undiscoveredSettings.Values)
            {
                foreach (SettingBase undiscoveredSetting in category.Values)
                {
                    undiscoveredSetting.SetDefault();
                    Debug.Log($"Assigning undiscovered setting ({undiscoveredSetting.name}) to the default value");
                }
            }
        }
    }
}
