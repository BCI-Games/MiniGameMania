using System.Collections.Generic;
using UnityEngine;

// This function sets up a P300 for a list of objects

public class P300_Object_Setup : MonoBehaviour
{
    public List<SPO> objectList;

    private void Start()
    {
        InstantiateObjects(objectList);
    }
    // Setup the objects
    public void InstantiateObjects(List<SPO> objects)
    {
        //Initial set up
        foreach (SPO spoObj in objects)
        {
            //turn off
            spoObj.GetComponent<SPO>().TurnOff();

            //activating the object
            spoObj.gameObject.SetActive(true);
        }
    }

    //Destroy the objects
    public void DestroyObjects(List<SPO> objects)
    {
        foreach (SPO spoObj in objects)
        {
            Destroy(spoObj.gameObject);
        }
    }
}