using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    [Header("Displacement")]
    public float rotationSpeed = 1f;
    public float translationSpeed = 1f;
    public float minDistanceTarget = 15f;
    public float maxDistanceTarget = 16f;

    // Displacement state
    protected Quaternion targetRotation;
    protected Vector3 targetPosition;

    // Status
    protected CameraState state;

    // Target
    protected GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        state = CameraState.FOLLOW_TARGET;
    }

    // In the current version of the project characters travel in groups
    // this is convenient to avoid camera split
    public void SetTarget(GameObject target)
    {
        this.target = target;
        state = CameraState.FOLLOW_TARGET;
    }

    // Update is called once per frame
    void Update()
    {
        if(state == CameraState.FOLLOW_TARGET)
        {
            FollowTarget();
        }
        
    }

    public void SetState(CameraState newState)
    {
        state = newState;
    }

    public void FollowTarget()
    {
        if (!ReachedTarget())
        {
            targetRotation = Quaternion.LookRotation(target.transform.position - transform.position);

            float xOffset = (float)Math.Cos(Mathf.Deg2Rad * 20) * (minDistanceTarget + maxDistanceTarget) / 2;
            float yOffset = (float)Math.Sin(Mathf.Deg2Rad * 20) * (minDistanceTarget + maxDistanceTarget) / 2;
            targetPosition = target.transform.position + new Vector3(-xOffset, yOffset, 0);

            // Smoothly rotate towards the target point
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            // Smoothly translate towards the target point
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, translationSpeed * Time.deltaTime);
        }
    }

    public bool ReachedTarget()
    {
        if(target == null) {
            return true; }

        float distance = ComputeDistance(target.transform.position);
        return (Quaternion.Angle(transform.rotation, targetRotation) < 1) &&
            (minDistanceTarget <= distance && distance <= maxDistanceTarget);
    }

    public float ComputeDistance(Vector3 targetPosition)
    {
        float target_x = targetPosition.x;
        float target_y = targetPosition.y;
        float this_x = transform.position.x;
        float this_y = transform.position.y;
        return (float) Math.Sqrt(Math.Pow(target_x - this_x, 2) + Math.Pow(target_y - this_y, 2));
    }
}


public enum CameraState
{
    FREE,
    FOLLOW_TARGET
}

