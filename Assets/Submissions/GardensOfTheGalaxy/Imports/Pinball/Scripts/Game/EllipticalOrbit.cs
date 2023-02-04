using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EllipticalOrbit : MonoBehaviour
{
    private GameObject targetObject;

    private float a;
    private float b;
    private float speed;
    private float alpha;

    private float x;
    private float y;
    private float X;
    private float Y;

    private bool initialized = false;

    public void Initialize(float a, float b, float speed, float alpha, GameObject targetObject)
    {
        this.a = a;
        this.b = b;
        this.speed = speed;
        this.alpha = alpha;
        this.targetObject = targetObject;

        x = targetObject.transform.position.x;
        y = targetObject.transform.position.z;

        initialized = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!initialized)
        {
            Debug.LogError("EllipticalOrbit not initialized.");
            return;
        }

        alpha += speed * Time.deltaTime;
        X = x + (a * Mathf.Cos(alpha * .005f));
        Y = y + (b * Mathf.Sin(alpha * .005f));
        this.gameObject.transform.position = targetObject.transform.position + new Vector3(X, 0, Y);
    }


}
