using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCounter
{
    public int Score { get { return _score; } }

    private static ScoreCounter instance = null;

    private int _score;

    private ScoreCounter()
    {
    }

    public static ScoreCounter Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new ScoreCounter();
            }
            return instance;
        }
    }

    public void RemoveFromScore(int value)
    {
        _score -= value;
    }

    public void AddToScore(int value)
    {
        _score += value;
    }

    public void Reset()
    {
        _score = 0;
    }
}
