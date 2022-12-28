using System;
using BCIEssentials.Controllers;
using UnityEngine;
using UnityEngine.Events;

namespace Submissions.BoatRescue
{
    public class InputManager : MonoBehaviour, IInitializable
    {
        [Serializable]
        private class InputBindings
        {
            public KeyCode OnConfirmKey;
            public KeyCode OnCancelKey;
            public KeyCode OnMoveUpKey;
            public KeyCode OnMoveDownKey;
            public KeyCode OnMoveLeftKey;
            public KeyCode OnMoveRightKey;
        }

        [Space] [SerializeField] private InputBindings _inputBindings;

        public static InputManager Instance;

        public UnityEvent<PlayerMoveDirection> OnDirectionTriggered = new();
        public UnityEvent OnConfirmTriggered = new();
        public UnityEvent OnCancelTriggered = new();
        public void Initialize()
        {
            Instance = this;
        }

        public void ToggleStimulusInput(bool enable)
        {
            if (BCIController.Instance == null)
            {
                return;
            }

            Debug.Log("<b>Starting Stim</b>");
            BCIController.Instance.StartStopStimulus();
        }

        private void Update()
        {
            if (Input.GetKeyDown(_inputBindings.OnConfirmKey))
            {
                OnConfirmTriggered?.Invoke();
            }
            else if (Input.GetKeyDown(_inputBindings.OnCancelKey))
            {
                OnCancelTriggered?.Invoke();
            }
            else if (Input.GetKeyDown(_inputBindings.OnMoveUpKey))
            {
                OnDirectionTriggered?.Invoke(PlayerMoveDirection.Up);
            }
            else if (Input.GetKeyDown(_inputBindings.OnMoveDownKey))
            {
                OnDirectionTriggered?.Invoke(PlayerMoveDirection.Down);
            }
            else if (Input.GetKeyDown(_inputBindings.OnMoveLeftKey))
            {
                OnDirectionTriggered?.Invoke(PlayerMoveDirection.Left);
            }
            else if (Input.GetKeyDown(_inputBindings.OnMoveRightKey))
            {
                OnDirectionTriggered?.Invoke(PlayerMoveDirection.Right);
            }
        }
    }
}