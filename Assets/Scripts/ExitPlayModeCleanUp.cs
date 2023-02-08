using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using BCIEssentials.Controllers;

public class ExitPlayModeCleanUp : MonoBehaviour
{

    void OnApplicationQuit()
    {
        BCIController.Instance.CloseLSLMarkerStream();
        BCIController.Instance.CloseLSLResponseStream();
        Debug.Log("Exiting play mode - LSL Networks should be closed");
    }

}
