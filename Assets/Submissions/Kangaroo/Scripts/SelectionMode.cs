using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionMode : MonoBehaviour
{
    private bool canCount;
    public P300Controller P300;
    private float countTime = 2f;
    private float counter;

    public void Start()
    {
        P300 = GameObject.FindGameObjectWithTag("MasterController").GetComponent<P300Controller>();
    }

    private void OnEnable() {
        counter = countTime;
        canCount = true;
    }

    private void Update() {
        
        if(canCount == true){
            if(counter > 0){
                counter -= Time.deltaTime;
            }
            else{
                P300.StartStopStimulus();
                canCount = false;
            }
        }
    }

}
