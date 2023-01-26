using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

using KeyBindTarget = BCIKeyBindTarget.KeyBindTarget;


// TODO: actually effect BCIController keybinds
// TODO: display current keybinds
// TODO: set up select object 0-9 as individual binds or it's own completely editable set of binds
public class BCIKeybindsMenu : MonoBehaviour
{

    public GameObject keybindEditPopup;
    public TextMeshProUGUI keybindDisplay;
    public string startingDisplayText;
    public Button applyKeybindEditButton;

    KeyBindTarget target;
    KeyCode selectedCode;

    bool isEditing;

    void Start()
    {
        keybindEditPopup.SetActive(false);
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
        target = bindTarget.keyBindTarget;
        keybindEditPopup.SetActive(true);
        keybindDisplay.text = startingDisplayText;
        applyKeybindEditButton.interactable = false;
        isEditing = true;
    }

    public void ApplyKeybindEdit()
    {
        // TODO: apply keybind to BCI controller
        EndKeybindEdit();
        throw new System.NotImplementedException();
    }
    public void EndKeybindEdit()
    {
        isEditing = false;
        keybindEditPopup.SetActive(false);
    }
}
