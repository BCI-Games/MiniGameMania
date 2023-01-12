using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TrainingController : MonoBehaviour
{

    #region Events...

    public event EventHandler BtnStartStopTrainingClick;

    #endregion

    #region Properties...

    public bool TrainingStart { get { return _start; } }


    #endregion

    #region Public Members...

    public Button btnStartStopFlashing;

    #endregion

    #region Private Members...

    private bool _start;
    private TextMeshProUGUI _buttonTxt;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _buttonTxt = GetComponentInChildren<TextMeshProUGUI>();
        _start = false;
        _buttonTxt.text = "Start Flashing";

        //add button event
        btnStartStopFlashing.onClick.AddListener(OnBtnConnectClick);
    }

    private void OnBtnConnectClick()
    {
        if (_start)
        {
            _start = false;
            _buttonTxt.text = "Start Flashing";
        }
        else
        {
            _start = true;
            _buttonTxt.text = "Stop Flashing";
        }
        
        BtnStartStopTrainingClick?.Invoke(this, null);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
