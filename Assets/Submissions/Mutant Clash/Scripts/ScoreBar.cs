using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBar : MonoBehaviour
{
    public Image positiveBar;
    public Image negativeBar;

    public void SetScore(int score)
    {
        if(score >= 0)
        {
            positiveBar.fillAmount = score / 7f;
            negativeBar.fillAmount = 0;
        }
        else
        {
            positiveBar.fillAmount = 0;
            negativeBar.fillAmount = -score / 7f;
        }
    }
}
