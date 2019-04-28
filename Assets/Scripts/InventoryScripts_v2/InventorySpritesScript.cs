using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "New Sprite", menuName = "Inventory/New Sprite")]
public class InventorySpritesScript : MonoBehaviour {

    public Sprite[] itemSprites;
    public static InventorySpritesScript instance;

    private void Awake()
    {
        ItemDictionary.GenerateDictionary(this);
        instance = this;
    }


    public Sprite GetSprite(ushort index){
        if(index < itemSprites.Length){
            return itemSprites[index];
        }
        return null;
    }
}
