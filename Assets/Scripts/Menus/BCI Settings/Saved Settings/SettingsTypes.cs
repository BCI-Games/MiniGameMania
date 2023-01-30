using UnityEngine;

public abstract class SettingBase
{
    public string name;
    [TextArea]
    public string description;

    public abstract string GetValueString();
    public abstract void SetValueFromString(string source);
    public abstract SettingRange GetRange();
    public abstract void SetDefault();
}

public struct SettingRange
{
    public string def;
    public string range;

    public SettingRange(string Def, string Min, string Max)
    {
        def = Def;
        range = $"{Min} - {Max}";
    }

    public SettingRange(string Def, string Range)
    {
        def = Def;
        range = Range;
    }
}

public abstract class GenericSetting<T> : SettingBase
{
    public T defaultValue;
    public T minimumValue;
    public T maximumValue;

    public T value;

    public override string GetValueString() => value.ToString();
    public override SettingRange GetRange()
    {
        return new SettingRange(defaultValue.ToString(), minimumValue.ToString(), maximumValue.ToString());
    }
    public override void SetDefault() => value = defaultValue;
}

[System.Serializable]
public class IntegerSetting : GenericSetting<int>
{
    public IntegerSetting(string name, int defaultValue, int minimumValue, int maximumValue)
    {
        this.name = name;
        this.defaultValue = defaultValue;
        this.minimumValue = minimumValue;
        this.maximumValue = maximumValue;
    }

    public override void SetValueFromString(string source)
    {
        int parseResult;
        if (int.TryParse(source, out parseResult))
            value = parseResult;
        else
            Debug.LogError("Error parsing integer setting");
    }

    public static implicit operator int(IntegerSetting setting) => setting.value;
}

[System.Serializable]
public class FloatSetting : GenericSetting<float>
{
    public FloatSetting(string name, float defaultValue, float minimumValue, float maximumValue)
    {
        this.name = name;
        this.defaultValue = defaultValue;
        this.minimumValue = minimumValue;
        this.maximumValue = maximumValue;
    }

    public override void SetValueFromString(string source)
    {
        float parseResult;
        if (float.TryParse(source, out parseResult))
            value = parseResult;
        else
            Debug.LogError("Error parsing float setting");
    }

    public static implicit operator float(FloatSetting setting) => setting.value;
}

[System.Serializable]
public class ToggleSetting : GenericSetting<bool>
{
    public ToggleSetting(string name, bool defaultValue)
    {
        this.name = name;
        this.defaultValue = defaultValue;
        minimumValue = false;
        maximumValue = true;
    }

    public override void SetValueFromString(string source)
    {
        if (source.ToLower() == "f")
            value = false;
        else if (source.ToLower() == "t")
            value = true;
        else
        {
            bool parseResult;
            if (bool.TryParse(source, out parseResult))
                value = parseResult;
            else
            {
                int integerParseResult;
                if (int.TryParse(source, out integerParseResult))
                    value = integerParseResult > 0;
                else
                    Debug.LogError("Error parsing toggle setting");
            }
        }
    }

    public override string GetValueString()
    {
        return value ? "T" : "F";
    }

    public override SettingRange GetRange()
    {
        return new SettingRange(value ? "T" : "F", "F", "T");
    }

    public static implicit operator bool(ToggleSetting setting) => setting.value;
}

[System.Serializable]
public class StringSetting: SettingBase
{
    public string defaultValue;
    public string value;

    public StringSetting(string name, string defaultValue)
    {
        this.name = name;
        this.defaultValue = defaultValue;
    }

    public override void SetValueFromString(string source)
    {
        value = source;
    }
    public override string GetValueString() => value;
    public override SettingRange GetRange() => new SettingRange(defaultValue, "N/A");
    public override void SetDefault() => value = defaultValue;

    public static implicit operator string(StringSetting setting) => setting.value;
}