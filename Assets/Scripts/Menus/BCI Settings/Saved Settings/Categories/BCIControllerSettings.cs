using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Stores the base BCIController settings shared by each specific controller
/// </summary>
public class BCIControllerSettings<ControllerType> : SettingsBlock where ControllerType: BCIControllerBehavior
{
    public override string Name => "BCI Controller Settings";

    public IntegerSetting numTrainingSelections = new("training selection count", 9, 6, 12);
    public IntegerSetting numTrainWindows = new("training window count", 3, 2, 5);

    public ToggleSetting trainTargetPersistant = new("train target persistant", false);
    public FloatSetting pauseBeforeTraining = new("pause before training", 2, 1, 5);
    public FloatSetting trainTargetPresentationTime = new("training target presentation time", 3, 2, 5);
    public FloatSetting trainBreak = new("training break", 1, 0, 3);

    public IntegerSetting trainTarget = new("training target", 99, 8, 99);

    public override IEnumerator<SettingBase> GetEnumerator()
    {
        yield return numTrainingSelections;
        yield return numTrainWindows;
        yield return trainTargetPersistant;
        yield return pauseBeforeTraining;
        yield return trainTargetPresentationTime;
        yield return trainBreak;
        yield return trainTarget;
    }

    public virtual void ApplyToController(ControllerType target)
    {
        target.numTrainingSelections = numTrainingSelections;
        target.numTrainWindows = numTrainWindows;
        target.trainTargetPersistent = trainTargetPersistant;
        target.pauseBeforeTraining = pauseBeforeTraining;
        target.trainTargetPresentationTime = trainTargetPresentationTime;
        target.trainBreak = trainBreak;
        target.trainTarget = trainTarget;
    }
}
