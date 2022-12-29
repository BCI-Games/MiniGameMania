using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnEventPlaySound : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private float volume = 1.0f;



    public void EventPlaySound()
    {
        _audioSource.volume = volume;
        _audioSource.Play();
    }
}
