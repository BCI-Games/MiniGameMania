using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BCIKeyBindTarget : MonoBehaviour
{
    public enum KeyBindTarget
    {
        StartStopStimulus,
        StartAutomatedTraining,
        StartIterativeTraining,
        StartUserTraining,
        SelectObject
    }

    public KeyBindTarget keyBindTarget;
}
