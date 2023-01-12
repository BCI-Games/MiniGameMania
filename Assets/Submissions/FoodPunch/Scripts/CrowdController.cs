using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Submissions.FoodPunch;

public class CrowdController : MonoBehaviour
{
    public int desired = 0;
    public bool useAttribute = false;
    public string request = "";
    public Food food;

    public void generateNewFood() {
        desired = Random.Range(0,3);
        useAttribute = Randomizer.RandomBool();

        food = new Food(true, useAttribute, desired);
    }

    public string getRequest() {
        string desiredString = Food.types[desired];
        if(useAttribute) {
            desiredString = Food.attributes[desired];
            return "We want something " + desiredString + "!";
        }
        return "We want " + desiredString + "!";
    }

}
