using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class InventoryManager : MonoBehaviour

{
    public static InventoryManager current ;
    [SerializeField]
    private Dictionary<Item,InventoryItem> item_dict;
    [SerializeField]
    public List<InventoryItem> inventory{get; private set; }
    private void Awake(){
        current=this;
        inventory=new List<InventoryItem>();
        item_dict=new Dictionary<Item,InventoryItem>();
    }
    public InventoryItem Get(Item item){
        if(item_dict.TryGetValue(item, out InventoryItem value)){
            return value;

        }
        return null;

    }
    // Start is called before the first frame update
   public void Add(Item item){
   
    if (item_dict.TryGetValue(item, out InventoryItem value)){
        value.AddToStack();
    }
    else{
        InventoryItem newItem=new InventoryItem(item);
        inventory.Add(newItem);
        item_dict.Add(item,newItem);
    }
    foreach(var i in inventory){
        Debug.Log(i.item.name);


    }


   }
    public void Remove(Item item){
        if (item_dict.TryGetValue(item, out InventoryItem value)){
            value.RemoveFromStack();
            if(value.stackSize==0){
                inventory.Remove(value);
                item_dict.Remove(item);
            }
        }

    }
}
