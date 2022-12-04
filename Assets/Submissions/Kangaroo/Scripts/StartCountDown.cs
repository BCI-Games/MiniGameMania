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
        if(CountDown > 0){
        CountDown -= Time.deltaTime;}
        else{
            gameStart.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }
}
