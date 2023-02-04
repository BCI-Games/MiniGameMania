using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetInteraction : MonoBehaviour
{
    public AudioSource soundEffect;
    public AudioSource pointSoundEffect;
    public Light glow;
    public int pointValue;
    
    private void OnCollisionEnter(Collision collision)
    {
        soundEffect.Play();

        if (glow.gameObject.activeSelf) return;

        if (collision.gameObject.CompareTag("Ball"))
        {
            PointManager.Instance.totalPoints += pointValue;
            PointManager.Instance.numGlow++;
            glow.gameObject.SetActive(true);
            pointSoundEffect.Play();
        }
    }

    public void ResetGlow()
    {
        glow.gameObject.SetActive(false);
    }
}
