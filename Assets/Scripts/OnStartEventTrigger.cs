using UnityEngine;
using UnityEngine.Events;

public class OnStartEventTrigger : MonoBehaviour
{
    public UnityEvent OnStartEvent;
    
    void Start()
    {
        OnStartEvent?.Invoke();
    }
}
