using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Quitter : MonoBehaviour
{
    public Camera MainCamera;
    public Button btnQuit;
    public TextMeshProUGUI Text;

    private bool _updatedScore;
    // Start is called before the first frame update
    void Start()
    {
        MainCamera.orthographicSize = 5;
        MainCamera.transform.position = new Vector3(0, 0, -10);
        btnQuit.onClick.AddListener(btnQuitClick);
        _updatedScore = false;
    }

    private void btnQuitClick()
    {
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        MainCamera.orthographicSize = 5;
        MainCamera.transform.position = new Vector3(0, 0, -10);

        if(!_updatedScore && FinishedController.Instance.FinishReached )
        {
            Text.text += String.Format("\n\nYour Score: {0}", ScoreCounter.Instance.Score);
            _updatedScore = true;
        }
    }
}
