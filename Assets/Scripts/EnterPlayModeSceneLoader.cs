using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using BCIEssentials.Controllers;

[InitializeOnLoad]
public class EnterPlayModeSceneLoader
{
    static EnterPlayModeSceneLoader()
    {
        EditorApplication.playModeStateChanged += LoadDefaultScene;
    }

    static void LoadDefaultScene(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.ExitingEditMode)
        {
            EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        }

        if (state == PlayModeStateChange.EnteredPlayMode)
        {
            //This was broken not letting me move to the next scene due to how we are handling loading.
            //SceneManager.LoadScene("Initialize");
            //SceneManager.LoadScene("MainMenu_Minigames");
        }
    }
}