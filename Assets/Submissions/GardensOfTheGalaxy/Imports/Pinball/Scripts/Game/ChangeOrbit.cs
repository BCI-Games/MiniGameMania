using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeOrbit : MonoBehaviour
{
    private void OnMouseDown()
    {
        BallManager.Instance.targetObject = this.gameObject;
        BallManager.Instance.StartOrbit();
    }
}
