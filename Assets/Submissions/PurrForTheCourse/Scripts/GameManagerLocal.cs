using HelloWorld;
using System.Collections;
using System.Collections.Generic;
using Submissions.PurrForTheCourse;
using UnityEngine;
using UnityEngine.Events;

public class GameManagerLocal : MonoBehaviour
{


    //public static int mode = 1;
    //public static bool useGui = true;
    public static int turnCounter = 0;
    public GameObject prefab;

    public Transform TransformToLookAtAfterTurnEnds;
    Cinemachine.CinemachineVirtualCamera cinemachineVirtualCamera;

    float NextTurnTime;
    // The duration of each turn in turn-based mode
    public float TurnDuration = 3;
    public static float timeRemaining;
    public static int NextPlayerIndex = 0;
    public int gameWinnerIndex = -1;
    public static int numOfPlayersDone = 0;
    public int mode;
    public int currentHole=1;
    public float speedPerFrame = 0.2f;

    public AudioSource mainAudioSource;
    public AudioClip TitleScreenClip;
    public AudioClip MainAmbientClip;

    int CurrentPlayerIndex
    {
        get
        {
            return (NextPlayerIndex + players.Count - 1) % players.Count;
        }
    }
    public static List<GameObject> players = new List<GameObject>();
    public static List<GameObject> playersOriginal = new List<GameObject>();

    public UnityEvent ButtonClicked = new UnityEvent();

    private void Start()
    {
        mode = 1;
        cinemachineVirtualCamera = GameObject.Find("orbitingIntroCam").GetComponent<Cinemachine.CinemachineVirtualCamera>();
        cinemachineVirtualCamera.Priority = 10;

        if (mainAudioSource != null)
        {
            mainAudioSource.clip = TitleScreenClip;
            mainAudioSource.loop = true;
            mainAudioSource.Play();
        }
    }

    public void updateCamPosition()
    {
        cinemachineVirtualCamera.GetCinemachineComponent<Cinemachine.CinemachineTrackedDolly>().m_PathPosition += speedPerFrame * Time.deltaTime;
    }
    //Start with GUI options in loading screen
    void OnGUI()
    {
        if (mode == 1)
        {
            //Draw all GUI objects

            GUILayout.BeginArea(new Rect(10, 10, 300, 300));
            
            
                StatusLabels();

                SubmitNewPosition();
            

            GUILayout.EndArea();
        }
        if(mode == 4)
        {
            GUILayout.BeginArea(new Rect(10, 10, 300, 300));
            levelDoneGUI();
            GUILayout.EndArea();

        }
    }


    public void levelDoneGUI()
    {

        if(gameWinnerIndex != -1) //if this was set (ie multiplayer version)
        {
            GUILayout.Label("Player " + gameWinnerIndex + " Won!");
        }

        int i = 1;
        foreach (GameObject p in playersOriginal)
        {
            GUILayout.Label("Player " +  i + " took  " + p.GetComponent<PlayerMovementLocal>().getTurnCount() + " turns!");
            i += 1;
        }

        if (GUILayout.Button("Add a player!"))
        {
            AddNewPlayer();
            ButtonClicked.Invoke();

        }
        if (GUILayout.Button("Play Again?"))
        {
            int playerCount = playersOriginal.Count;
            for (int j = 0; j < playersOriginal.Count; j++)
            {
                Destroy(playersOriginal[j].gameObject);
            }
            players.Clear();
            playersOriginal.Clear();
            for (int j = 0; j < playerCount; j++)
            {
                AddNewPlayer();
                ButtonClicked.Invoke();
            }
            mode = 2;

        }

        if (GUILayout.Button("Play Next Hole!"))
        {
            int playerCount = playersOriginal.Count;
            for (int j = 0; j < playersOriginal.Count; j++)
            {
                Destroy(playersOriginal[j].gameObject);
            }
            players.Clear();
            playersOriginal.Clear();
            for (int j = 0; j < playerCount; j++)
            {
                AddNewPlayer();
                ButtonClicked.Invoke();
            }
            if ( currentHole == 5) 
            {
                currentHole = 0;
            }
            currentHole += 1;
            mode = 2;

        }



    }


    static void StatusLabels()
    {
       

        
            GUILayout.Label("Number of players ready: " + players.Count);

            System.String temp2 = "";
            foreach (GameObject p in players)
            {
                temp2 += " " + p.GetInstanceID();



            }
            GUILayout.Label("Players: " + temp2);

        

    }

    public void SubmitNewPosition()
    {
        if (GUILayout.Button("Add a player!"))
        {
            AddNewPlayer();
            ButtonClicked.Invoke();

        }
        if (GUILayout.Button("Start Game!"))
        {
            mode = 2;
            ButtonClicked.Invoke();
        }



    }

    public void initialPositions()
    {
        //Just place em a bit apart for now and get the cameras going.
        //placing
        cinemachineVirtualCamera.Priority = 0;

        int i = 0;
        gameWinnerIndex = -1;
        System.String temp2 = "Course" + currentHole;
        GameObject originalGameObject = GameObject.Find("Course" + currentHole);
        TransformToLookAtAfterTurnEnds = GameObject.Find("Hole" + currentHole).transform;
        Vector3 startPos = GetHoleStartPosition();
        Vector3 p1Pos = startPos;

        foreach (GameObject p in playersOriginal)
        {

            Vector3 playerLoc = startPos + (new Vector3(i, 0f, 0f));
            p.GetComponent<PlayerMovementLocal>().MoveAbsolute(playerLoc);



            //updateCamera(p.gameObject);
            i += 2;

        }

        Camera.main.GetComponent<Camera>().gameObject.transform.position = new Vector3(p1Pos.x, p1Pos.y + 4, p1Pos.z - 10);
        Camera.main.GetComponent<Camera>().transform.LookAt(p1Pos);


        // Set the next turn time and player index
        NextTurnTime = Time.time + TurnDuration;
        timeRemaining = 0;
        mode = 3;

        mainAudioSource.clip = MainAmbientClip;   
        mainAudioSource.Play();



    }

    public Vector3 GetHoleStartPosition()
    {
        return GameObject.Find("Course" + currentHole).transform.position;
    }


    public void transitionCamerasAfterTurnEnds(int pIndex)
    {
        // TODO: change active virtual camera to new player
        Camera.main.GetComponent<Camera>().transform.LookAt(players[pIndex].transform);

    }

    public void levelDone()
    {

    }
    public void gameLoop()
    {

        turnCounter = 109;

        if (timeRemaining <= 0)
        {
            turnCounter += 1;
            // It is time to switch turns
            // Set the next player's turn
            if (players.Count == 0)
            {
                AddNewPlayer();
            }
            
            if (players.Count == 1)
            {
                players[0].GetComponent<PlayerMovementLocal>().SubmitTurnRequest(false);
                if (players[0].GetComponent<PlayerMovementLocal>().getIsInHole())
                {
                    //last player finally got it in!
                    mode = 4;
                }
                else
                {
                    players[0].GetComponent<PlayerMovementLocal>().SubmitTurnRequest(true);
                    transitionCamerasAfterTurnEnds(NextPlayerIndex);
                }
              
            }
            else
            {
                

                // Set the current player's turn to false
                players[CurrentPlayerIndex].GetComponent<PlayerMovementLocal>().SubmitTurnRequest(false);
                if(players[CurrentPlayerIndex].GetComponent<PlayerMovementLocal>().getIsInHole())
                { //take care, someone just got it in! 
                    if(gameWinnerIndex == -1)
                    {
                        gameWinnerIndex = CurrentPlayerIndex + 1;
                    }
                    players.RemoveAt(CurrentPlayerIndex);
                    if(NextPlayerIndex >=1)
                    {
                        //Subtract to make up for lost element
                        NextPlayerIndex -= 1;
                    }
                   
                   
                }

                transitionCamerasAfterTurnEnds(NextPlayerIndex);
                players[NextPlayerIndex].GetComponent<PlayerMovementLocal>().SubmitTurnRequest(true);
                
            }
            // Set the next turn time and player index
            timeRemaining = TurnDuration;
            
            NextPlayerIndex = (NextPlayerIndex + 1) % players.Count;

        }
        else if (players[CurrentPlayerIndex].GetComponent<PlayerController>().getIsHit())
        {
            //Debug.Log("Player is hitting the ball: " + CurrentPlayerIndex);
        }
        else
        {
            if(players.Count != 1)
            {//dont give time limit if only 1 player!
                timeRemaining -= Time.deltaTime;
            }
        }
    }

    public static void SetTimeRemainingZero()
    {
        timeRemaining = 0.0f;
    }

    public Transform getTransformToLookAtAfterTurnEnds()
    {
        // TODO: Get jazzes jizz
        return TransformToLookAtAfterTurnEnds;
    }

    // Update is called once per frame
    void Update()
    {
        if (mode ==1)
        {
            updateCamPosition();
        }
        if (mode == 2)
        {

            //setup players
            initialPositions();
        }

        if (mode ==3)
        {
            //Play the game!
            gameLoop();
        }

        if(mode ==4 )
        {
            //Game has finished!
            levelDone();
        }
        
    }

    private void AddNewPlayer()
    {
        var newPlayer = SpawnPlayer();
        newPlayer.GetComponent<PlayerMovementLocal>().GameManager = this;
        
        players.Add(newPlayer);
        playersOriginal.Add(newPlayer);
    }
    
    private GameObject SpawnPlayer()
    {
        return Instantiate(prefab, new Vector3(players.Count*4, 0, 0), Quaternion.identity);
    }
}
