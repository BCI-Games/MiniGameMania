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

    void Start()
    {
        CountDown = CountTime;
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log("Hey! I am updating!");
        
        // if(Input.GetKeyDown(KeyCode.G))
        // {
        //     Debug.Log("Hey! I was pressed");
        //     Debug.Log("Time:" + Time.deltaTime.ToString());
        //     if(CountDown > 0){
        //     CountDown -= 0.001f;}
        //     else{
        //         gameStart.SetActive(true);
        //         this.gameObject.SetActive(false);
        //     }
        // }
        Debug.Log("Hey! I was pressed");
        Debug.Log("Time:" + Time.deltaTime.ToString());
        if(CountDown > 0){
        CountDown -= 0.01f;}
        else{
            gameStart.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }
}
