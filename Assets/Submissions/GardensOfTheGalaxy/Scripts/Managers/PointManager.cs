using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointManager : MonoBehaviour
{
    private static PointManager _instance;

    public static PointManager Instance
    {
        get
        {
            return _instance;
        }
    }

    public AudioSource soundEffect;

    public TMPro.TextMeshProUGUI textMeshPro;
    public int totalPoints = 0;
    public PlanetInteraction[] planets;

    private void Awake()
    {
        _instance = this;
    }

    public int numGlow = 0;

    // Update is called once per frame
    void Update()
    {
        textMeshPro.text = "Points: " + totalPoints;
        if (numGlow >= planets.Length)
        {
            soundEffect.Play();
            totalPoints += 100;
            numGlow = 0;
            Invoke("ResetGlow", 5f);
        }
    }
    public void ResetGlow()
    {
        numGlow = 0;
        foreach (PlanetInteraction p in planets)
        {
            p.ResetGlow();
        }
    }
}
