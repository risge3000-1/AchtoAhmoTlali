using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class RepairableRuin : MonoBehaviour
{
    public bool haveIBeenRepaired = false,
                hasNotChangedColorsYet = true,
                isPlayerNearMe = false,
                canIBeRepaired = false;

    public List<Item> itemsINeed = new List<Item>();
    public ItemDatabase itemDatabase;
    public InventoryUI inventoryUI;

    public string historyWhenNotRepaired;
    public string historyWhenRepaired;
    public string material0, material1;

    Light RuinLight;

    Color newColorOfLight;

    private void Start()
    {
        RuinLight = gameObject.GetComponentInChildren<Light>();

        Item item0ToAdd = itemDatabase.GetItem(materialName:material0);
        Item item1ToAdd = itemDatabase.GetItem(materialName:material1);
        itemsINeed.Add(item0ToAdd);
        itemsINeed.Add(item1ToAdd);

        Debug.Log(itemsINeed[0]);
        Debug.Log(itemsINeed[1]);
    }

    public void Update()
    {
        if (hasNotChangedColorsYet)
        {
            if (haveIBeenRepaired)
                newColorOfLight = Color.red;
            else
                newColorOfLight = Color.green;

            hasNotChangedColorsYet = false;
            
            RuinLight.color = newColorOfLight;
        }

        if (isPlayerNearMe)
        {
            if (RuinLight.intensity <= 5)
            {
                RuinLight.intensity += Time.deltaTime * 4;
            }  
        }
        else
        {
            RuinLight.intensity -= Time.deltaTime;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        int validItemCounter = 0;

        
        if (other.gameObject.GetComponent<PlayerMovement>() != null)
        {
            isPlayerNearMe = true;
            if (RuinLight.intensity < 0)
            {
                RuinLight.intensity = 0;
            }

            

            for (int i = 0; i < inventoryUI.ItemsUI.Count; i++)
            {
                Debug.Log((inventoryUI.ItemsUI[i].item == itemsINeed[0]) + " " + (inventoryUI.ItemsUI[i].item == itemsINeed[1]));
                
                //if the item in the inventory slot of the player is equal to one of the required materials of the ruin, increase counter
                if (inventoryUI.ItemsUI[i].item == itemsINeed[0] || inventoryUI.ItemsUI[i].item == itemsINeed[1])
                {
                    validItemCounter++;
                }
            }

            if (validItemCounter >= 2)
                canIBeRepaired = true;
            else
                canIBeRepaired = false;
        }


    }

    private void OnTriggerStay(Collider other)
    {
      
        
        //if I'm colliding with the player AND I haven't been repaired
        if (other.gameObject.GetComponent<PlayerMovement>() != null && !haveIBeenRepaired && canIBeRepaired)
        {
            if (Input.GetKeyDown(KeyCode.E)) //decides to repair it
            {
                haveIBeenRepaired = true;
                hasNotChangedColorsYet = true;
                PlayerMovement.aRuinGotRepaired = true;
                other.gameObject.GetComponent<Inventory>().RemoveItem(itemType: itemsINeed[0].resourceName);
                other.gameObject.GetComponent<Inventory>().RemoveItem(itemType: itemsINeed[1].resourceName);
            }
            else if (Input.GetKeyDown(KeyCode.F)) //decides to destroy it
            {
                PlayerMovement.aRuinHasBeenDestroyed = true;
                gameObject.SetActive(false);
            }
        }


    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerMovement>() != null)
            isPlayerNearMe = false;
    }

    public string ExportStoryText()
    {
        if (haveIBeenRepaired)
        {
            Debug.Log("I read that i need to return the repaired text wich is \"" + historyWhenRepaired + "\"");
            return historyWhenRepaired;
        }   
        else
        {
            Debug.Log("I read that i need to return the incomplete text wich is \"" + historyWhenNotRepaired + "\"");
            return historyWhenNotRepaired;
        }
            
    }
}
