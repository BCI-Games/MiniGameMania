using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{


    public GameManager gameManager;
    public string[] toppings;

    public Transform point1;
    public Transform point2;

    public GameObject Lettuce;
    public GameObject Tomato;
    public GameObject Cheese;
    public GameObject Mushroom;
    public GameObject Onion;
    public GameObject Pepper;

    GameObject object1;
    GameObject object2;

    public GameObject SliderSpawn;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void SetTray(string toSpawn, int location)
    {
       
        Debug.Log(toSpawn);
        GameObject objectToSpawn;
        GameObject secondObjectToSpawn;

        Debug.Log("Instantiating");

        switch (toSpawn)
        {
            case ("Lettuce"):
                objectToSpawn = Lettuce;
                secondObjectToSpawn = Tomato;
                break;
            case ("Tomato"):
                objectToSpawn = Tomato;
                secondObjectToSpawn = Cheese;
                break;
            case ("Cheese"):
                objectToSpawn = Cheese;
                secondObjectToSpawn = Lettuce;
                break;
            case ("Onion"):
                objectToSpawn = Onion;
                secondObjectToSpawn = Lettuce;
                break;
            case ("Pepper"):
                objectToSpawn = Pepper;
                secondObjectToSpawn = Lettuce;
                break;
            case ("Mushroom"):
                objectToSpawn = Mushroom;
                secondObjectToSpawn = Lettuce;
                break;
            default:
                objectToSpawn = Onion;
                secondObjectToSpawn = Lettuce;
                Debug.Log("settray failed");
                break;
        }




        if (location == 1)
        {
            object1 = Instantiate(objectToSpawn, point1.position, Quaternion.identity);
            object2 = Instantiate(secondObjectToSpawn, point2.position, Quaternion.identity);

        }
        else
        {
            object2 = Instantiate(objectToSpawn, point2.position, Quaternion.identity);
            object1 = Instantiate(secondObjectToSpawn, point1.position, Quaternion.identity);
        }
        
    }

    public IEnumerator SpawnTopping (string topping)
    {

        
        Debug.Log("Spawn" + topping);
        GameObject objectToSpawn;

        switch (topping)
        {
            case ("Lettuce"):
                objectToSpawn = Lettuce;
                break;
            case ("Tomato"):
                objectToSpawn = Tomato;
                break;
            case ("Cheese"):
                objectToSpawn = Cheese;
                break;
            case ("Onion"):
                objectToSpawn = Onion;
                break;
            case ("Pepper"):
                objectToSpawn = Pepper;
                break;
            case ("Mushroom"):
                objectToSpawn = Mushroom;
                break;
            default:
                objectToSpawn = Onion;
                Debug.Log("settray failed");
                break;
        }

        object1 = Instantiate(objectToSpawn, SliderSpawn.transform.position, Quaternion.identity);
        gameManager.SetState(GameManager.GameState.INPUT);
        yield return new WaitForSeconds(1);
    }

    public void ClearTray()
    {
        Destroy(object1);
        Destroy(object2);
    }
}
