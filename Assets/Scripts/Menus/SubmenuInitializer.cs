using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmenuInitializer : MonoBehaviour
{
    void Start()
    {
        foreach (var submenu in GetComponentsInChildren<IRequiresInit>(true))
            submenu.Init();
        // TODO: add BCI Keybind menu call
    }
}

public interface IRequiresInit
{
    public void Init();
}