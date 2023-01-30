using System.Collections.Generic;
using UnityEngine;


[System.Serializable, CreateAssetMenu(fileName = "SSVEP Settings", menuName = "Settings/SSVEP Settings")]
public class SSVEPSettings : BCIControllerSettings<SSVEPControllerBehavior>
{
    new public const string Name = "SSVEP Settings";

    // TODO: add members (may not be any unique members)

    // Add overriden GetEnumerator if members are added

    public override void ApplyToController(SSVEPControllerBehavior target)
    {
        base.ApplyToController(target);

        // TODO: apply members to target
    }
}
