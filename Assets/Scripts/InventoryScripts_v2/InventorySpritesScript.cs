using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "New Sprite", menuName = "Inventory/New Sprite")]
public class InventorySpritesScript : MonoBehaviour {

    public static Sprite[] itemSprites;

    public Sprite GetSprite(ushort index){
        Debug.Log("Getsprite called");
        if(index < itemSprites.Length){
            return itemSprites[index];
        }
        Debug.Log("Getsprite return null");
        return null;
    }
}
