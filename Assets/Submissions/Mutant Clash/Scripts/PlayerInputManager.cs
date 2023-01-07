using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInputManager : MonoBehaviour
{
    public float unitDisplayTime;
    public float placementDisplayTime;

    public TimerBar timerDisplay;
    public Image delayImage;

    public Vector2[] lanePositions;

    public Color[] possibleColors;
    public Color playerColour;

    public bool moveLeft = false;

    public enum PlayerType { Bci, Mouse, AI}
    public PlayerType playerType;

    public float startingOffset;
    public float unitCostMultiplier = 3;

    UnitSelector unitSelector;
    UnitPlacer unitPlacer;

    P300Controller bciController;
    BattleManager battleManager;

    float selectionDelay;

    GameObject selectedUnitPrefab;
    int selectedLane;
    float timer;

    public enum State
    {
        ShowingUnits,
        SelectingUnits,
        ShowingPlacements,
        SelectingPlacement,
        Battle
    }
    public State state;


    private void OnDrawGizmos()
    {
        foreach (Vector2 pos in lanePositions)
        {
            Gizmos.DrawRay(pos, moveLeft ? Vector3.left : Vector3.right);
        }
    }

    void Start()
    {
        playerColour = possibleColors[Random.Range(0, possibleColors.Length)];

        //This doesn't work on scene load!!!! Find object of type is broken on scene load. Maybe I will switch how the game manager/controller works....
        bciController = FindObjectOfType<P300Controller>();
        battleManager = FindObjectOfType<BattleManager>();

        unitSelector = GetComponentInChildren<UnitSelector>();
        unitSelector.Init();
        unitSelector.SetUnitColour(playerColour);
        unitSelector.onComplete = OnUnitSelected;

        unitPlacer = GetComponentInChildren<UnitPlacer>();
        unitPlacer.Init();
        unitPlacer.onComplete = OnPlacementSelected;

        selectionDelay = startingOffset;

        delayImage.color = playerColour;
        ShowDelay(selectionDelay);

        if (selectionDelay <= 0)
            ShowUnits();


    }

    void Update()
    {
        Debug.Log(bciController.isActiveAndEnabled);
        if(timer > 0)
        {
            timer -= Time.deltaTime;

            if(timer <= 0)
            {
                switch (state)
                {
                    case State.ShowingUnits:
                        StartSelection(State.SelectingUnits, unitSelector);
                        break;
                    case State.ShowingPlacements:
                        StartSelection(State.SelectingPlacement, unitPlacer);
                        break;
                }
            }
        }
        else if (BattleManager.active && selectionDelay > 0)
        {
            // do battle stuff
            selectionDelay -= Time.deltaTime;
            ShowDelay(selectionDelay);

            if(selectionDelay <= 0)
            {
                ShowUnits();
            }
        }
    }

    void StartBattle()
    {
        // spawn unit
        UnitBehavior unitInstance = Instantiate(selectedUnitPrefab).GetComponent<UnitBehavior>();
        unitInstance.transform.position = lanePositions[selectedLane];
        selectionDelay += unitInstance.stats.cost * unitCostMultiplier;
        ShowDelay(selectionDelay);
        unitInstance.Init(moveLeft, playerColour);

        // unpause
        state = State.Battle;
        battleManager.AddUnitToLane(unitInstance, selectedLane);
    }

    void OnUnitSelected(GameObject unitPrefab)
    {
        selectedUnitPrefab = unitPrefab;

        ShowDelay(unitPrefab.GetComponent<UnitBehavior>().stats.cost * unitCostMultiplier);

        ShowPlacements();
    }

    void OnPlacementSelected(int laneIndex)
    {
        selectedLane = laneIndex;

        StartBattle();
    }

    void ShowUnits()
    {
        // pause unit actions
        BattleManager.active = false;

        // for mouse player skip showing, go right to selection
        if(playerType == PlayerType.Mouse)
        {
            state = State.SelectingUnits;
            return;
        }

        state = State.ShowingUnits;
        timer = unitDisplayTime;


        // show units and timer
        timerDisplay.StartTimer(unitDisplayTime);
        unitSelector.SetActive(true);
    }

    void ShowPlacements()
    {
        // for mouse player skip showing, go right to selection
        if (playerType == PlayerType.Mouse)
        {
            state = State.SelectingUnits;
            return;
        }

        state = State.ShowingPlacements;
        timer = placementDisplayTime;

        // show placements and timer
        timerDisplay.StartTimer(placementDisplayTime);
        unitPlacer.SetActive(true);
    }

    void StartSelection<T>(State newState, SPOSelectionManager<T> selectionManager)
    {
        state = newState;

        if (playerType == PlayerType.Bci)
        {
            selectionManager.TurnOffAll();
            bciController.StartStopStimulus();
        }
        else if (playerType == PlayerType.AI)
        {
            selectionManager.SelectRandom();
        }
    }

    void ShowDelay(float delay)
    {
        delayImage.fillAmount = delay / (4 * unitCostMultiplier);
    }
}
