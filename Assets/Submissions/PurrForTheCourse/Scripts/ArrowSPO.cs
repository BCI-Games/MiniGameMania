using System;
using UnityEngine.Events;

namespace Submissions.PurrForTheCourse
{
    public class ArrowSPO : SPO
    {
        public UnityEvent<SPO> OnSelectionEvent = new UnityEvent<SPO>();

        private void OnEnable()
        {
            includeMe = true;
        }

        private void OnDisable()
        {
            includeMe = false;
        }

        public override void OnSelection()
        {
            OnSelectionEvent?.Invoke(this);
        }
    }
}
