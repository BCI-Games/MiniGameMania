 using UnityEngine;
 using System;
 [Serializable]
public class InventoryItem 
{
     public Item item {get; private set; }
     public int stackSize{get; private set; }
     public InventoryItem(Item n_item){
        item=n_item;
        AddToStack();
     }
     public void AddToStack(){
        stackSize++;
     }
     public void RemoveFromStack(){
        stackSize--;
     }

}
