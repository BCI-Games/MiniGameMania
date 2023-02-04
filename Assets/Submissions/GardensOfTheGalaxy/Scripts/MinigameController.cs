using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameController : MonoBehaviour
{
    protected void OnGameEnd(WinState winState)
    {
        MainGameController.Instance.OnGameEnd(winState);
    }
}
