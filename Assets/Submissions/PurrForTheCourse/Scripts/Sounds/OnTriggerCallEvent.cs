using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnTriggerCallEvent : MonoBehaviour
{
    public UnityEvent eventToCall = new UnityEvent();

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("not getting called");

        eventToCall.Invoke();
    }
}
