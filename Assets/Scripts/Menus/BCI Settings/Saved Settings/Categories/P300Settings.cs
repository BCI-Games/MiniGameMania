using System.Collections.Generic;
using UnityEngine;


[System.Serializable, CreateAssetMenu(fileName = "P300 Settings", menuName = "Settings/P300 Settings")]
public class P300Settings : SettingsBlock
{
    new public const string Name = "P300 Settings";


    public IntegerSetting numFlashesLowerLimit = new IntegerSetting("flash count lower limit", 9, 6, 12);
    public IntegerSetting numFlashesUpperLimit = new IntegerSetting("flash count upper limit", 12, 9, 15);

    public FloatSetting flashOnTime = new FloatSetting("flash on time", 0.2f, 0.1f, 0.5f);
    public FloatSetting flashOffTime = new FloatSetting("flash off time", 0.3f, 0.1f, 0.5f);

    public ToggleSetting singleFlash = new ToggleSetting("single flash", true);
    public ToggleSetting multiFlash = new ToggleSetting("multi-flash", false);

    public ToggleSetting rowColumn = new ToggleSetting("row-column", false);
    public ToggleSetting checkerboard = new ToggleSetting("checkerboard", true);

    public FloatSetting trainBufferTime = new FloatSetting("train buffer time", 0f, 0f, 1f);


    public override IEnumerator<SettingBase> GetEnumerator()
    {
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
}
