using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    private static BallManager _instance;

    public static BallManager Instance
    {
        get
        {
            return _instance;
        }
    }

    public AudioSource soundEffect;
    public AudioSource cancelSoundEffect;

    public GameObject targetObject = null;
    private GameObject ballInstance;
    private bool inOrbit = false;
    private float gravitationalForce = 5f;

    private void Awake()
    {
        _instance = this;
    }
    public void StartGame(GameObject ball)
    {
        ballInstance = ball;
    }
    public void StartOrbit()
    {
        inOrbit = true;
        soundEffect.Play();
    }
    public void EndOrbit()
    {
        inOrbit = false;
        cancelSoundEffect.Play();
    }

    private void FixedUpdate()
    {
        if (ballInstance == null || targetObject == null) return;

        if (inOrbit)
        {
            ballInstance.GetComponent<Rigidbody>().AddForce((targetObject.transform.position - ballInstance.transform.position).normalized * gravitationalForce, ForceMode.Acceleration);
        }
    }
}
