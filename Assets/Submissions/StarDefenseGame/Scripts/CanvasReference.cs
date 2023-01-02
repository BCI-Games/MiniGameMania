using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CanvasReference : MonoBehaviour
{
    public static CanvasReference Instance;

    public Slider rightWallHealth;
    public Slider leftWallHealth;
    public TextMeshProUGUI turnDisplay;
    public GameObject playerUpcoming;
    public GameObject enemyUpcoming;
    public GameObject prepare;
    public GameObject watch;
    public GameObject statusDisplay;
    public TextMeshProUGUI statusText;

    private void Awake()
    {
        Instance = this;

    }
}
