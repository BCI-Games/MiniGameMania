using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHitBallEvent : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private float volume = 1.0f;

    [SerializeField] private float minVolume = 0.2f;
    [SerializeField] private BallContoller _ballContoller;

    private void Awake()
    {
        _ballContoller.onPuttBallEvent.AddListener(EventPlaySound);
    }

    public void EventPlaySound(float intensity)
    {
        float newVolume = Mathf.Lerp(minVolume, 1.0f, intensity);

        _audioSource.volume = newVolume;
        _audioSource.Play();
    }
}
