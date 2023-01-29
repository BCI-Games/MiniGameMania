using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P300SettingsInterface : SettingsInterface<P300Settings, P300ControllerBehavior>
{
    protected override void ApplySettings()
    {
        // TODO: apply settings to target
        target.numFlashesLowerLimit = settings.numFlashesLowerLimit;
        target.numFlashesUpperLimit = settings.numFlashesUpperLimit;
        target.onTime = settings.flashOnTime;
        target.offTime = settings.flashOffTime;
        target.singleFlash = settings.singleFlash;
        target.multiFlash = settings.multiFlash;
        target.rowColumn = settings.rowColumn;
        target.checkerboard = settings.checkerboard;
        target.trainBufferTime = settings.trainBufferTime;
    }

    public void SetLowerFlashLimit(int value)
    {
        settings.numFlashesLowerLimit.value = value;
        target.numFlashesLowerLimit = value;
        SaveSettings();
    }
    public void SetUpperFlashLimit(int value)
    {
        settings.numFlashesUpperLimit.value = value;
        target.numFlashesUpperLimit = value;
        SaveSettings();
    }

    public void SetFlashOnTime(float value)
    {
        settings.flashOnTime.value = value;
        target.onTime = value;
        SaveSettings();
    }
    public void SetFlashOffTime(float value)
    {
        settings.flashOffTime.value = value;
        target.offTime = value;
        SaveSettings();
    }

    public void SetSingleFlash(bool value)
    {
        settings.singleFlash.value = value;
        target.singleFlash = value;
        SaveSettings();
    }
    public void SetMultiFlash(bool value)
    {
        settings.multiFlash.value = value;
        target.multiFlash = value;
        SaveSettings();
    }

    public void SetRowColumn(bool value)
    {
        settings.rowColumn.value = value;
        target.rowColumn = value;
        SaveSettings();
    }
    public void SetCheckerboard(bool value)
    {
        settings.checkerboard.value = value;
        target.checkerboard = value;
        SaveSettings();
    }

    public void SetTrainBufferTime(float value)
    {
        settings.trainBufferTime.value = value;
        target.trainBufferTime = value;
        SaveSettings();
    }
}
