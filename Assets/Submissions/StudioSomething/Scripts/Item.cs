 
using UnityEngine;
using System;
[Serializable]
[CreateAssetMenu(fileName="New Item",menuName="Item/Create New Item")]
public class Item :ScriptableObject
{

    public string itemName;
    public Sprite icon;
    public bool cookable= false;
    public GameObject prefab; [System.Serializable]
    public struct Recipe
    {
        public Item[] Items;
    }

    public Recipe[] recipes;

    // Start is called before the first frame update
     
}
