using System.Collections.Generic;
using UnityEngine;


[System.Serializable, CreateAssetMenu(fileName = "LSL Settings", menuName = "BCI Settings/LSL Settings")]
public class LSLSettings : SettingsBlock
{
    public override string Name => "Lab Streaming Layer Settings";


    public StringSetting profileName = new StringSetting("profile name", "profile-name");


    public override IEnumerator<SettingBase> GetEnumerator()
    {
        yield return profileName;
    }
}
