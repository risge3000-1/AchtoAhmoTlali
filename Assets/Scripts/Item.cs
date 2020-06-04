using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public int id;
    public string resourceName;
    public Sprite icon;
    public Dictionary<string, int> ItemsDict = new Dictionary<string, int>();

    public Item (int id, string resourceName)
    {
        this.id = id;
        this.resourceName = resourceName;
        icon = Resources.Load<Sprite>("aesthetics/itemIcons/" + resourceName);
    }

    public Item(Item Item)
    {
        this.id = Item.id;
        this.resourceName = Item.resourceName;
        icon = Resources.Load<Sprite>("aesthetics/itemIcons/" + Item.resourceName);
    }
}
