using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{


    public GameObject vCam1;
    public GameObject vCam2;
    public GameObject vCam3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void SwitchToVCam2() { 


        vCam2.SetActive(true);
    }

    public void SwitchToVCam3()
    {


        vCam3.SetActive(true);
    }
}
