using UnityEngine;
using UnityEngine.Events;

namespace Submissions.BoatRescue
{
    public class TriggerPassThrough : MonoBehaviour
    {
        public UnityEvent OnTriggerEntered;

        private void OnTriggerEnter(Collider collider)
        {
            OnTriggerEntered?.Invoke();
        }

        private void OnCollisionEnter(Collision collision)
        {
            OnTriggerEntered?.Invoke();
        }
    }
}