using System;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Recipe))]
public class GameManager : MonoBehaviour
{

    // private Vector2 screenBounds;

    public static int numRecipeTrials = 1;
    public int currentRecipeTrial = 0;


    public static int numIngredients = 5;
    public static string[] ingredientSpritePaths = {
        "applepie/applepie_apple",
        "applepie/applepie_egg",
        "applepie/applepie_flour",
        "cheeseburger/cheeseburger_bun",
        "cheeseburger/cheeseburger_cheese",
        "cheeseburger/cheeseburger_meat",
        "curry/curry_sauce",
        "curry/curry_bellpeppers",
        "curry/curry_steak",
        "strawberrycake/strawberrycake_strawberry",
        "strawberrycake/strawberrycake_butter",
        "strawberrycake/strawberrycake_milk",
        "pizza/pizza_flour",
        "pizza/pizza_tomato",
        "pizza/pizza_cheese",
        "wrong_items/wrong_apples",
        "wrong_items/wrong_bacon",
        "wrong_items/wrong_bagel",
        "wrong_items/wrong_banana",
        "wrong_items/wrong_bulb",
        "wrong_items/wrong_cabbage",
        "wrong_items/wrong_chips",
        "wrong_items/wrong_donut",
        "wrong_items/wrong_duck",
        "wrong_items/wrong_glue",
        "wrong_items/wrong_gummybear",
        "wrong_items/wrong_salmon",
        "wrong_items/wrong_soap",
        "wrong_items/wrong_soda",
        "wrong_items/wrong_toothpaste",
        "wrong_items/wrong_tuna",
        "wrong_items/wrong_watermelon",
    };
    public GameObject[] conveyorBelt = new GameObject[numIngredients];
    public GameObject[] conveyorBeltMovementPaths = new GameObject[numIngredients];
    private GameObject borderPathMovement;

    public GameObject[] recipeTrials = new GameObject[numRecipeTrials];
    public GameObject ingredientObject;
    public GameObject ingredientPrefab;

    public Dictionary <string, object> foodSprites = new Dictionary<string, object>();
    public static Dictionary<string, string[]> recipes = new Dictionary<string, string[]>
    {
        { 
            "applepie", new [] {
                "applepie_apple",
                "applepie_egg",
                "applepie_flour",
                "applepie_dish",
            }
        },
        { 
            "cheeseburger", new [] {
                "cheeseburger_bun",
                "cheeseburger_cheese",
                "cheeseburger_meat",
                "cheeseburger_dish",
            }
        },
        { 
            "curry", new [] {
                "curry_sauce",
                "curry_bellpeppers",
                "curry_steak",
                "curry_dish",
            }
        },
        { 
            "strawberrycake", new [] {
                "strawberrycake_strawberry",
                "strawberrycake_butter",
                "strawberrycake_milk",
                "strawberrycake_dish",
            }
        },
        { 
            "pizza", new [] {
                "pizza_flour",
                "pizza_tomato",
                "pizza_cheese",
                "pizza_dish",
            }
        },
        
    };
    public List<string> recipeNames;

    public string currentRecipeName;
    public string[] currentRecipe;

    public GameObject[] setPathPoints;

    void Awake()
    {
        
        recipeNames = new List<string>(recipes.Keys);

        // float x1 = GameObject.Find("Left Border").GetComponent<SpriteRenderer>().bounds.size.x / 2 + GameObject.Find("Left Border").transform.position.x;
        // float x2 = GameObject.Find("Right Border").transform.position.x - GameObject.Find("Right Border").GetComponent<SpriteRenderer>().bounds.size.x / 2;
        // float y1 = GameObject.Find("Bottom Border").GetComponent<SpriteRenderer>().bounds.size.y / 2 + GameObject.Find("Bottom Border").transform.position.y;
        // float y2 = GameObject.Find("Top Border").transform.position.y - GameObject.Find("Top Border").GetComponent<SpriteRenderer>().bounds.size.y / 2;

        // foodSprites = new Dictionary<string, object>
        // {
        //     {"Food-2", Resources.LoadAll<Sprite>("Sprites/Food-2")}
        // };
    }

    // Start is called before the first frame update
    void Start()
    {

        // Create the recipe trials
        for (int i = 0; i < numRecipeTrials; i++)
        {

            // Get random recipe from recipes dictionary
            KeyValuePair <string, string[]> randomRecipe = GetRandomRecipe();

            recipeTrials[i] = new GameObject();
            recipeTrials[i].name = "Recipe Trial " + i;
            recipeTrials[i].AddComponent<Recipe>();
            recipeTrials[i].GetComponent<Recipe>().ingredients = new GameObject[randomRecipe.Value.Length];
            recipeTrials[i].GetComponent<Recipe>().recipeName = randomRecipe.Key;
            recipeTrials[i].GetComponent<Recipe>().transform.parent = this.transform;

            currentRecipeName = randomRecipe.Key;
            currentRecipe = randomRecipe.Value;

            // Create the ingredients
            for (int j = 0; j < randomRecipe.Value.Length; j++)
            {
                
                recipeTrials[i].GetComponent<Recipe>().ingredients[j] = Instantiate(ingredientPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                recipeTrials[i].GetComponent<Recipe>().ingredients[j].name = $"Recipe {i} - Ingredient {j}";
                
                // Parse the sprite name from the recipes dictionary and load it from the foodSprites dictionary 
                // string[] ingredientSpriteInfo = randomRecipe.Value[j].Split('/');
                // string ingredientSpriteSheet = ingredientSpriteInfo[0];
                // MatchCollection ingredientSpriteIndexMatches = Regex.Matches(ingredientSpriteInfo[1], @"\d+");
                // int ingredientSpriteIndex = Int32.Parse(ingredientSpriteIndexMatches[ingredientSpriteIndexMatches.Count - 1].Value);
                // Sprite ingredientSprite = (foodSprites[ingredientSpriteSheet] as Sprite[])[ingredientSpriteIndex];

                Sprite ingredientSprite = Resources.Load<Sprite>($"Sprites/menu/{randomRecipe.Key}/{randomRecipe.Value[j]}");

                recipeTrials[i].GetComponent<Recipe>().ingredients[j].GetComponent<SpriteRenderer>().sprite = ingredientSprite;
                recipeTrials[i].GetComponent<Recipe>().ingredients[j].GetComponent<SpriteRenderer>().sortingLayerName = "Foreground";
                recipeTrials[i].GetComponent<Recipe>().ingredients[j].GetComponent<SpriteRenderer>().sortingOrder = 1;
                recipeTrials[i].GetComponent<Recipe>().ingredients[j].transform.parent = recipeTrials[i].gameObject.transform;
                
                // ! TODO: Optimize the placement and sizing of the ingredients
                recipeTrials[i].GetComponent<Recipe>().ingredients[j].transform.position = new Vector3(-4.84f + 3.2f*j, 2.8f, 0.0f);
                recipeTrials[i].GetComponent<Recipe>().ingredients[j].transform.localScale = new Vector3(5f, 5f, 5f);
                
                // recipeTrials[i].GetComponent<Recipe>().ingredients[j].AddComponent<BoxCollider2D>();

            }
            
        }

    }

    private int gameLoopCounter = 0;

    // Update is called once per frame
    void Update()
    {

        // gameLoopCounter++;

        if (Time.time > gameLoopCounter) {
            spawnIngredientOnBelt();
            gameLoopCounter += 2;
        }
        
        
    }

    // Generate random integer
    private int GetRandInt(int minimum, int maximum)
    {
        return UnityEngine.Random.Range(minimum, maximum);
    }

    // Create a sprite object
    private GameObject CreateSpriteObject(string name, string spritePath, int sortingOrder, Vector3 position)
    {
        GameObject spriteObject = new GameObject();
        spriteObject.name = name;
        spriteObject.AddComponent<SpriteRenderer>();
        spriteObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(spritePath);
        spriteObject.GetComponent<SpriteRenderer>().sortingOrder = sortingOrder;
        spriteObject.AddComponent<BoxCollider2D>();
        spriteObject.transform.parent = this.gameObject.transform;
        
        // Position the ingredient object
        spriteObject.transform.position = position;

        return spriteObject;
    }

    // Create an ingredient object
    private GameObject CreateIngredientObject(string ingredientName, string spritePath, Vector3 position)
    {
        return CreateSpriteObject(ingredientName, spritePath, 1, position);
    }

    // Get random recipe from recipes dictionary
    // private KeyValuePair<string, string[]> GetRandomRecipe()
    // {
    //     int randomRecipeIndex = GetRandInt(0, recipeNames.Count);
    //     return new KeyValuePair<string, string[]>(recipeNames[randomRecipeIndex], recipes[recipeNames[randomRecipeIndex]]);
    // }

    private KeyValuePair<string, string[]> GetRandomRecipe()
    {
        int randomRecipeIndex = GetRandInt(0, recipeNames.Count);
        return new KeyValuePair<string, string[]>(recipeNames[randomRecipeIndex], recipes[recipeNames[randomRecipeIndex]]);
    }


    private void spawnIngredientOnBelt() {
        
        string randIngredient = ingredientSpritePaths[UnityEngine.Random.Range(0, ingredientSpritePaths.Length)];
        GameObject ingredient = Instantiate(ingredientPrefab, new Vector3(8, -8, 0), Quaternion.identity);
        ingredient.name = $"Ingredient {randIngredient}";

        Sprite ingredientSprite = Resources.Load<Sprite>($"Sprites/menu/{randIngredient}");

        ingredient.GetComponent<SpriteRenderer>().sprite = ingredientSprite;
        ingredient.GetComponent<SpriteRenderer>().sortingLayerName = "Foreground";
        ingredient.GetComponent<SpriteRenderer>().sortingOrder = 0;
        ingredient.transform.parent = ingredient.transform;
        ingredient.transform.localScale = new Vector3(5f, 5f, 5f);
        ingredient.AddComponent<BoxCollider2D>();

        GameObject np = new GameObject();
        np.AddComponent<BorderPathMovement>().name = "path";
        np.GetComponent<BorderPathMovement>().obj = ingredient;
        np.GetComponent<BorderPathMovement>().pathPoints = setPathPoints;
        np.GetComponent<BorderPathMovement>().numPoints = 4;
        np.GetComponent<BorderPathMovement>().speed = 6;
        np.transform.parent = ingredient.transform;

    }


    // private int ingredientCounter = 0;

    // void FixedUpdate() {

    //     int i = ingredientCounter;

    //     if (i < numIngredients) {

    //         conveyorBelt[i] = Instantiate(ingredientPrefab, new Vector3(0, 0, 0), Quaternion.identity);

    //         ingredientCounter++;

    //     }


    // }

}
