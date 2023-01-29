using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class SettingsBlock : ScriptableObject, IEnumerable
{
    public const string Name = "Settings";

    public abstract IEnumerator<SettingBase> GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
