using UnityEditor;
using UnityEngine;

namespace BCIEssentials.Controllers.Editor
{
    [CustomEditor(typeof(BCIController))]
    public class BCIControllerDebugInspector : UnityEditor.Editor
    {
        private BehaviorType _requestedBehavior;
        
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            GUILayout.Space(20);
            EditorGUILayout.TextField("Debug Controls", EditorStyles.boldLabel);
            _requestedBehavior =  (BehaviorType)EditorGUILayout.EnumPopup("Behavior Type:", _requestedBehavior);
            if (GUILayout.Button("Request Behavior"))
            {
                var controller = (BCIController)target;
                controller.ChangeBehavior(_requestedBehavior);
            }
        }
    }
}