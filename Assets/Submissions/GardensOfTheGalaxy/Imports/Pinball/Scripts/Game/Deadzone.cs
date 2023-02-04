using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deadzone : MonoBehaviour
{
    public LaunchZone launchZone;
    public GameObject asteroidBelt;
    public AudioSource soundEffect;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            Destroy(other.gameObject);
            launchZone.SpawnBall();
            asteroidBelt.SetActive(false);
            soundEffect.Play();
            PointManager.Instance.totalPoints = 0;
            PointManager.Instance.ResetGlow();
        }
    }
}
