using System.Collections.Generic;
using UnityEngine;


[System.Serializable, CreateAssetMenu(fileName = "SSVEP Settings", menuName = "BCI Settings/SSVEP Settings")]
public class SSVEPSettings : BCIControllerSettings<SSVEPControllerBehavior>
{
    public override string Name => "SSVEP Settings";

    // TODO: add members (may not be any unique members)

    // Add overriden GetEnumerator and ApplyToController if members are added
}
