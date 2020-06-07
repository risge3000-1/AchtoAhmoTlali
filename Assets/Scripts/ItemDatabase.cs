using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ItemDatabase : MonoBehaviour
{
    public List<Item> items = new List<Item>();

    private void Awake()
    {
        BuildDatabase();
    }

    public Item GetItem(int id)
    {
        return items.Find(item => item.id == id);
    }

    public Item GetItem(string materialName)
    {
        return items.Find(item => item.resourceName == materialName);
    }

    void BuildDatabase()
    {
        items = new List<Item>() 
        {
            new Item(0, "wood"),
            new Item(1, "stone"),
            new Item(2, "ropes"),
            new Item(3, "clay")
        };
    }
}
