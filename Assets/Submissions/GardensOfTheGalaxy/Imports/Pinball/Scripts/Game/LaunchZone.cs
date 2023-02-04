using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaunchZone : MonoBehaviour
{
    public GameObject ballPref;

    public GameObject explosionEffectPref;
    public AudioSource soundEffect;

    private GameObject ballInstance = null;

    // Start is called before the first frame update
    void Start()
    {
        SpawnBall();
    }

    public void Launch(float force)
    {
        if (ballInstance == null) return;
        GameObject g = Instantiate(explosionEffectPref, transform);
        g.GetComponent<ParticleSystem>().Play();
        soundEffect.Play();
        ballInstance.GetComponent<Rigidbody>().AddForce(-transform.right * force);
        ballInstance = null;
    }

    public void SpawnBall()
    {
        ballInstance = Instantiate(ballPref, transform.position, transform.rotation);
    }
}
