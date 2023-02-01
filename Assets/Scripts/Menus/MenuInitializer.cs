using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuInitializer : MonoBehaviour
{
    void Start()
    {
        foreach (var submenu in GetComponentsInChildren<IRequiresInit>(true))
            submenu.Init();
        // TODO: add Option menu init call
    }
}

public interface IRequiresInit
{
    public void Init();
}