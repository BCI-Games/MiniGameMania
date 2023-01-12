using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    public TextMeshProUGUI Text;

    private int _prevScore;
    // Start is called before the first frame update
    void Start()
    {
        _prevScore = -1;
    }

    // Update is called once per frame
    void Update()
    {

        int score = ScoreCounter.Instance.Score;
        if(_prevScore != score)
        {
            Text.text = string.Format("Score: {0}", score);
        }
        _prevScore = score;
    }
}
