using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recipie : MonoBehaviour
{

    private string[] ingredients = new string[3];
    private Sprite[] ingredientSprites = new Sprite[3];

    // Start is called before the first frame update
    void Start()
    {

        for (int i = 0; i < ingredients.Length; i++) {
            // ingredientSprites[i] = Resources.Load<Sprite>("Sprites/" + ingredients[i]);
            // ingredientSprites[i] = Sprite.Create(Resources.Load<Texture2D>("Sprites/" + ingredients[i]), new Rect(0, 0, 100, 100), new Vector2(0.5f, 0.5f));
            ingredientSprites[i] = Sprite.Create(Resources.Load<Texture2D>("Sprites/" + ingredients[i]), new Rect(0, 0, 100, 100), new Vector2(0.5f, 0.5f));
        }

        // mySprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
