using BCIEssentials.Controllers;
using UnityEngine;

namespace DefaultNamespace
{
    public class InitializeTrainingEnvironment : MonoBehaviour
    {
        private void Start()
        {
            if (BCIController.Instance != null && BCIController.Instance.ActiveBehavior != null)
            {
                BCIController.Instance.ActiveBehavior.SetupMatrix();
            }
        }
        private void OnDestroy()
        {
            if (BCIController.Instance != null && BCIController.Instance.ActiveBehavior != null)
            {
                BCIController.Instance.ActiveBehavior.CleanUpMatrix();
            }
        }
    }
}