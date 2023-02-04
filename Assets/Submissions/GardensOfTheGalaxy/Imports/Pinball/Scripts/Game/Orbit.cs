using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{
    public GameObject targetObject;
    public float spinSpeed;
    public float orbitSpeed;

    public bool active;
    // Update is called once per frame
    void Update()
    {
        if (!active) return;

        // spin
        transform.Rotate(new Vector3(0, spinSpeed, 0) * Time.deltaTime);

        if (targetObject == null) return;

        // orbit around target object
        transform.RotateAround(targetObject.transform.position, new Vector3(0, 1, 0), orbitSpeed * Time.deltaTime);

    }
}
