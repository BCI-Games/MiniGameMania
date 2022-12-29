using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISoundListener : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private float volume = 1.0f;



    public void PlayUISound()
    {
        _audioSource.volume = volume;
        _audioSource.Play();
    }
}
