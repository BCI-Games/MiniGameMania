using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BCIManager : MonoBehaviour
{
    public static BCIManager Instance;

    public SSVEPController Controller;
    public IEnumerator BCICoroutine;
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        if (Controller == null)
        {
            Controller = GameObject.FindObjectOfType<SSVEPController>();
        }
    }

    public IEnumerator BCICoFunction()
    {
        yield return new WaitForSeconds(4f);
        Controller.StartStopStimulus();
    }
}
