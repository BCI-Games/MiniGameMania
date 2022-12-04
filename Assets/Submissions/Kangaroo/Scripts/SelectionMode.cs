using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionMode : MonoBehaviour
{
    private bool canCount;
    public P300Controller P300;
    private float countTime = 2f;
    private float counter;

    private void OnEnable() {
        counter = countTime;
        canCount = true;
    }

    private void Update() {
        
        if(canCount == true){
            if(counter > 0){
                counter -= 0.01f;
            }
            else{
                P300.StartStopStimulus();
                canCount = false;
            }
        }
    }

}
