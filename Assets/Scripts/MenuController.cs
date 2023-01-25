using System;
using BCIEssentials.Controllers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class MenuController : MonoBehaviour
{
    [FormerlySerializedAs("canvas")]
    [SerializeField] private Transform _canvas;
    [SerializeField] private KeyCode _menuKey = KeyCode.Escape;
    [SerializeField] private bool _dontDestroy = true;

    private static MenuController _activeController;

    private void Awake()
    {
        if (_activeController != null)
        {
            Debug.Log("Disabling MenuController. There is already an active instance.");
            Debug.Log("I am the duplicate coming from this scene: " + gameObject.scene);
            Destroy(gameObject);
        }
        else if (_dontDestroy)
        {
            _activeController = this;
            DontDestroyOnLoad(this);
        }
    }

    private void OnDestroy()
    {
        if (_activeController == this)
        {
            _activeController = null;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(_menuKey))
        {
            ShowMenu();
        }
    }

    public void ShowMenu()
    {
        if (_canvas.gameObject.activeInHierarchy == false)
        {
            _canvas.gameObject.SetActive(true);
        }
        else
        {
            _canvas.gameObject.SetActive(false);
        }
    }

    public void RequestBCIController(string behaviorType)
    {
        if (BCIController.Instance != null && Enum.TryParse(behaviorType, out BehaviorType parsedType))
        {
            BCIController.Instance.ChangeBehavior(parsedType);
        }
        else
        {
            Debug.LogError("Failed to request a bci controller behavior");
        }
    }

    public void ToggleSetupBehavior(bool setupBool)
    {
        BCIController.Instance.ActiveBehavior.setupRequired = setupBool;
    }
    
    public void LoadScene(string sceneName)
    {
        _canvas.gameObject.SetActive(false);
        SceneManager.LoadScene(sceneName);
    }
}
