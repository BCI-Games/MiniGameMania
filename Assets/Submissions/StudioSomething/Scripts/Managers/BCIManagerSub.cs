using System.Collections;
using System.Collections.Generic;
using BCIEssentials.Controllers;
using UnityEngine;

namespace Submissions.StudioSomething
{
    // I had to change the name of this, as a .dll from BrainBuddy broke the BCIManager name. Will fix implementations elsewhere.
    public class BCIManagerSub : MonoBehaviour
    {
        public static BCIManagerSub Instance;

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
