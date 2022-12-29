using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnTriggerTagCallEvent : MonoBehaviour
{
    [SerializeField] public UnityEvent TriggerTagEvent = new UnityEvent();

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "Wall")
        {
            TriggerTagEvent.Invoke();
        }
    }
}
