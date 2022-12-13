using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Submissions.BoatRescue
{
    public class SpoButton : SPO
    {
        private enum ButtonType
        {
            Select,
            Cancel,
        }

        [SerializeField] private Image _buttonImage;
        [SerializeField] private ButtonType _type;
        [SerializeField] protected UnityEvent _onInputReceived;

        public Color selectedColor;

        private bool _registerOnStart;

        private void Start()
        {
            if (_registerOnStart)
            {
                Register();
            }
        }

        private void OnEnable()
        {
            if (InputManager.Instance != null)
            {
                Register();
            }
            else
            {
                _registerOnStart = true;
            }
        }

        private void OnDisable()
        {
            Unregister();
        }

        public override float TurnOn()
        {
            _buttonImage.color = onColour;
            return Time.time;
        }

        public override void TurnOff()
        {
            _buttonImage.color = offColour;
        }

        public override void OnSelection()
        {
            _onInputReceived?.Invoke();
            _buttonImage.color = selectedColor;
            Debug.Log($"SPO button received input: {gameObject.name}");
        }

        protected virtual void Register()
        {
            includeMe = true;

            switch (_type)
            {
                case ButtonType.Select:
                    InputManager.Instance.OnConfirmTriggered.AddListener(OnSelection);
                    break;
                case ButtonType.Cancel:
                    InputManager.Instance.OnCancelTriggered.AddListener(OnSelection);
                    break;
            }
        }

        protected virtual void Unregister()
        {
            if (InputManager.Instance == null)
            {
                return;
            }

            includeMe = false;

            switch (_type)
            {
                case ButtonType.Select:
                    InputManager.Instance.OnConfirmTriggered.RemoveListener(OnSelection);
                    break;
                case ButtonType.Cancel:
                    InputManager.Instance.OnCancelTriggered.RemoveListener(OnSelection);
                    break;
            }
        }
    }
}