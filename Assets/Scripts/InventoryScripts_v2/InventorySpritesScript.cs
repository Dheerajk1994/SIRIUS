using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "New Sprite", menuName = "Inventory/New Sprite")]
public class InventorySpritesScript : MonoBehaviour {

    public Sprite[] itemSprites;
    public Sprite[] enemieSprites;
    public static InventorySpritesScript instance;

    private void Awake()
    {
        instance = this;
        ItemDictionary.GenerateDictionary(this);
        instance = this;
    }


    public Sprite GetSprite(ushort index){
        if(index < itemSprites.Length){
            return itemSprites[index];
        }
        return null;
    }

    public Sprite GetEnemieSrite(EnumClass.EnemyEnum id)
    {
        if((int)id < enemieSprites.Length)
        {
            return enemieSprites[(int)id];
        }
        return null;
    }
}
