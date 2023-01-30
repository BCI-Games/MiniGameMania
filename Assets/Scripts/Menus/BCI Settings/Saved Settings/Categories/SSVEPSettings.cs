using System.Collections.Generic;
using UnityEngine;


[System.Serializable, CreateAssetMenu(fileName = "SSVEP Settings", menuName = "Settings/SSVEP Settings")]
public class SSVEPSettings : BCIControllerSettings<SSVEPControllerBehavior>
{
    new public const string Name = "SSVEP Settings";

    // TODO: add members


    public override IEnumerator<SettingBase> GetEnumerator()
    {
        yield return null;
    }

    public override void ApplyToController(SSVEPControllerBehavior target)
    {
        base.ApplyToController(target);

        // TODO: apply membets to target
    }
}
