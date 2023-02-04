using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectLaunch : MonoBehaviour
{
    public GameObject asteroidBelt;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            asteroidBelt.SetActive(true);
            BallManager.Instance.StartGame(other.gameObject);
        }
    }
}
