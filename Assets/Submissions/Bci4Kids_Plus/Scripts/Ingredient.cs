using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(AudioSource))]
public class Ingredient : MonoBehaviour
{

    // public GameObject gameManager;

    private Rigidbody2D _rigidbody;
    private AudioSource _audioSource;
    private bool sendDown = false;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // gameManager = GameObject.Find("GameManager");
    }
        // Update is called once per frame
    void Update()
    {
        if (sendDown == true)
        {
            FallDown();
        }
        if (transform.position is { x: > 6, y: > 6 }) {
            Destroy(gameObject);
        }
    }

    private void OnMouseDown()
    {
        if (transform.position.y > 7 && transform.position.x > -6.5 && transform.position.x < 5)
        {
            sendDown = true;
            _audioSource.Play();
            _rigidbody.gravityScale = 1;

            // Delete path child
            Destroy(transform.GetChild(0).gameObject);

            // gameObject.AddComponent<Rigidbody2D>();
        }
    }
    private void FallDown()
    {     
        if (transform.position.y < -5)
        {
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.gravityScale = 0;
            _rigidbody.isKinematic = true;
            
            sendDown = false;

            // Remove the ridigbody component
            // Destroy(gameObject.GetComponent<Rigidbody2D>());

            // Set mass to 0
            // gameObject.GetComponent<Rigidbody2D>().mass = 0;
            // gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
        }

    }


}
