using System.Collections.Generic;
using UnityEngine;


[System.Serializable, CreateAssetMenu(fileName = "P300 Settings", menuName = "Settings/P300 Settings")]
public class P300Settings : BCIControllerSettings<P300ControllerBehavior>
{
    new public const string Name = "P300 Settings";


    public IntegerSetting numFlashesLowerLimit = new("flash count lower limit", 9, 6, 12);
    public IntegerSetting numFlashesUpperLimit = new("flash count upper limit", 12, 9, 15);

    public FloatSetting flashOnTime = new("flash on time", 0.2f, 0.1f, 0.5f);
    public FloatSetting flashOffTime = new("flash off time", 0.3f, 0.1f, 0.5f);

    public ToggleSetting singleFlash = new("single flash", true);
    public ToggleSetting multiFlash = new("multi-flash", false);

    public ToggleSetting rowColumn = new("row-column", false);
    public ToggleSetting checkerboard = new("checkerboard", true);

    public FloatSetting trainBufferTime = new("train buffer time", 0f, 0f, 1f);


    public override IEnumerator<SettingBase> GetEnumerator()
    {
        using (IEnumerator<SettingBase> ie = base.GetEnumerator())
        while (ie.MoveNext())
        {
            yield return ie.Current;
        }

        yield return numFlashesLowerLimit;
        yield return numFlashesUpperLimit;
        yield return flashOnTime;
        yield return flashOffTime;
        yield return singleFlash;
        yield return multiFlash;
        yield return rowColumn;
        yield return checkerboard;
        yield return trainBufferTime;
    }

    public override void ApplyToController(P300ControllerBehavior target)
    {
        base.ApplyToController(target);

        target.numFlashesLowerLimit = numFlashesLowerLimit;
        target.numFlashesUpperLimit = numFlashesUpperLimit;
        target.onTime = flashOnTime;
        target.offTime = flashOffTime;
        target.singleFlash = singleFlash;
        target.multiFlash = multiFlash;
        target.rowColumn = rowColumn;
        target.checkerboard = checkerboard;
        target.trainBufferTime = trainBufferTime;
    }
}
