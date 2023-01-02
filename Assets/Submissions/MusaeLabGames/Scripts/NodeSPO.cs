using System;

namespace Submissions.MusaeLabGames
{
    public class NodeSPO : SPO
    {
        private PosNodeScript _parentNode;
        
        private void OnEnable()
        {
            includeMe = true;
        }

        private void OnDisable()
        {
            includeMe = false;
        }

        public void AssignPosNode(PosNodeScript parent)
        {
            _parentNode = parent;
        }
        
        public override void OnSelection()
        {
            TurnOff();
            
            _parentNode.SelectTarget(gameObject);
        }
    }
}