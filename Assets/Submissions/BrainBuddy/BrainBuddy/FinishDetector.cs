using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishDetector : MonoBehaviour
{
    public Camera MainCamera;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Finished"))
        {
            FinishedController.Instance.FinishReached = true;
        }
    }
    void Update()
    {
        if(FinishedController.Instance.FinishReached)
        {
            MainCamera.orthographicSize = 5;
            MainCamera.transform.position = new Vector3(0,0,-10);
        }
    }
}

public class FinishedController
{
    public bool FinishReached {
        get { return _finishReached; }
        set { _finishReached = value; }
    }

    private static FinishedController instance = null;

    private bool _finishReached;

    private FinishedController()
    {
        _finishReached = false;
    }

    public static FinishedController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new FinishedController();
            }
            return instance;
        }
    }
}