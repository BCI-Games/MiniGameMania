using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    public Item item;
    
    // Start is called before the first frame update
     public void pickup(){
        InventoryManager.current.Add(item);
        
     }         
     public void remove(){
      InventoryManager.current.Remove(item);
     }
}
