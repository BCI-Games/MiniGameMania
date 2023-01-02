using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Submissions.StudioSomething;

public class GameplayManager : MonoBehaviour
{

    public GameObject MainMenu;
    public GameObject GameUI;
    public GameObject StationCamera;
    public GameObject GameVirtualCamera;
    public GameObject WinCanvas;
    public GameObject LoseCanvas;
    public StarUI StarUI;
    public LightingManager LightingManager;
    public CustomerManager CustomerManager;
    public int NumPlayers = 2;
    public Transform[] SpawnPoints;

    public GameObject PlayerPrefab;
    public static GameplayManager Instance;

    public GameObject FoodImage1;
    public GameObject FoodImage2;
    public Image FoodSprite1;
    public Image FoodSprite2;

    public bool GameHasStart;

    private bool SixCheck;

    private bool EightCheck;

    void Awake()
    {

        if (Instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        MainMenu.SetActive(true);
        StationCamera.SetActive(false);
        GameVirtualCamera.SetActive(false);
        GameUI.SetActive(false);
        WinCanvas.SetActive(false);
        LoseCanvas.SetActive(false);
    }

    void Update()
    {
        if (!GameHasStart) return;
        float currentTime = LightingManager.TimeOfDay;
        if (currentTime > 19f && !SixCheck)
        {
            CustomerManager.Instance.IsRunning = false;
            SixCheck = true;
        }

        if (currentTime > 21f && !EightCheck && CustomerManager.Instance.Customers.Count <= 0)
        {
            WinGame();
            EightCheck = true;
        }
    }

    // Add your game mananger members here
    public void Pause(bool paused)
    {

    }

    public void SinglePlayer()
    {
        NumPlayers = 1;
        StartGame();
    }
    public void quit(){
        Application.Quit();
    }
    public void Coop()
    {
        NumPlayers = 2;
        StartGame();
    }

    public void WinGame(){
        WinCanvas.SetActive(true);
        LightingManager.IsRunning = false;
        StartCoroutine(ResetGame());
    }

    public void LoseGame(){
        LoseCanvas.SetActive(true);
        StartCoroutine(ResetGame());
    }

    public void StartGame()
    {

        if (NumPlayers == 1)
        {
            GameObject player1 = GameObject.Instantiate(PlayerPrefab, SpawnPoints[0].position, SpawnPoints[0].rotation);
            PlayerController.BCI = player1.GetComponent<PlayerController>();
            PlayerController.BCI.FoodSprite = FoodSprite1;
            FoodImage1.SetActive(true);
        }
        else if (NumPlayers == 2)
        {
            FoodImage1.SetActive(true);
            GameObject player1 = GameObject.Instantiate(PlayerPrefab, SpawnPoints[0].position, SpawnPoints[0].rotation);
            PlayerController.BCI = player1.GetComponent<PlayerController>();
            PlayerController.BCI.FoodSprite = FoodSprite1;

            FoodImage2.SetActive(true);
            GameObject player2 = GameObject.Instantiate(PlayerPrefab, SpawnPoints[1].position, SpawnPoints[1].rotation);
            PlayerController.Keyboard = player2.GetComponent<PlayerController>();
            PlayerController.Keyboard.FoodSprite = FoodSprite2;

        }
        StationManager.Instance.RefreshStations();
        MainMenu.SetActive(false);
        StationCamera.SetActive(true);
        GameVirtualCamera.SetActive(true);
        GameUI.SetActive(true);
        LightingManager.TimeOfDay = 6;
        CustomerManager.Instance.IsRunning = true;
        LightingManager.IsRunning = true;
        BCIManager.Instance.BCICoroutine = BCIManager.Instance.BCICoFunction();
        StartCoroutine(BCIManager.Instance.BCICoroutine);
        GameHasStart = true;
    }

    public IEnumerator ResetGame()
    {
        yield return new WaitForSeconds(5f);
        StationManager.Instance.RefreshStations();
        MainMenu.SetActive(true);
        StationCamera.SetActive(false);
        GameVirtualCamera.SetActive(false);
        StarUI.Reset();
        GameUI.SetActive(false);
        LightingManager.TimeOfDay = 12;
        CustomerManager.Instance.IsRunning = false;
        LightingManager.IsRunning = false;
        WinCanvas.SetActive(false);
        LoseCanvas.SetActive(false);
        if (PlayerController.BCI != null) Destroy(PlayerController.BCI.gameObject);
        if (PlayerController.Keyboard != null) Destroy(PlayerController.Keyboard.gameObject);
        StopCoroutine(BCIManager.Instance.BCICoroutine);
        SixCheck = false;
        EightCheck = false;
        GameHasStart = false;
    }

}
