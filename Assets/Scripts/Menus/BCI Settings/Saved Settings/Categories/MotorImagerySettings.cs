using System.Collections.Generic;
using UnityEngine;


[System.Serializable, CreateAssetMenu(fileName = "Motor Imagery Settings", menuName = "Settings/Motor Imagery Settings")]
public class MotorImagerySettings : SettingsBlock
{
    new public const string Name = "Motor Imagery Settings";

    // TODO: add members


    public override IEnumerator<SettingBase> GetEnumerator()
    {
        yield return null;
    }
}
