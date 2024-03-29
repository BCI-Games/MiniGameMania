using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using BCIEssentials.Networking;

namespace BCIEssentials.Controllers
{
    public partial class BCIController : MonoBehaviour
    {
        [SerializeField] private LSLMarkerStream _lslMarkerStream;
        [SerializeField] private LSLResponseStream _lslResponseStream;
        
        [Space]
        [SerializeField] private bool _dontDestroyActiveInstance = true;

        public static BCIController Instance { get; private set; }
        public BCIControllerBehavior ActiveBehavior { get; private set; }

        [Header("Keybinds")]
        public KeyCode startStopStimulusKeyCode = KeyCode.S;
        public KeyCode startAutomatedTrainingKeyCode = KeyCode.T;
        public KeyCode startIterativeTrainingKeyCode = KeyCode.I;
        public KeyCode startUserTrainingKeyCode = KeyCode.U;

        private Dictionary<KeyCode, UnityAction> _keyBindings = new();
        private Dictionary<BehaviorType, BCIControllerBehavior> _registeredBehaviors = new();

        private void Awake()
        {
            if (_lslMarkerStream == null && !TryGetComponent(out _lslMarkerStream))
            {
                Debug.LogError($"No component of type {typeof(LSLMarkerStream)} found");
                enabled = false;
            }

            if (_lslResponseStream == null && !TryGetComponent(out _lslResponseStream))
            {
                Debug.LogError($"No component of type {typeof(LSLResponseStream)} found");
                enabled = false;
            }

            if (Instance != null)
            {
                Debug.Log("An existing controller instance is already assigned.");
                return;
            }

            Instance = this;
            RegisterKeyBindings();

            if (_dontDestroyActiveInstance)
            {
                DontDestroyOnLoad(this);
            }
        }

        private void Update()
        {
            //Check for key inputs
            foreach (var (keyCode, action) in _keyBindings)
            {
                if (Input.GetKeyDown(keyCode))
                {
                    action?.Invoke();
                }
            }

            if (Input.GetKeyDown(startStopStimulusKeyCode))
                Instance.StartStopStimulus();
            if(Input.GetKeyDown(startAutomatedTrainingKeyCode))
                Instance.StartAutomatedTraining();
            if(Input.GetKeyDown(startIterativeTrainingKeyCode))
                Instance.StartIterativeTraining();
            if(Input.GetKeyDown(startUserTrainingKeyCode))
                Instance.StartUserTraining();
        }

        public void ChangeBehavior(BehaviorType behaviorType)
        {
            if (Instance.ActiveBehavior != null)
            {
                Instance.ActiveBehavior.CleanUp();
            }

            if (Instance._registeredBehaviors.TryGetValue(behaviorType, out var requestedBehavior))
            {
                Instance.ActiveBehavior = requestedBehavior;
                Instance.ActiveBehavior.Initialize(_lslMarkerStream, _lslResponseStream);
                Debug.Log($"New BCI Controller active of type {behaviorType}");
            }
            else
            {
                Debug.LogError($"Unable to find a registered behavior for type {behaviorType}]");
            }
        }

        private void RegisterKeyBindings()
        {
            //Register Object Selection
            _keyBindings.TryAdd(KeyCode.Alpha0, () => { Instance.SelectObject(0); });
            _keyBindings.TryAdd(KeyCode.Alpha1, () => { Instance.SelectObject(1); });
            _keyBindings.TryAdd(KeyCode.Alpha2, () => { Instance.SelectObject(2); });
            _keyBindings.TryAdd(KeyCode.Alpha3, () => { Instance.SelectObject(3); });
            _keyBindings.TryAdd(KeyCode.Alpha4, () => { Instance.SelectObject(4); });
            _keyBindings.TryAdd(KeyCode.Alpha5, () => { Instance.SelectObject(5); });
            _keyBindings.TryAdd(KeyCode.Alpha6, () => { Instance.SelectObject(6); });
            _keyBindings.TryAdd(KeyCode.Alpha7, () => { Instance.SelectObject(7); });
            _keyBindings.TryAdd(KeyCode.Alpha8, () => { Instance.SelectObject(8); });
            _keyBindings.TryAdd(KeyCode.Alpha9, () => { Instance.SelectObject(9); });
        }

        public bool RegisterBehavior(BCIControllerBehavior behavior, bool setAsActive = false)
        {
            if (behavior == null)
            {
                Debug.LogError("Controller Behavior is null");
                return false;
            }

            if (Instance._registeredBehaviors.TryAdd(behavior.BehaviorType, behavior))
            {
                if (setAsActive)
                {
                    Instance.ChangeBehavior(behavior.BehaviorType);
                }

                return true;
            }

            Debug.LogWarning($"Was unable to register a new controller behavior for type {behavior.BehaviorType}");
            return false;
        }

        public void UnregisterBehavior(BCIControllerBehavior behavior)
        {
            if (behavior == null)
            {
                return;
            }

            if (!Instance._registeredBehaviors.TryGetValue(behavior.BehaviorType, out var foundBehavior) ||
                foundBehavior != behavior)
            {
                return;
            }

            Instance._registeredBehaviors.Remove(behavior.BehaviorType);
            Debug.Log($"Unregistered behavior for type {behavior.BehaviorType}");
            
            if (Instance.ActiveBehavior == behavior)
            {
                Instance.ActiveBehavior = null;
                Debug.Log("The active behavior was also removed.");
            }
        }

        //EKL additions
        public void OpenLSLMarkerStream()
        {
            //Todo
            Debug.Log("Doing nothing right now");
        }

        public void CloseLSLMarkerStream()
        {
            //Stop all coroutines?
            //_lslResponseStream.StopAllCoroutines();
            if (_lslMarkerStream == null)
            {
                Debug.Log("No active marker stream to close");
            }
            else
            {
                _lslMarkerStream.CloseMarkerStream();
            }
        }

        public void OpenLSLResponseStream()
        {
            //Todo
            Debug.Log("Doing nothing right now");
        }

        public void CloseLSLResponseStream()
        {
            //Stop all coroutines?
            //_lslResponseStream.StopAllCoroutines();
            if (_lslResponseStream == null)
            {
                Debug.Log("No active response stream to close");
            }
            else
            {
                _lslResponseStream.CloseResponseStream();
            }
        }

        #region Behavior Passthroughs

        public void StimulusOn(bool sendConstantMarkers = true)
        {
            if (Instance.ActiveBehavior == null)
            {
                return;
            }

            Instance.ActiveBehavior.StimulusOn(sendConstantMarkers);
        }

        public void StimulusOff()
        {
            if (Instance.ActiveBehavior == null)
            {
                return;
            }

            Instance.ActiveBehavior.StimulusOff();
        }

        public void StartStopStimulus()
        {
            if (Instance.ActiveBehavior == null)
            {
                return;
            }

            Instance.ActiveBehavior.StartStopStimulus();
        }

        public void SelectObject(int objectIndex)
        {
            if (Instance.ActiveBehavior == null)
            {
                return;
            }

            Instance.ActiveBehavior.SelectObject(objectIndex);
        }

        public void StartAutomatedTraining()
        {
            if (Instance.ActiveBehavior == null)
            {
                return;
            }

            Instance.ActiveBehavior.StartAutomatedTraining();
        }


        public void StartIterativeTraining()
        {
            //EKL Edit - I think this is causing an issue with iterative training with MI
            if (Instance.ActiveBehavior == null || Instance.ActiveBehavior.BehaviorType != BehaviorType.MI)
            {
                return;
            }

            Instance.ActiveBehavior.StartIterativeTraining();
        }

        public void StartUserTraining()
        {
            if (Instance.ActiveBehavior == null)
            {
                return;
            }

            Instance.ActiveBehavior.StartUserTraining();
        }

        #endregion
    }
}