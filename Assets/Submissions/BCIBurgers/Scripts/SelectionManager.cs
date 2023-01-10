using System.Collections;
using System.Collections.Generic;
using BCIEssentials.Controllers;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{

    public GameManager gameManager;
    public ObjectManager objectManager;

    private int correctSelection;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {



        //Debug.Log("Input" + horizontalInput);
    }

    public void ReadInput()
    {

        if (Input.GetKey("left"))
        {
            Debug.Log("Pressed left");
            Select(1);
        }
        else if (Input.GetKey("right"))
        {
            Debug.Log("Pressed right");
            Select(2);
        }
    }

    private Coroutine _itemDropped;
    public void ReadInput2()
    {
        if (Input.GetKey("down") && _itemDropped == null)
        {
            SetDropped();
        }
    }

    public void SetDropped()
    {
        Debug.Log("Pressed left");
        _itemDropped = StartCoroutine(Drop());
    }


    public void SetCorrectSelection(int selection)
    {
        correctSelection = selection;
    }

    public void Select(float input)
    {
        if (input == correctSelection)
        {
            StartCoroutine(gameManager.Win());
        }
        else
        {
            gameManager.StartDelayedBCISelection();
        }
        
    }

    private IEnumerator Drop()
    {
        gameManager.Drop();
        StartCoroutine(gameManager.Win());

        yield return new WaitForSeconds(1);
        _itemDropped = null;
    }
}
