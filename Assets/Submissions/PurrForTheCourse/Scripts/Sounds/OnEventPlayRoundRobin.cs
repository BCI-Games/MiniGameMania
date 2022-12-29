using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnEventPlayRoundRobin : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private List<AudioClip> _clips;
    private int index = 0;

    private Coroutine co;
    private bool isCoRunnning = false;

    private void Start()
    {
        index = Random.Range(0, _clips.Count);
    }

    public void RoundRobinPlaySound()
    {
        if (!isCoRunnning)
        {
            co = StartCoroutine(EnumPlaySOund());
        }
    }

    private IEnumerator EnumPlaySOund()
    {
        isCoRunnning = true;

        if (_clips[index] == null)
            Debug.Log("This clip is null");
        _audioSource.PlayOneShot(_clips[index]);

        Debug.Log("I'm playing sound rn");
        do
        {
            yield return null;
        } while (_audioSource.isPlaying);
        Debug.Log("No longer playing sound rn");

        index = (index + 1) % _clips.Count;
        isCoRunnning = false;
    }
}
