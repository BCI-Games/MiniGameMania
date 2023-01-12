using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditsController : MonoBehaviour
{
    public event EventHandler BtnCreditsBackClick;

    public Button btnCreditsBack;

    // Start is called before the first frame update
    void Start()
    {
        btnCreditsBack.onClick.AddListener(btnCreditsBackClick);
    }

    private void btnCreditsBackClick()
    {
        BtnCreditsBackClick?.Invoke(this, null);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
