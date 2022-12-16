using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderPathMovement : MonoBehaviour
{

    public GameObject obj;
    public GameObject[] pathPoints;
    public int numPoints;
    public float speed;

    private Vector3 actualPosition;
    private int i = 1;
    private bool loop = true;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        actualPosition = obj.transform.position;
        obj.transform.position = Vector3.MoveTowards(actualPosition, pathPoints[i].transform.position, speed * Time.deltaTime);

        if (actualPosition == pathPoints[i].transform.position && i != numPoints - 1)
        {
            i++;
        }

        if (loop && actualPosition == pathPoints[numPoints - 1].transform.position)
        {
            i = 0;
        }
    }
}
