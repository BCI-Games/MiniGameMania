using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Net.Sockets;

public class WinScreen : MonoBehaviour
{
    public TextMeshProUGUI winText;

    public PlayerInputManager positiveScorePlayerInput;
    public PlayerInputManager negativeScorePlayerInput;

    public void OnWin(int score)
    {
        string playerId;

        if (score > 0)
            playerId = GetNameForPlayer(positiveScorePlayerInput, 1);
        else
            playerId = GetNameForPlayer(negativeScorePlayerInput, 2);

        gameObject.SetActive(true);
        winText.text = playerId + "\nBig Winner!";
    }

    string GetNameForPlayer(PlayerInputManager player, int index)
    {
        if (player.playerType == PlayerInputManager.PlayerType.AI)
            return "AI";

        return "Player " + index;
    }
}
