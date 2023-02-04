using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class BCIControllerSettingsProxy<SettingsType, ControllerType> : GenericSettingsProxy<SettingsType, ControllerType>
    where SettingsType : BCIControllerSettings<ControllerType> where ControllerType : BCIControllerBehavior
{
    public override bool ApplySettings()
    {
        if (Target)
        {
            Settings.ApplyToController(Target);
            return true;
        }

        Debug.LogWarning("Attempted to apply BCI Settings To null target");
        return false;
    }
}
