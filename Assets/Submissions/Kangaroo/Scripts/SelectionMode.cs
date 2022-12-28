using System.Collections;
using System.Collections.Generic;
using BCIEssentials.Controllers;
using UnityEngine;

public class SelectionMode : MonoBehaviour
{
    private bool canCount;
    public P300Controller P300;
    private float countTime = 2f;
    private float counter;

    private BCIControllerBehavior _bciController;

    public void Start()
    {
        if (BCIController.Instance == null)
        {
            _bciController = GameObject.FindGameObjectWithTag("MasterController")
                .GetComponent<BCIControllerBehavior>();
        }
    }

    private void OnEnable()
    {
        counter = countTime;
        canCount = true;
    }

    private void Update()
    {
        if (canCount)
        {
            if (counter > 0)
            {
                counter -= Time.deltaTime;
            }
            else
            {
                if (_bciController != null)
                {
                    _bciController.StartStopStimulus();
                }
                else if (BCIController.Instance != null)
                {
                    BCIController.Instance.StartStopStimulus();
                }

                canCount = false;
            }
        }
    }
}