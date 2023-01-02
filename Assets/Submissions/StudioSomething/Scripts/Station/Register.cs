using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Submissions.StudioSomething;

public class Register : StationBehaviour
{
    
    // Start is called before the first frame update
    
    public override void Action(PlayerController player){
        if (player.HasItem){
            player.InventoryManager.Remove( player.HeldItem);
            Destroy(player.HeldItemObject);
            CustomerManager.Instance.Customers[0].GetComponent<CustomerController>().EvaluateOrder(player.HeldItem);
            player.HeldItem=null;
            player.HasItem=false;
            player.FoodSprite.sprite=null;
        }
    }
}
