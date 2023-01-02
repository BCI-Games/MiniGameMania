using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class ReturnToMainMenu : MonoBehaviour {
	float timeTilBootToMainMenu = 5.0f;

	public bool countdownActivated = true;

    public UnityEvent OnMenuRequested;
    
	public void TriggerReturnToMainMenu() {
        OnMenuRequested?.Invoke();
		//SceneManager.LoadScene("MainMenu");
	}

	public void FixedUpdate() {
		if(!countdownActivated) {
			return;
		}
		timeTilBootToMainMenu -= Time.fixedDeltaTime;
		if(timeTilBootToMainMenu <= 0) {
			TriggerReturnToMainMenu();
		}
	}
}
