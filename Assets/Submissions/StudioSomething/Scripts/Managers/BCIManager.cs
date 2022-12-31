using System.Collections;
using System.Collections.Generic;
using BCIEssentials.Controllers;
using UnityEngine;

namespace Submissions.StudioSomething
{
    public class BCIManager : MonoBehaviour
    {
        public static BCIManager Instance;

        public IEnumerator BCICoroutine;

        void Awake()
        {
            if (Instance != null)
            {
                Destroy(this.gameObject);
            }
            else
            {
                Instance = this;
            }
        }

        void Start()
        {
            if (BCIController.Instance == null)
            {
                Debug.LogError("Couldn't find an instance of the BCI Controller");
                enabled = false;
            }
            else
            {
                BCIController.Instance.ChangeBehavior(BehaviorType.SSVEP);
            }
        }

        public IEnumerator BCICoFunction()
        {
            yield return new WaitForSeconds(4f);
            BCIController.Instance.StartStopStimulus();
        }
    }
}
