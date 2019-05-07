using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class ItemDictionary
{
    private static Dictionary<ushort, ItemDescription> itemDictionary = new Dictionary<ushort, ItemDescription>();
    private static ItemList listOfItems;

    private static string path;
    private static bool isItemDictionaryPopulated = false;

    static InventorySpritesScript inventorySpritesScript;

    public static void GenerateDictionary(InventorySpritesScript iss)
    {
        if (!isItemDictionaryPopulated)
        {
            //Debug.Log("generate itemdictionary called");
            inventorySpritesScript = iss;
            path = Application.streamingAssetsPath + "/itemDescription.json";
            if (File.Exists(path))
            {
                string jsonString = File.ReadAllText(path);
                listOfItems = JsonUtility.FromJson<ItemList>(jsonString);

                foreach (ItemDescription item in listOfItems.Items)
                {
                    //Debug.Log(item.itemName);
                    itemDictionary.Add(item.id, item);
                    //Debug.Log(item.itemName + " " + item.type);
                }
                isItemDictionaryPopulated = true;
            }
            else
            {
                Debug.LogError("ITEMDESCRIPTION FILE CANNOT BE FOUNDO");
            }
        }
        
    }

    public static CompleteItem GetItem(ushort fetchID)
    {
        if (fetchID == (ushort)EnumClass.TileEnum.EMPTY) return null;

        ItemDescription output;
        if (itemDictionary.TryGetValue(fetchID, out output ))
        {
            CompleteItem newItem = new CompleteItem();
            newItem.itemDescription = output;
            newItem.icon = inventorySpritesScript.GetSprite(output.id);
            return newItem;
        }
        else
        {
            Debug.LogError("Item ID: " + fetchID + " does not exist in the dictionary");
            return null; }
    }

    //RETURNS AN ITEM OF A CERTAIN TYPE - WILL RETURN THE FIRST ITEM WITH THE TYPE
    public static ItemDescription GetItemOfType(ushort type)
    {
        foreach (KeyValuePair<ushort, ItemDescription> item in itemDictionary)
        {
            if (item.Value.type == type)
            {
                return item.Value;
            }
        }
        return null;
    }

    public static List<ItemDescription> GetItemsOfType(ushort type)
    {
        List<ItemDescription> itemsOfType = new List<ItemDescription>();
        foreach (KeyValuePair<ushort, ItemDescription> item in itemDictionary)
        {
            if (item.Value.type == type)
            {
                itemsOfType.Add(item.Value);
            }
        }
        return itemsOfType;
    }


    public static List<ItemDescription> GetItemsOfTier(ushort tier)
    {
        List<ItemDescription> craftableItemsList = new List<ItemDescription>();

        foreach(KeyValuePair<ushort, ItemDescription> item in itemDictionary)
        {
            if(item.Value.tier == tier)
            {
                craftableItemsList.Add(item.Value);
            }
        }

        return craftableItemsList;
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
    public uint type;
    public ushort tier;
    public ushort [] recipe;
}

public class CompleteItem
{
    public ItemDescription itemDescription;
    public Sprite icon;

    public CompleteItem()
    {

    }
    public CompleteItem(ItemDescription newItem, Sprite newIcon)
    {
        itemDescription = newItem;
        icon = newIcon;
    }

}




