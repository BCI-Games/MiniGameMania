using UnityEngine;
using System.Collections;

public class RandomRotator : MonoBehaviour
{
    public AudioSource tapSoundEffect;
    [SerializeField]
    private float tumble;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            tapSoundEffect.Play();
        }
    }
    private Vector3 angularVelocity;
    void Start()
    {
        angularVelocity = Random.insideUnitSphere * tumble;
    }
    private void Update()
    {
        transform.Rotate(angularVelocity * Time.deltaTime);
    }
}