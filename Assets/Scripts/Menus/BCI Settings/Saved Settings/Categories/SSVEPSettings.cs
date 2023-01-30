using System.Collections.Generic;
using UnityEngine;


[System.Serializable, CreateAssetMenu(fileName = "SSVEP Settings", menuName = "Settings/SSVEP Settings")]
public class SSVEPSettings : BCIControllerSettings<SSVEPControllerBehavior>
{
    new public const string Name = "SSVEP Settings";

    // TODO: add members (may not be any unique members)

    // Add overriden GetEnumerator and ApplyToController if members are added
}
