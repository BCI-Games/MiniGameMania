using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Submissions.StarDefense;

public class UpcomingDisplay : MonoBehaviour
{
    public Sprite sprite;
    public Selection.SelectionType selectionType;
    public int index;
    public bool rightToLeft = false;
    private float speed = 300;
    private float decayTimer = 1;
    private bool decayActive = false;

    private void Update()
    {
        if (decayActive)
        {
            decayTimer -= Time.deltaTime;
            GetComponent<Image>().color = new Color(1, 1, 1, decayTimer);
            if (decayTimer <= 0)
            {
                Destroy(gameObject);
            }
        }
        
        if(rightToLeft == false)
        {
            Move(1);
        }
        else if (rightToLeft)
        {
            Move(-1);
        }

    }

    public void Decay()
    {
        //Debug.Log("test");
        decayActive = true;
    }

    public void Setup(Sprite sprite, Selection.SelectionType type, bool rightToLeft, int i)
    {
        this.sprite = sprite;
        GetComponent<Image>().sprite = sprite;
        this.selectionType = type;
        this.rightToLeft = rightToLeft;
        this.index = i;

    }

    private void Move(int multipler)
    {
        //left to right by default
        bool check = false;
        if(multipler > 0)
        {
            check = transform.localPosition.x < index * 100 * -multipler;
        }
        else
        {
            check = transform.localPosition.x > index * 100 * -multipler;
        }
        if (check)
        {
            transform.localPosition += new Vector3(Time.deltaTime * speed * multipler, 0, 0);
        }
        else
        {
            transform.localPosition = new Vector3(index*100*-multipler, 0, 0);
        }
    }

}
