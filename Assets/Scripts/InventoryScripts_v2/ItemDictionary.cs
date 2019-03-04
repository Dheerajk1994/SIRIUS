using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ItemDictionary : MonoBehaviour
{

    //Create the Dictionary
    private Dictionary<ushort, Item> itemDictionary = new Dictionary<ushort, Item>();
    private ItemList listOfItems;

    private string path;

    void Start()
    {
        path = Application.streamingAssetsPath + "/itemDescription.json";
        listOfItems = new ItemList();
        if (File.Exists(path))
        {
            string jsonString = File.ReadAllText(path);
            Debug.Log(jsonString);
            listOfItems = JsonUtility.FromJson<ItemList>(jsonString);

            //Populate dictionary via JSON
            foreach (Item item in listOfItems.itemList)
            {
                Debug.Log(item.itemName);
                //itemDictionary.Add(item.id, item);
            }
        }
        else
        {
            Debug.LogError("ITEMDESCRIPTION FILE CANNOT BE FOUNDD");
        }

    }

    public Item GetItem(ushort id)
    {
        Item itemToReturn;
        if (itemDictionary.TryGetValue(id, out itemToReturn))
        {
            return itemToReturn;
        }
        return null;
    }
}


[System.Serializable]
public class ItemList
{
    public List<Item> itemList = new List<Item>();
}

[System.Serializable]
public class Item
{
    public ushort id;
    public string itemName;
    public string description;
    public ushort stack;
    public uint cost;

    public Item(ushort newId, string newItemName, string newDescription, ushort newStack, uint newCost)
    {
        id = newId;
        itemName = newItemName;
        description = newDescription;
        stack = newStack;
        cost = newCost;
    }
}


