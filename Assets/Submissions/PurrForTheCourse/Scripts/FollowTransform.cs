using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTransform : MonoBehaviour
{
    [SerializeField] private Transform target;
    private Vector3 offset;
    [SerializeField] float smoothSpeed = 0.125f;

    [SerializeField] private float distanceBack;
    [SerializeField] private float distanceUp;

    private void Start()
    {
        offset.y = distanceUp;
        offset.z = -distanceBack;
        
    }

    

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        transform.position = desiredPosition;
    }
}
