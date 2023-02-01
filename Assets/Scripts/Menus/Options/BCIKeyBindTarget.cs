using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using BCIEssentials.Controllers;

public class BCIKeyBindTarget : MonoBehaviour
{
    public enum KeyBindTarget
    {
        StartStopStimulus,
        StartAutomatedTraining,
        StartIterativeTraining,
        StartUserTraining
    }

    public KeyBindTarget keyBindTarget;

    public TextMeshProUGUI keybindDisplay;

    BCIKeybindsMenu parentMenu;
    string playerPrefsKey;

    public void Init(BCIKeybindsMenu parentMenu)
    {
        if (!BCIController.Instance)
            return;

        this.parentMenu= parentMenu;

        playerPrefsKey = $"bci keybind {(int)keyBindTarget}";
        KeyCode keyCode = KeyCode.None;

        if (PlayerPrefs.HasKey(playerPrefsKey))
        {
            keyCode = (KeyCode)PlayerPrefs.GetInt(playerPrefsKey);
        }
        else
        {
            switch (keyBindTarget)
            {
                case KeyBindTarget.StartStopStimulus:
                    keyCode = BCIController.Instance.startStopStimulusKeyCode;
                    break;
                case KeyBindTarget.StartAutomatedTraining:
                    keyCode = BCIController.Instance.startAutomatedTrainingKeyCode;
                    break;
                case KeyBindTarget.StartIterativeTraining:
                    keyCode = BCIController.Instance.startIterativeTrainingKeyCode;
                    break;
                case KeyBindTarget.StartUserTraining:
                    keyCode = BCIController.Instance.startUserTrainingKeyCode;
                    break;
            }
        }

        ApplyKeybindEdit(keyCode);
    }

    public void StartEditingKeyBind()
    {
        parentMenu.StartEditingKeybind(this);
    }

    public void ApplyKeybindEdit(KeyCode selectedCode)
    {
        switch (keyBindTarget)
        {
            case KeyBindTarget.StartStopStimulus:
                BCIController.Instance.startStopStimulusKeyCode = selectedCode;
                break;
            case KeyBindTarget.StartAutomatedTraining:
                BCIController.Instance.startAutomatedTrainingKeyCode = selectedCode;
                break;
            case KeyBindTarget.StartIterativeTraining:
                BCIController.Instance.startIterativeTrainingKeyCode = selectedCode;
                break;
            case KeyBindTarget.StartUserTraining:
                BCIController.Instance.startUserTrainingKeyCode = selectedCode;
                break;
        }
        DisplayKeybind(selectedCode);
        SaveKeybind(selectedCode);
    }

    void SaveKeybind(KeyCode selectedCode) => PlayerPrefs.SetInt(playerPrefsKey, (int)selectedCode);

    public void DisplayKeybind(KeyCode newCode)
    {
        keybindDisplay.text = newCode.ToString();
    }
}