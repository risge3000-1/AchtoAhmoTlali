using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairingMaterialsScript : MonoBehaviour
{
    bool isPlayerOnARuin = false;
    public bool wasIGeneratedFromARuin = false;
    bool doIHavePriority = true;
    public string materialName,
                  optionsMessageToExport;
    public InventoryUI inventoryUI;
    
    private void Start()
    {
        PlayerMovement.IAmInABrokenRuin += ThereIsARuinOvelappingMe;
        PlayerMovement.IAmNotInARuin += NoRuinOvelapsMeAnymore;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerMovement>() )
        {
            if (inventoryUI.ItemsUI.FindIndex(i => i.item == null) == -1)
                optionsMessageToExport = "Inventory full";
            
            else
                optionsMessageToExport  = "E - Pick up " + materialName; 
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerMovement>() != null && !isPlayerOnARuin && doIHavePriority)
        {
            if (Input.GetKeyDown(KeyCode.E) && inventoryUI.ItemsUI.FindIndex(i => i.item == null) != -1)
                AddToInventory();
        }

        if (other.gameObject.GetComponent<RepairingMaterialsScript>() != null && wasIGeneratedFromARuin)
            doIHavePriority = false;
        
        else
            doIHavePriority = true;
    }

    void ThereIsARuinOvelappingMe()
    {
        isPlayerOnARuin = true;
    }

    void NoRuinOvelapsMeAnymore()
    {
        isPlayerOnARuin = false;
    }

    void AddToInventory()
    {
        PlayerMovement.aGameItemGotPickedUp = true;
        PlayerMovement.materialTypeToGive = materialName;
        
        gameObject.SetActive(false);
    }

    public void IWasGeneratedFromARuinDestruction()
    {
        wasIGeneratedFromARuin = true;
    }
}
