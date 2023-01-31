using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuInitializer : MonoBehaviour
{
    void Start()
    {
        GetComponentInChildren<SettingsTabManager>(true).Init();
        // TODO: add Option menu init call
    }
}
