using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstructionController : MonoBehaviour
{
    public event EventHandler BtnInstructionsContinueClick;

    public Button btnInstructionsContinue;

    // Start is called before the first frame update
    void Start()
    {
        btnInstructionsContinue.onClick.AddListener(btnInstructionsContinueClick);
    }

    private void btnInstructionsContinueClick()
    {
        BtnInstructionsContinueClick?.Invoke(this, null);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
