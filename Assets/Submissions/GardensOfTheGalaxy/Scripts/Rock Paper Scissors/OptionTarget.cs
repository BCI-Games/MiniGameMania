using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionTarget : MonoBehaviour
{
    public string optionName;

    private Renderer renderer;
    private bool hovered = false;
    private RPSManager rpsManager;


    private void Start()
    {
        renderer = GetComponent<Renderer>();
        rpsManager = GameObject.Find("RPS Manager").GetComponent<RPSManager>();
    }


    private void OnMouseEnter()
    {
        hovered = true;
        renderer.material.color = Color.green;
    }

    private void OnMouseDown()
    {
        renderer.material.color = Color.yellow;
        SelectOption();
    }

    private void OnMouseUp()
    {
        if (hovered) { renderer.material.color = Color.green; }
        else if (!hovered) { renderer.material.color = Color.white; }
    }

    private void OnMouseExit()
    {
        hovered = false;
        renderer.material.color = Color.white;
    }

    public void SelectOption()
    {
        rpsManager.player1DecisionText.text = "P1 Decision: " + optionName;
        rpsManager.GenerateAIDecision();
    }
}
