using BCIEssentials.Controllers;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BCIKeybindsMenu : MonoBehaviour, IRequiresInit
{

    public GameObject keybindEditPopup;
    public TextMeshProUGUI keybindDisplay;
    public string startingDisplayText;
    public Button applyKeybindEditButton;

    BCIKeyBindTarget target;
    KeyCode selectedCode;

    bool isEditing;

    public void Init()
    {
        keybindEditPopup.SetActive(false);

        foreach (BCIKeyBindTarget keyBindTarget in GetComponentsInChildren<BCIKeyBindTarget>(true))
            keyBindTarget.Init(this);
    }

    private void OnDisable()
    {
        EndKeybindEdit();
    }

    private void OnGUI()
    {
        if (!isEditing)
            return;

        Event e = Event.current;
        if (e.isKey && e.keyCode != KeyCode.None)
        {
            selectedCode = e.keyCode;
            keybindDisplay.text = selectedCode.ToString();
            applyKeybindEditButton.interactable = true;
        }
    }

    public void StartEditingKeybind(BCIKeyBindTarget bindTarget)
    {
        target = bindTarget;
        keybindEditPopup.SetActive(true);
        keybindDisplay.text = startingDisplayText;
        applyKeybindEditButton.interactable = false;
        isEditing = true;
    }

    public void ApplyKeybindEdit()
    {
        target.ApplyKeybindEdit(selectedCode);

        EndKeybindEdit();
    }
    public void EndKeybindEdit()
    {
        isEditing = false;
        keybindEditPopup.SetActive(false);
    }
}
