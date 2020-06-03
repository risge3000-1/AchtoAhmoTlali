using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public List<ItemUI> ItemsUI = new List<ItemUI>();
    public GameObject slotPrefab;
    public Transform slotPanel;
    int numberOfSlots = 10;

    private void Awake()
    {
        for (int i = 0; i < numberOfSlots; i++)
        {
            GameObject instantiate = Instantiate(slotPrefab);
            instantiate.transform.SetParent(slotPanel);
            ItemsUI.Add(instantiate.GetComponentInChildren<ItemUI>());
        }
    }

    public void UpdateSlot(int slot, Item item)
    {
        ItemsUI[slot].UpdateItem(item);
    }

    public void AddNewItem(Item item)
    {
        UpdateSlot(ItemsUI.FindIndex(i => i.item == null), item);
    }
    public void RemoveItem(Item item)
    {
        UpdateSlot(ItemsUI.FindIndex(i => i.item == item), null);
    }
}
