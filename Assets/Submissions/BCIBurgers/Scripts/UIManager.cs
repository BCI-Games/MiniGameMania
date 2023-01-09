using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{

    public GameManager gameManager;
    public List<string> order;

    public Sprite Lettuce;
    public Sprite Tomato;
    public Sprite Onion;
    public Sprite Cheese;
    public Sprite Pepper;
    public Sprite Mushroom;
    public Sprite GreenCheck;

    public Image image1;
    public Image image2;
    public Image image3;
    public Image image4;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator DrawOrder()
    {

        Debug.Log("draworder");

        order = gameManager.order;

        Sprite spriteToDraw;
        int i = 0;
        Image imageToDrawOn;


        foreach (string topping in order)
        {

            yield return new WaitForSeconds(1);

            switch (topping)
            {
                case ("Lettuce"):
                    spriteToDraw = Lettuce;
                    break;
                case ("Tomato"):
                    spriteToDraw = Tomato;
                    break;
                case ("Cheese"):
                    spriteToDraw = Cheese;
                    break;
                case ("Onion"):
                    spriteToDraw = Onion;
                    break;
                case ("Pepper"):
                    spriteToDraw = Pepper;
                    break;
                case ("Mushroom"):
                    spriteToDraw = Mushroom;
                    break;
                default:
                    spriteToDraw = Onion;
                    Debug.Log("DrawOrder order broke");
                    break;
            }

            switch (i)
            {
                case (0):
                    imageToDrawOn = image1;
                    break;
                case (1):
                    imageToDrawOn = image2;
                    break;
                case (2):
                    imageToDrawOn = image3;
                    break;
                case (3):
                    imageToDrawOn = image4;
                    break;
                default:
                    imageToDrawOn = image1;
                    break;
            }

            imageToDrawOn.sprite = spriteToDraw;

            i++;
        }


        gameManager.SetState(GameManager.GameState.INPUT);


    }

    public void Correct(int index)
    {

        Image imageToDrawOn;

        switch (index)
        {
            case (0):
                imageToDrawOn = image1;
                break;
            case (1):
                imageToDrawOn = image2;
                break;
            case (2):
                imageToDrawOn = image3;
                break;
            case (3):
                imageToDrawOn = image4;
                break;
            default:
                imageToDrawOn = image1;
                break;
        }

        imageToDrawOn.sprite = GreenCheck;

    }



}
