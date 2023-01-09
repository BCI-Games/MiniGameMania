using UnityEngine;
using UnityEngine.Events;

namespace Submissions.BCIBurgers
{
    public class ArrowSPO : SPO
    {
        [SerializeField] private MeshRenderer[] _renderers;

        public UnityEvent OnSelectionEvent ;
        
        // Turn the stimulus on
        public override float TurnOn()
        {
            foreach (var meshRenderer in _renderers)
            {
                meshRenderer.material.color = onColour;
            }


            //Return time since stim
            return Time.time;
        }

        // Turn off/reset the SPO
        public override void TurnOff()
        {
            foreach (var meshRenderer in _renderers)
            {
                meshRenderer.material.color = offColour;
            }
        }

        public override void OnSelection()
        {
            base.OnSelection();
            OnSelectionEvent?.Invoke();
        }
    }
}