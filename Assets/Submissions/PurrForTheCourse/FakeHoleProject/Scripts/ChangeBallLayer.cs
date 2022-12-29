using UnityEngine;
using System.Collections;

public class ChangeBallLayer : MonoBehaviour {

    public int LayerOnEnter; // BallInHole
    public int LayerOnExit;  // BallOnTable
	
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("helloooo ");
          if(other.gameObject.tag == "Player")
        {
            Debug.Log("helloooo ");
            other.gameObject.layer = LayerOnEnter;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.layer = LayerOnExit;
        }
    }
}