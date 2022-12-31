using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class StarUI : MonoBehaviour
{

    public Sprite StarSprite;

    public int StarCount
    {
        get { return starCount; }
        set
        {
            if (value < 0) return;
            starCount = value;
            StarSprites[starCount].gameObject.SetActive(false);
            if (starCount <= 0) GameplayManager.Instance.LoseGame();
        }
    }

    [SerializeField] private List<Image> StarSprites;

    public Color ActiveColor;
    public Color InactiveColor;

    private int starCount = 5;

    public void Reset()
    {

        for (int i = 0; i < StarSprites.Count; i++)
        {
            StarSprites[i].gameObject.SetActive(true);
        }
    }
}
