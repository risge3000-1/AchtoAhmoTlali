using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking.Match;

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

            //set cloned gameObject near because I Don't know why unity sets them at 500 scale, rotated and misplaced in the lines above
            instantiate.transform.localScale = new Vector3(1, 1, 1);
            instantiate.transform.localPosition = new Vector3(0, 0, 0);
            instantiate.transform.localRotation = new Quaternion(0, 0, 0, 1);
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
