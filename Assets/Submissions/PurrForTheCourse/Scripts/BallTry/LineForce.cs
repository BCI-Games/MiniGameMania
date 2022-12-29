using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineForce : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;

    private void DrawLine(Vector3 worldPoint)
    {
        Vector3[] positions = 
        {
            transform.position,
            worldPoint
        };

        lineRenderer.SetPositions(positions);
        lineRenderer.enabled = true;
    }

    private Vector3? CastMouseClickRay()
    {
        Vector3 screenMousePosFar = new Vector3();
        return null;
    }
}
