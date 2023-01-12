using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Submissions.BrainBuddy
{
    public class MenuController : MonoBehaviour
    {
        #region Events...

        public event EventHandler BtnConnectClick;
        public event EventHandler BtnCreditsClick;

        #endregion

        #region Properties...

        public string SelectedSerial { get { return _selectedSerial; } }

        #endregion

        #region Public Members...

        public Button btnCredits;
        public Button btnConnect;
        public TMP_Dropdown ddDevices;

        #endregion

        #region Private Members...

        private string _selectedSerial = string.Empty;
        #endregion

        // Start is called before the first frame update
        void Start()
        {
            //get available devices
            List<string> serials = BCIManager.Instance.GetAvailableDevices();

            _selectedSerial = ddDevices.options[ddDevices.value].text;

            //write devices to dialog
            ddDevices.ClearOptions();
            ddDevices.AddOptions(serials);

            //add button event
            btnConnect.onClick.AddListener(OnBtnConnectClick);
            btnCredits.onClick.AddListener(OnBtnCreditsClick);
        }

        private void OnBtnCreditsClick()
        {
            BtnCreditsClick?.Invoke(this, null);
        }

        private void OnBtnConnectClick()
        {
            //update selected serial
            _selectedSerial = ddDevices.options[ddDevices.value].text;
            BtnConnectClick?.Invoke(this, null);
        }

        // Update is called once per frame
        void Update()
        {
            //DO NOTHING 
        }
    }
}