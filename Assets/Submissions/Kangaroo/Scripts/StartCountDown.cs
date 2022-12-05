using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCountDown : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject gameStart; 
    [SerializeField]
    private float CountTime = 5f;
    [SerializeField]
    private float CountDown = 0f;

    //BCI Games Edit


    void Start()
    {
        CountDown = CountTime;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Time:" + Time.deltaTime.ToString());
        if(CountDown > 0){
        CountDown -= time.deltaTime;}
        else{
            gameStart.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }
}
