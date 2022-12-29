using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMultipleSounds : MonoBehaviour
{
    [SerializeField] private List<AudioSource> _audioSource;
    [SerializeField] private float volume = 1.0f;

    private float time = 0.5f;


    public void PlayAllSounds()
    {
        StartCoroutine(delayThenPlay());
    }

    private IEnumerator delayThenPlay()
    {
        yield return new WaitForSeconds(time);

        if (_audioSource.Count > 0)
        {
            foreach (var item in _audioSource)
            {
                item.volume = volume;
                item.Play();
            }
        }
    }
}
