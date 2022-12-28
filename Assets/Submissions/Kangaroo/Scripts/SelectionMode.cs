using BCIEssentials.Controllers;
using UnityEngine;

public class SelectionMode : MonoBehaviour
{
    private bool canCount;
    private float countTime = 2f;
    private float counter;

    private void OnEnable()
    {
        counter = countTime;
        canCount = true;
    }

    private void Update()
    {
        if (canCount)
        {
            if (counter > 0)
            {
                counter -= Time.deltaTime;
            }
            else
            {
                if (BCIController.Instance != null)
                {
                    BCIController.Instance.StartStopStimulus();
                }

                canCount = false;
            }
        }
    }
}