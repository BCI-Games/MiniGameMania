using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    // Game components
    [Header("Game Components")]
    public Camera cam;
    public GameObject guiObject;
    protected CameraBehavior cam_script;

    // Characters
    [Header("Characters")]
    public int nb_characters=1;
    public GameObject[] character_model;
    protected GameObject[] list_characters;

    // Character displacement
    [Header("Character displacement")]
    public GameObject pn_start;
    protected PosNodeScript pn_start_script;

    protected GameState state;

    // Mini game instance
    // TODO DEBUG: uncomment
    /*
    protected DestroyLogMiniGame destroy_log_minigame;
    protected CollectMushroomMiniGame mushroom_minigame;
    protected RobThiefMiniGame thief_minigame;
    protected DestroyWoodBoxMiniGame box_minigame;*/


    void Awake()
    {
        // Example: Preset gameObject colors
    }

    void Start()
    {
        pn_start_script = pn_start.GetComponent<PosNodeScript>();
        cam_script = cam.GetComponent<CameraBehavior>();
        EnterVillageState();
        //ResetTurn();
    }

    

    // Update is called once per frame
    void Update()
    {
        /*
        switch (state)
        {
            case GameState.Intro:
                PlayDialogIntro();
                break;

            case GameState.Village:
                PlayVillageState();
                break;

            case GameState.WaterMill:
                PlayDestroyLog();
                break;

            case GameState.Temple:
                //SceneManager.LoadScene("TempleIndoor");
                break;

            case GameState.Forest:
                //SceneManager.LoadScene("Forest");
                break;

            default:
                Debug.Log(state + "Not Implemented yet.");
                break;
        }*/
    }

    protected void PlayDialogIntro()
    {
        // TODO: Enable DialogIntro class

        // 
        state = GameState.Village;
    }

    protected void PlayDestroyLog()
    {
        // TODO next
        /*
        if(destroy_log_minigame.isDisabled())
        {
            destroy_log_minigame.Enable(list_characters);
        }
        else if(destroy_log_minigame.isFinished())
        {
            Inventory[] result = destroy_log_minigame.GetGameResult();
            AddNewInventoryPlayers();
            ResetLastPlayerPosition();
        }
        */
    }

    

    protected void EnterVillageState()
    {
        Debug.Log("Entering Village state");
        state = GameState.Village;

        PlaceCharacters();
        cam_script.SetTarget(list_characters[0]); // Follow first character by default
    }

    protected void PlaceCharacters()
    {
        Debug.Log("Placing characters");

        list_characters = new GameObject[nb_characters];
        for (int i = 0; i < nb_characters; i++)
        {
            GameObject new_player = Instantiate(character_model[i],
                                pn_start.transform.position,
                                Quaternion.identity);

            // TODO quick work: TODO improve characters alignement
            new_player.GetComponent<Character>().SetOffset(2* i * new_player.transform.right);
            list_characters[i] = new_player;
        }

        pn_start_script.ActivateNode();
    }

    public void OnEnable()
    {
        PosNodeScript.OnHasPlayer += CheckEntryMiniGame;
        PosNodeScript.OnRequestedDisplacement += MovePlayers;
    }

    public void OnDisable()
    {
        PosNodeScript.OnHasPlayer -= CheckEntryMiniGame;
        PosNodeScript.OnRequestedDisplacement -= MovePlayers;
    }

    protected void CheckEntryMiniGame(GameObject node)
    {
        if(node.tag == "MiniGamePos")
        {
            // TODO: launch minigame
        }
    }

    protected void MovePlayers(GameObject target)
    {
        Debug.Log("list_characters " + list_characters);
        for(int ix=0; ix<nb_characters; ix++) {
            GameObject character = list_characters[ix];

            // TODO quick work: use offset
            Vector3 offset = 10 * ix * target.transform.right;
            character.GetComponent<Character>().SetDestination(target, offset);
        }
    }
}


public enum GameState
{
    Intro,
    Village,
    Arena,
    WaterMill,
    Temple,
    MushroomGlade,
    Forest,
    MiniGameResultScene,
    DialogMode,
}


