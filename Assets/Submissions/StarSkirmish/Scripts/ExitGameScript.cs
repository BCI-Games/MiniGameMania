using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BCIEssentials.Controllers;

public class ExitGameScript : MonoBehaviour {
	public void ExitApplication() {
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;

#endif
        Application.Quit();
	}
}
