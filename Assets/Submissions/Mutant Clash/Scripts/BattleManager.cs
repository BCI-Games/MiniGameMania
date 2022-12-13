using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using System.Linq;
using System;

public class BattleManager : MonoBehaviour
{
    public static bool active = true;

    public float baseMoveSpeed = 1.5f;
    public float collisionDistance = 1;
    public float scoreDistance = 10;

    List<UnitBehavior>[] lanes;
    List<Battle> battles;

    ScoreBar scoreBar;
    WinScreen winScreen;

    int score;

    void Start()
    {
        lanes = new List<UnitBehavior>[3];
        for (int i = 0; i < 3; i++)
            lanes[i] = new List<UnitBehavior>();

        battles = new List<Battle>();

        scoreBar = GetComponentInChildren<ScoreBar>();
        scoreBar.SetScore(0);

        winScreen = FindObjectOfType<WinScreen>();
        winScreen.gameObject.SetActive(false);

        active = false;
    }


    void Update()
    {
        if (!active)
            return;

        MoveUnits();

        CheckForScoring();

        UpdateBattles();
    }

    void MoveUnits()
    {
        foreach (var lane in lanes)
            MoveUnitsInLane(lane);
    }

    void MoveUnitsInLane(List<UnitBehavior> lane)
    {
        Dictionary<UnitBehavior, UnitBehavior> unitCollisions = new Dictionary<UnitBehavior, UnitBehavior>();

        foreach (UnitBehavior unit in lane)
        {
            UnitBehavior collision = unit.MoveAndCollide(baseMoveSpeed, lane, collisionDistance);

            if (collision)
            {
                //print($"collision between {unit.name} and {collision.name}");
                if (unitCollisions.ContainsKey(collision) && unitCollisions[collision] == unit)
                {
                    bool battleExists = false;
                    foreach (Battle battle in battles)
                        battleExists |= battle.HasUnits(unit, collision);

                    // mutual collision, start battle
                    if (!battleExists)
                        battles.Add(new Battle(unit, collision, RemoveUnit));
                }
                unitCollisions[unit] = collision;
            }
        }
    }

    void CheckForScoring()
    {
        List<UnitBehavior> doomedUnits = new List<UnitBehavior>();

        foreach(var lane in lanes)
        {
            foreach(var unit in lane)
            {
                if(Mathf.Abs(unit.position.x) > scoreDistance)
                {
                    if (unit.position.x > 0)
                        score++;
                    else
                        score--;

                    if(Math.Abs(score) >= 7)
                    {
                        winScreen.OnWin(score);
                        active = false;
                    }

                    doomedUnits.Add(unit);

                    scoreBar.SetScore(score);
                }
            }
        }

        foreach(var unit in doomedUnits)
        {
            Destroy(unit.gameObject);
            RemoveUnit(unit);
        }
    }

    void UpdateBattles()
    {
        List<Battle> completedBattles = new List<Battle>();

        foreach(Battle battle in battles)
        {
            if (battle.Update())
            {
                completedBattles.Add(battle);
            }
        }

        foreach (Battle battle in completedBattles)
            battles.Remove(battle);
    }

    void RemoveUnit(UnitBehavior deadUnit)
    {
        foreach (var lane in lanes)
            lane.Remove(deadUnit);
    }

    public void AddUnitToLane(UnitBehavior unit, int lane)
    {
        active = true;

        lanes[lane].Add(unit);
        unit.transform.SetParent(transform);
    }
}
