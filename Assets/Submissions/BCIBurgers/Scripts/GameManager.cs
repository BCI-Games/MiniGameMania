using System.Collections;
using System.Collections.Generic;
using BCIEssentials.Controllers;
using Submissions.BCIBurgers;
using UnityEngine;

public class GameManager : MonoBehaviour
{


    public SelectionManager selectionManager;
    public UIManager uIManager;
    public ObjectManager objectManager;
    public CameraManager cameraManager;

    public GameObject LeftArrow;
    public GameObject RightArrow;
    public GameObject DownArrow;

    public enum GameState
    {
        DEFAULT,
        MENU,
        START,
        SCENE,
        WAIT,
        INPUT,
        NEXT,
        FINISH
    }

    public enum GameNum
    {
        one,
        two
    }

    public GameState gameState;
    public GameNum gameNum;


    public List<string> order = new List<string>();
    public int orderIndex = 0;

    public string[] toppings = new string[] { "Lettuce", "Tomato", "Onion", "Cheese", "Pepper", "Mushroom"};

    public GameObject tray1;

    // Start is called before the first frame update
    void Start()
    {
        gameState = GameState.START;
        gameNum = GameNum.one;
        SetArrowState();
        CreateOrder();
        //PlayerPrefs.SetString("order", string.join(',', order));
        CreateGame();

        //string test = PlayerPrefs.GetString("order");
        //Debug.Log(test);

        StartDelayedBCISelection(6);
    }

    public void SetState(GameState newState)
    {
        gameState = newState;
        cameraManager.SwitchToVCam2();
    }


    void CreateOrder()
    {
        for (int i = 0; i < 4; i++)
        {
            order.Add(toppings[Random.Range(0, toppings.Length)]);
        }
        foreach (string o in order)
        {
            Debug.Log("Topping: " + o);
        }

        StartCoroutine(uIManager.DrawOrder());
    }

    void CreateGame()
    {

        int location = Random.Range(1, 3);
        //instantiate objects at tray
        objectManager.SetTray(order[orderIndex], location);
        
        selectionManager.SetCorrectSelection(location);
        Debug.Log(location);
        gameState = GameState.INPUT;
    }

    // Update is called once per frame
    void Update()
    {

        

        if (gameState == GameState.INPUT && gameNum == GameNum.one)
        {

            selectionManager.ReadInput();
        }

        if (gameNum == GameNum.two)
        {

            selectionManager.ReadInput2();
            gameState = GameState.SCENE;
            

        }



    }
    
    public IEnumerator Win()
    {

        if (gameNum == GameNum.two) {
            Debug.Log("Nice!");
            orderIndex++;
        } else
        {
            if (gameState == GameState.INPUT)
            {
                Debug.Log("Correct");
                uIManager.Correct(orderIndex);
                orderIndex++;
                gameState = GameState.SCENE;
            }


            objectManager.ClearTray();

            //yield on a new YieldInstruction that waits for 5 seconds.
            yield return new WaitForSeconds(2);

            if (order.Count > orderIndex)
            {
                CreateGame();
            }
            else
            {
                gameState = GameState.NEXT;
                cameraManager.SwitchToVCam3();
                orderIndex = 0;
                gameNum = GameNum.two;
                SetArrowState();
                StartCoroutine(uIManager.DrawOrder());
                //play finished
                //switch scene
            }
        }

        StartDelayedBCISelection();
    }


    public void Drop()
    {
        if (orderIndex >= order.Count)
        {
            return;
        }
        
        var topping = order[orderIndex];
        StartCoroutine(objectManager.SpawnTopping(topping));
    }

    private void SetArrowState()
    {
        LeftArrow.gameObject.SetActive(gameNum == GameNum.one);
        RightArrow.gameObject.SetActive(gameNum == GameNum.one);
        DownArrow.gameObject.SetActive(gameNum == GameNum.two);
    }

    public void StartDelayedBCISelection(float seconds = 1)
    {
        StartCoroutine(DelayedBCISelection(seconds));
    }
    
    private IEnumerator DelayedBCISelection(float seconds = 1)
    {
        yield return new WaitForSeconds(seconds);
        BCIController.Instance.StartStopStimulus();
    }
}
