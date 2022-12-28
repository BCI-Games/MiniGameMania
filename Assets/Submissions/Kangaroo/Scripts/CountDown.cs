using System.Collections;
using System.Collections.Generic;
using BCIEssentials.Controllers;
using UnityEngine;

public class CountDown : MonoBehaviour
{
    public TextMesh Ctext;
    public GameObject BG;
    public GameObject haveYouFound;

    [SerializeField]
    private float countTime = 3f;
    [SerializeField]
    private float countDown = 0f;

    [SerializeField]
    private float stiCountDown = 0f;
    
    private void Start()
    {
        countDown = countTime;
        stiCountDown = countTime + 5f;
    }
    
    private void OnEnable() {
        countDown = countTime;
        stiCountDown = countTime + 5f;     
    }
    

    // Update is called once per frame
    void Update()
    {
        if (countDown >= 0){
            countDown -= Time.deltaTime;
            Ctext.text = string.Format(countDown.ToString("00.0"));
        }
        else{

            BG.SetActive(false);
            haveYouFound.SetActive(true);

        }

        if (stiCountDown >= 0){
            stiCountDown -= Time.deltaTime;

        }
        else{

            if (BCIController.Instance != null)
            {
                BCIController.Instance.StartStopStimulus();
            }
            
            this.gameObject.SetActive(false);

        }


    }
}
