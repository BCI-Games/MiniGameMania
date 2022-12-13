using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battle
{
    const float WindupTime = 0.5f;
    const float PushTime = 0.5f;

    UnitBehavior unit1;
    UnitBehavior unit2;

    BattleState state;
    float timer;

    Action<UnitBehavior> OnUnitDeath;

    enum BattleState
    {
        unit1Windup,
        unit1Push,
        unit2Windup,
        unit2Push
    }

    public Battle(UnitBehavior unit1, UnitBehavior unit2, Action<UnitBehavior> removeUnit)
    {
        this.unit1 = unit1;
        this.unit2 = unit2;
        OnUnitDeath = removeUnit;

        state = BattleState.unit1Windup;

        if (unit2.stats.speed > unit1.stats.speed)
            state = BattleState.unit2Windup;

        ApplyState();
    }

    public bool HasUnits(UnitBehavior a, UnitBehavior b)
    {
        return (unit1 == a && unit2 == b) ||
            (unit2 == a && unit1 == b);
    }

    public bool Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            if (++state > BattleState.unit2Push)
                state = 0;

            return ApplyState();
        }
        return false;
    }

    bool ApplyState()
    {
        switch (state)
        {
            case BattleState.unit1Windup:
                unit1.SetSprite(UnitSpriteState.Windup);
                unit2.SetSprite(UnitSpriteState.Idle);
                timer = WindupTime;

                return false;

            case BattleState.unit1Push:
                unit1.SetSprite(UnitSpriteState.Push);
                unit2.SetSprite(UnitSpriteState.Sit);
                timer = PushTime;

                if (unit2.TakeDamage())
                {
                    OnUnitDeath(unit2);
                    return true;
                }
                return false;

            case BattleState.unit2Windup:
                unit2.SetSprite(UnitSpriteState.Windup);
                unit1.SetSprite(UnitSpriteState.Idle);
                timer = WindupTime;

                return false;

            case BattleState.unit2Push:
                unit2.SetSprite(UnitSpriteState.Push);
                unit1.SetSprite(UnitSpriteState.Sit);
                timer = PushTime;

                if (unit1.TakeDamage())
                {
                    OnUnitDeath(unit1);
                    return true;
                }
                return false;
        }
        return false;
    }
}
