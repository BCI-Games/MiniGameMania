using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerBar : MonoBehaviour
{
    float timerMax;
    float timer;

    Image barImage;

    void Awake()
    {
        barImage = GetComponent<Image>();
        gameObject.SetActive(false);
    }

    public void StartTimer(float time)
    {
        if(time == 0)
        {
            gameObject.SetActive(false);
            return;
        }

        timer = timerMax = time - Time.deltaTime;
        barImage.fillAmount = 1;
        gameObject.SetActive(true);
    }

    void Update()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                timer = 0;
                gameObject.SetActive(false);
            }

            barImage.fillAmount = timer / timerMax;
        }
    }
}
