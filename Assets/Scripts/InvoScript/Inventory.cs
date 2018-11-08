using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Inventory : MonoBehaviour {

    public Image[] itemImages = new Image[numItemSlots];
    public Item[] items = new Item[numItemSlots];

    public const int numItemSlots = 4;      //maximum amount of items in inventory

    public void AddItem (Item itemToAdd)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if(items[i] == null)
            {
                items[i] = itemToAdd;
                itemImages[i].sprite = itemToAdd.sprite;
                itemImages[i].enabled = true;
                return;
            }
        }
    }

    public void RemoveItem(Item itemToremove)
    {
        for(int i = 0; i < items.Length; i++)
        {
            if(items[i] == itemToremove)
            {
                items[i] = null;
                itemImages[i].sprite = null;
                itemImages[i].enabled = false;
                return;
            }
        }
    }
}
