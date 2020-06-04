using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> characterItems = new List<Item>();
    public ItemDatabase itemDatabase;
    public InventoryUI inventoryUI;

    bool doOnce = false;

    private void Update()
    {
        if (!doOnce)
        {
            
            doOnce = true;
        }


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
        inventoryUI.AddNewItem(itemToAdd);
        Debug.Log("Added item " + itemToAdd.resourceName);
    }

    public Item CheckForItem(int id0)
    {
        return characterItems.Find(item => item.id == id0);
    }

    public Item CheckForItem(string typeOfMaterial)
    {
        return characterItems.Find(item => item.resourceName == typeOfMaterial);
    }

    public void ChecKForTwoMaterials(string material0, string material1)
    {
        
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

    public void RemoveItem(string itemType)
    {
        Item itemToRemove = CheckForItem(itemType);
        if (itemToRemove != null)
        {
            characterItems.Remove(itemToRemove);
            inventoryUI.RemoveItem(itemToRemove);
            Debug.Log("Item removed: " + itemToRemove.resourceName);
        }
    }

}
