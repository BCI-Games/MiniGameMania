using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideOnDelay : MonoBehaviour
{
    public void Hide(float delay)
    {
        Invoke("Hide", delay);
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
