using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    public int rowIndex;
    public int colIndex;

    public void MoveTo(Vector3 pos, Action moveComplete)
    {
        StartCoroutine(SmoothLerp(pos, 3f, moveComplete));
    }
    private IEnumerator SmoothLerp(Vector3 destination, float time, Action moveComplete)
    {
        Vector3 startingPos = transform.position;
        Vector3 finalPos = destination;
        float elapsedTime = 0;

        while (elapsedTime < time)
        {
            transform.position = Vector3.Lerp(startingPos, finalPos, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        moveComplete.Invoke();
    }
}
