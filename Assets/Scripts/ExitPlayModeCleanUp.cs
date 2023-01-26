using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using BCIEssentials.Controllers;

public class ExitCleanUp : MonoBehaviour
{
    //static void ExitPlayModeSceneLoader()
    //{
    //    EditorApplication.playModeStateChanged += CloseBCINetwork;
    //}

    //static void CloseBCINetwork(PlayModeStateChange state)
    //{
    //    if (state == PlayModeStateChange.ExitingPlayMode)
    //    {
    //        BCIController.Instance.CloseLSLMarkerStream();
    //        BCIController.Instance.CloseLSLResponseStream();
    //    }

    //    if (state == PlayModeStateChange.EnteredEditMode)
    //    {
    //        //This was broken not letting me move to the next scene due to how we are handling loading.
    //        Debug.Log("Entering edit mode - LSL Networks should be closed");
    //        //SceneManager.LoadScene("MainMenu_Minigames");
    //    }
    //}

    void OnApplicationQuit()
    {
        BCIController.Instance.CloseLSLMarkerStream();
        BCIController.Instance.CloseLSLResponseStream();
        Debug.Log("Exiting play mode - LSL Networks should be closed");
    }

}
