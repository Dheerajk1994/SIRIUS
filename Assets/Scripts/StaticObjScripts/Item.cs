using UnityEngine;
using System.Collections;

public class Item : ScriptableObject
{
    public string name;
    public Sprite sprite;
    public GameObject itemObject;

    public Item(string newName)
    {
        name = newName;
    }
}
