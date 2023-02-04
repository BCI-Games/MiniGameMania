using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MoveTowards : MonoBehaviour
{
    public GameObject initialWayPoint;
    public GameObject finalWayPoint;
    public UnityEvent onStart;
    public UnityEvent onComplete;

    public void StartMove()
    {
        onStart.Invoke();
        StartCoroutine(SmoothLerp(3f));
    }

    private IEnumerator SmoothLerp(float time)
    {
        Vector3 startingPos = initialWayPoint.transform.position;
        Vector3 finalPos = finalWayPoint.transform.position;
        float elapsedTime = 0;

        while (elapsedTime < time)
        {
            transform.position = Vector3.Lerp(startingPos, finalPos, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        onComplete.Invoke();
    }
}
