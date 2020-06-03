using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> characterItems = new List<Item>();
    public ItemDatabase itemDatabase;
    public InventoryUI inventoryUI;

    private void Start()
    {
        GiveItem("ropes");
        GiveItem(1);
        //RemoveItem(1);
    }

    public void GiveItem(int id)
    {
        Item itemToAdd = itemDatabase.GetItem(id);
        characterItems.Add(itemToAdd);
        inventoryUI.AddNewItem(itemToAdd);
        Debug.Log("Added item " + itemToAdd.resourceName);
    }

    public void GiveItem(string itemType)
    {
        Item itemToAdd = itemDatabase.GetItem(itemType);
        characterItems.Add(itemToAdd);
        Debug.Log("Added item " + itemToAdd.resourceName);
    }

    public Item CheckForItem(int id0)
    {
        return characterItems.Find(item => item.id == id0);
    }

    public void RemoveItem (int id)
    {
        Item itemToRemove = CheckForItem(id);
        if (itemToRemove != null)
        {
            characterItems.Remove(itemToRemove);
            inventoryUI.RemoveItem(itemToRemove);
            Debug.Log("Item removed: " + itemToRemove.resourceName);
        }
    }

}
