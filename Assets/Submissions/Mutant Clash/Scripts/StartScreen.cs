using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StartScreen : MonoBehaviour
{
    public PlayerInputManager player1;
    public PlayerInputManager player2;

    public TMP_Dropdown player1DropDown;
    public TMP_Dropdown player2DropDown;

    public void StartGame()
    {
        BattleManager.active = true;
        gameObject.SetActive(false);

        SelectPlayer1InputMethod(player1DropDown.value);
        SelectPlayer2InputMethod(player2DropDown.value);
    }

    public void SelectPlayer1InputMethod(int selection)
    {
        player1.playerType = (PlayerInputManager.PlayerType)selection;
    }

    public void SelectPlayer2InputMethod(int selection)
    {
        player2.playerType = (PlayerInputManager.PlayerType)selection;
    }
}
