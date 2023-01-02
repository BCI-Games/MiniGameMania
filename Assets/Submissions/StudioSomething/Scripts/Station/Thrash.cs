using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Submissions.StudioSomething;

public class Thrash : StationBehaviour
{
    
    // Start is called before the first frame update
    
    public override void Action(PlayerController player){
        if (player.HasItem){
            player.InventoryManager.Remove( player.HeldItem);
            Destroy(player.HeldItemObject);
            player.HeldItem=null;
            player.HasItem=false;
            player.FoodSprite.sprite=null;
            
        }
    }
}
