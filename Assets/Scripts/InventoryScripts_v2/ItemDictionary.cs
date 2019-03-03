using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDictionary : MonoBehaviour
{

    //Create the Dictionary
    Dictionary<int, Item> items = new Dictionary<int, Item>();

    // Use this for initialization
    void Start()
    {
        //Adding item variables to be added to the dictionary
        //Use an item constructor
        //Item constructor consists of (String itemName, string itemDetail, Sprite icon, int cost, ItemTypes itemtype, int currentStack, int maxStack) 
        Item dirt = new Item("Dirt");

        //Add Variables to the dictionary
        //items.Add("ID", <item variable from above>)
        items.Add(1, dirt);
    }

    public Item getItem(int ID)
    {
        Debug.Log("ID sent: " + ID);
        Debug.Log(items.Values);
        return items[ID];
    }
}
