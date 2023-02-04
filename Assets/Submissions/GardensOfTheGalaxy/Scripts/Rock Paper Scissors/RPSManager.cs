using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RPSManager : MonoBehaviour
{
    public TextMeshPro player1DecisionText;
    public TextMeshPro player2DecisionText;
    public TextMeshPro winnerText;
    private List<string> rpsOptions = new List<string>();

    private void Start()
    {
        player1DecisionText.text = "";
        player2DecisionText.text = "";
        winnerText.text = "";

        rpsOptions.Add("Rock");
        rpsOptions.Add("Paper");
        rpsOptions.Add("Scissors");
    }

    public void GenerateAIDecision()
    {
        player2DecisionText.text = "Player 2 deciding...";
        winnerText.text = "";
        StartCoroutine(WaitToGenerate(1.5f));
    }

    IEnumerator WaitToGenerate(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        string p2decision = rpsOptions[Random.Range(0, rpsOptions.Count - 1)];
        player2DecisionText.text = "P2 Decision: " + p2decision;
        winnerText.text = DecideWinner();
    }

    private string DecideWinner()
    {
        if (player1DecisionText.text == "P1 Decision: Rock" && player2DecisionText.text == "P2 Decision: Rock")
            return "Tie!";
        else if (player1DecisionText.text == "P1 Decision: Rock" && player2DecisionText.text == "P2 Decision: Paper")
            return "P2 Wins!";
        else if (player1DecisionText.text == "P1 Decision: Rock" && player2DecisionText.text == "P2 Decision: Scissors")
            return "P1 Wins!";
        else if (player1DecisionText.text == "P1 Decision: Paper" && player2DecisionText.text == "P2 Decision: Rock")
            return "P1 Wins!";
        else if (player1DecisionText.text == "P1 Decision: Paper" && player2DecisionText.text == "P2 Decision: Paper")
            return "Tie!";
        else if (player1DecisionText.text == "P1 Decision: Paper" && player2DecisionText.text == "P2 Decision: Scissors")
            return "P2 Wins!";
        else if (player1DecisionText.text == "P1 Decision: Scissors" && player2DecisionText.text == "P2 Decision: Rock")
            return "P1 Wins!";
        else if (player1DecisionText.text == "P1 Decision: Scissors" && player2DecisionText.text == "P2 Decision: Paper")
            return "P2 Wins!";
        else if (player1DecisionText.text == "P1 Decision: Scissors" && player2DecisionText.text == "P2 Decision: Scissors")
            return "Tie!";

        return "NA";
    }
}
