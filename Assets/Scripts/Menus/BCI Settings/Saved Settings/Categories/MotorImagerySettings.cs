using System.Collections.Generic;
using UnityEngine;


[System.Serializable, CreateAssetMenu(fileName = "Motor Imagery Settings", menuName = "BCI Settings/Motor Imagery Settings")]
public class MotorImagerySettings : BCIControllerSettings<MIControllerBehavior>
{
    public override string Name => "Motor Imagery Settings";

    public IntegerSetting numSelectionsBeforeTraining = new("selections before training", 3, 2, 5);
    public IntegerSetting numSelectionsBetweenTraining = new("selections between training", 3, 2, 5);

    public override IEnumerator<SettingBase> GetEnumerator()
    {
        IEnumerator<SettingBase> baseIE = base.GetEnumerator();
        while (baseIE.MoveNext())
        {
            yield return baseIE.Current;
        }

        yield return numSelectionsBeforeTraining;
        yield return numSelectionsBetweenTraining;
    }

    public override void ApplyToController(MIControllerBehavior target)
    {
        base.ApplyToController(target);

        target.numSelectionsBeforeTraining = numSelectionsBeforeTraining;
        target.numSelectionsBetweenTraining = numSelectionsBetweenTraining;
    }
}
