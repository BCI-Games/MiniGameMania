using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipperInteraction : MonoBehaviour
{
    public AudioSource soundEffect;

    public float restPosition = 0f;
    public float pressedPosition = 45f;
    public float hitStrength = 5000f;
    public float flipperDamper = 150f;

    HingeJoint hinge;
    public string inputName;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            soundEffect.Play();
        }
    }
    void Start()
    {
        hinge = GetComponent<HingeJoint>();
        hinge.useSpring = true;
    }

    void Update()
    {
        JointSpring spring = new JointSpring();
        spring.spring = hitStrength;
        spring.damper = flipperDamper;
        if (Input.GetAxis(inputName) == 1)
        {
            spring.targetPosition = pressedPosition;
        }
        else
        {
            spring.targetPosition = restPosition;
        }
        hinge.spring = spring;
        hinge.useLimits = true;
    }
}
