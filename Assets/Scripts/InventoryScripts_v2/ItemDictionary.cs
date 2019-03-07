using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ItemDictionary : MonoBehaviour
{
    private Dictionary<ushort, ItemDescription> itemDictionary = new Dictionary<ushort, ItemDescription>();
    private ItemList listOfItems;

    private string path;

    void Start()
    {
        path = Application.streamingAssetsPath + "/itemDescription.json";
        if (File.Exists(path))
        {
            string jsonString = File.ReadAllText(path);
            listOfItems = JsonUtility.FromJson<ItemList>(jsonString);

            foreach (ItemDescription item in listOfItems.Items)
            {
                Debug.Log(item.itemName);
                itemDictionary.Add(item.id, item);
            }
        }
        else
        {
            Debug.LogError("ITEMDESCRIPTION FILE CANNOT BE FOUNDD");
        }

    }

    public ItemDescription getItem(ushort fetchID)
    {
        ItemDescription output;
        if (itemDictionary.TryGetValue(fetchID, out output ))
        {
            return output;
        }
        else
        {
            Debug.LogError("Item ID: " + fetchID + " does not exist in the dictionary");
            return null; }
    }
}


[System.Serializable]
public class ItemList
{
    public List<ItemDescription> Items;
}

[System.Serializable]
public class ItemDescription
{
    public string itemName;
    public ushort id;
    public string description;
    public ushort stackAmnt;
    public uint cost;
}


