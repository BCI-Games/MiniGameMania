using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SettingsProxy: Object, IEnumerable
{
    public abstract void ApplySettings();

    public abstract IEnumerator<SettingBase> GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public virtual string Name => "Settings";
}

public abstract class GenericSettingsProxy<SettingsType, SettingsTargetType>: SettingsProxy
    where SettingsType: SettingsBlock where SettingsTargetType : Object
{
    public override string Name => Settings.Name;

    protected SettingsType Settings
    {
        get
        {
            if (!_settings)
                _settings = SettingsManager.GetCategory<SettingsType>();

            return _settings;
        }
    }
    SettingsType _settings;
    protected SettingsTargetType Target
    {
        get
        {
            if (!_target)
                _target = FindObjectOfType<SettingsTargetType>();

            return _target;
        }
    }
    SettingsTargetType _target;

    public override IEnumerator<SettingBase> GetEnumerator()
    {
        return Settings.GetEnumerator();
    }
}
