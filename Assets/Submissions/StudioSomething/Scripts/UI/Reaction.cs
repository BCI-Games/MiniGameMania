using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reaction : MonoBehaviour
{
    public float TimeToDie = 5f;
    private float timer = 0;
    public float Speed = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timer >= TimeToDie)
        {
            Destroy(gameObject);
        }
        transform.position = transform.position + Vector3.up * Speed;
        timer+=Time.deltaTime;
    }
}
