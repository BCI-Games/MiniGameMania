using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class BCIControllerSettingsProxy<SettingsType, ControllerType> : GenericSettingsProxy<SettingsType, ControllerType>
    where SettingsType : BCIControllerSettings<ControllerType> where ControllerType : BCIControllerBehavior
{
    public override void ApplySettings()
    {
        Settings.ApplyToController(Target);
    }
}
