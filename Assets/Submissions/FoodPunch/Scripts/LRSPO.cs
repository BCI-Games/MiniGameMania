using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Submissions.FoodPunch;

public class LRSPO : SPO
{
    public GameController gameController;
    public float value = 0f;

    public string lable = "";

    public override float TurnOn() { return Time.time; }
    public override void TurnOff() { }

    public override void OnSelection() {
        if(lable == "left") {
            gameController.left();
        } else if(lable == "right") {
            gameController.right();
        }
    }
}
